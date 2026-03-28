using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace TopKnow.PlayGround.Api
{
    public class GameHub : Hub
    {
        public static ConcurrentDictionary<string, Guid> waitingPlayers = new ConcurrentDictionary<string, Guid>();
        public static ConcurrentDictionary<string, Guid> players = new ConcurrentDictionary<string, Guid>();

        public Task Join()
        {
            waitingPlayers.TryAdd(Context.ConnectionId, Guid.NewGuid());
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
