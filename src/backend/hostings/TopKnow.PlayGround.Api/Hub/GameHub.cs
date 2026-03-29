using System.Collections.Concurrent;
using System.ComponentModel;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TopKnow.Modules.PlayGround.Commands.Matches;

namespace TopKnow.PlayGround.Api;

public class UserInfoDto
{
    public Guid Id { get; set; }
    public string DisplayName { get; set; }
}

public class ConnectionInfo
{
    public string ConnectionId { get; set; }
    public string DiplayName { get; set; }
}

[Authorize]
public class GameHub : Hub
{
    public GameHub(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public static ConcurrentDictionary<Guid, ConnectionInfo> waitingPlayers = new ConcurrentDictionary<Guid, ConnectionInfo>();
    public static ConcurrentDictionary<Guid, ConnectionInfo> players = new ConcurrentDictionary<Guid, ConnectionInfo>();
    private Guid currentUserId;
    private string currentUserDisplayName;
    private readonly IMediator mediator;

    public async Task Join()
    {
        GetUserInformation();
        if (waitingPlayers.ContainsKey(currentUserId))
        {
            return;
        }
        waitingPlayers.TryAdd(currentUserId, new ConnectionInfo { ConnectionId = Context.ConnectionId, DiplayName = currentUserDisplayName });
        await Clients.All.SendAsync("LobbyChanged", waitingPlayers.Count);
        await Task.Delay(2000);
        await Clients.Caller.SendAsync("Joined");

        var otherUsers = waitingPlayers.Where(f => f.Key != currentUserId)
                                       .OrderBy(f => Guid.NewGuid())
                                       .Take(5)
                                       .Select(s => new UserInfoDto
                                       {
                                           Id = s.Key,
                                           DisplayName = s.Value.DiplayName
                                       })
                                       .ToList();

        await Clients.Caller.SendAsync("OpponentsAssigned", otherUsers);
    }

    public async Task AskForChallenge(Guid opponentId)
    {
        GetUserInformation();
        if (waitingPlayers.TryGetValue(opponentId, out var opponent))
        {
           await Clients.Client(opponent.ConnectionId).SendAsync("ChallengeRequested", currentUserId, currentUserDisplayName);
        }
    }

    public async Task AcceptChallenge(Guid opponentId)
    {
        GetUserInformation();
        if (waitingPlayers.TryRemove(opponentId, out var opponent))
        {
            waitingPlayers.TryRemove(currentUserId, out var currentUser);
            await Clients.All.SendAsync("LobbyChanged", waitingPlayers.Count);

            players.TryAdd(opponentId, opponent);
            players.TryAdd(currentUserId, currentUser);

            await mediator.Send(new CreateMatchRequest(currentUserId, opponentId));
            // game id iyi olabilir
            await Clients.Client(opponent.ConnectionId).SendAsync("GameStarted");
            await Clients.Caller.SendAsync("GameStarted");
        }
    }

    public override Task OnConnectedAsync()
    {
        return Clients.Caller.SendAsync("LobbyChanged", waitingPlayers.Count, CancellationToken.None);
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        GetUserInformation();
        if (waitingPlayers.TryGetValue(currentUserId, out var connectionId))
        {
            waitingPlayers.Remove(currentUserId, out connectionId);
            await Clients.All.SendAsync("LobbyChanged", waitingPlayers.Count);
        }
        await base.OnDisconnectedAsync(exception);
    }

    private void GetUserInformation()
    {
        var idClaim = Context.User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.NameIdentifier);
        if (idClaim is null)
        {
            throw new InvalidOperationException("Unauthorized Cccess");
        }

        currentUserId = Guid.Parse(idClaim.Value);
        currentUserDisplayName = Context.User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.Name).Value;
    }
}
