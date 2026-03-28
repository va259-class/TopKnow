using System.Collections.Concurrent;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace TopKnow.PlayGround.Api
{
    [Authorize]
    public class GameHub : Hub
    {
        public static ConcurrentDictionary<string, Guid> waitingPlayers = new ConcurrentDictionary<string, Guid>();
        public static ConcurrentDictionary<string, Guid> players = new ConcurrentDictionary<string, Guid>();

        public Task Join()
        {
            var id = Context.User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.NameIdentifier);
            waitingPlayers.TryAdd(Context.ConnectionId, Guid.Parse(id.Value));
            return Clients.All.SendAsync("LobbyChanged", waitingPlayers.Count, CancellationToken.None);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (waitingPlayers.TryGetValue(Context.ConnectionId, out var id))
            {
                waitingPlayers.Remove(Context.ConnectionId, out id);
                await Clients.All.SendAsync("LobbyChanged", waitingPlayers.Count, CancellationToken.None);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
