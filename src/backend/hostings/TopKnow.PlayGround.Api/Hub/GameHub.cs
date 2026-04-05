using System.Collections.Concurrent;
using System.ComponentModel;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TopKnow.Modules.PlayGround.Commands.Matches;
using TopKnow.Modules.PlayGround.Queries.Questions;

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

    private static ConcurrentDictionary<Guid, Guid> waitingForMatch = new ConcurrentDictionary<Guid, Guid>();

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

            var match = await mediator.Send(new CreateMatchRequest(currentUserId, opponentId));

            if (match.IsSuccess)
            {
                await Clients.Client(opponent.ConnectionId).SendAsync("GameStarted", match.Value);
                await Clients.Caller.SendAsync("GameStarted", match.Value);
            }
        }
    }

    public async Task UserIsReady(Guid id)
    {
        //Daha önce rakiplerden biri ilk defa hazýrým dediyse
        if (!waitingForMatch.ContainsKey(id))
        {
            GetUserInformation();
            waitingForMatch.TryAdd(id, currentUserId);
            return;
        }

        waitingForMatch.TryRemove(id, out var _);

        var question = await mediator.Send(new GetRandomQuestionsRequest(id), CancellationToken.None);
        if (question.IsSuccess)
        {
            var p1 = players.TryGetValue(question.Value.LeftUserId, out var pc1);
            var p2 = players.TryGetValue(question.Value.RightUserId, out var pc2);

            await Clients.Client(pc1.ConnectionId).SendAsync("LoadQuestion", question.Value.QuestionId);
            await Clients.Client(pc2.ConnectionId).SendAsync("LoadQuestion", question.Value.QuestionId);
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

        players.TryRemove(currentUserId, out _);
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
