using System.Collections.Concurrent;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

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
    public static ConcurrentDictionary<Guid, ConnectionInfo> waitingPlayers = new ConcurrentDictionary<Guid, ConnectionInfo>();
    public static ConcurrentDictionary<string, Guid> players = new ConcurrentDictionary<string, Guid>();
    private Guid userId;
    private string displayName;
    
    public async Task Join()
    {
        GetUserInformation();
        if (waitingPlayers.ContainsKey(userId))
        {
            return;
        }
        waitingPlayers.TryAdd(userId, new ConnectionInfo { ConnectionId = Context.ConnectionId, DiplayName = displayName });
        await Clients.All.SendAsync("LobbyChanged", waitingPlayers.Count);
        await Task.Delay(2000);
        await Clients.Caller.SendAsync("Joined");

        var otherUsers = waitingPlayers.Where(f => f.Key != userId)
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

    public async Task AskForChallenge(Guid id)
    {
        GetUserInformation();
        if (waitingPlayers.TryGetValue(id, out var opponent))
        {
           await Clients.Client(opponent.ConnectionId).SendAsync("ChallengeRequested", userId, displayName);
        }
    }

    public override Task OnConnectedAsync()
    {
        return Clients.Caller.SendAsync("LobbyChanged", waitingPlayers.Count, CancellationToken.None);
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        GetUserInformation();
        if (waitingPlayers.TryGetValue(userId, out var connectionId))
        {
            waitingPlayers.Remove(userId, out connectionId);
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

        userId = Guid.Parse(idClaim.Value);
        displayName = Context.User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.Name).Value;
    }
}
