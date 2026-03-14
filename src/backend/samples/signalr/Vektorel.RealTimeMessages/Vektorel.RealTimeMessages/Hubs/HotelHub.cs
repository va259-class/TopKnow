using Microsoft.AspNetCore.SignalR;

namespace Vektorel.RealTimeMessages.Hubs;

public class HotelHub : Hub
{
    private static Dictionary<int, string> rooms = new Dictionary<int, string>();
    public Task SetMode(bool isDayLight)
    {
        if (isDayLight)
        {
            return Clients.All.SendAsync("ChangeModeToDayLight");
        }
        return Clients.All.SendAsync("ChangeModeToNight");
    }

    public Task CheckInRoom(int number, string nickname)
    {
        if (rooms.ContainsKey(number))
        {
            return Task.CompletedTask;
        }
        rooms.Add(number, nickname);
        return Clients.All.SendAsync("CheckIn", number);
    }

    public Task CheckOutRoom(int number, string nickname)
    {
        if (rooms.TryGetValue(number, out var guest))
        {
            if (guest == nickname)
            {
                rooms.Remove(number);
                return Clients.All.SendAsync("CheckOut", number);
            }
        }
        return Task.CompletedTask;
    }
}
