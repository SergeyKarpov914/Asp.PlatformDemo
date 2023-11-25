using Clio.Demo.Util.Telemetry.NLog;
using Microsoft.AspNetCore.SignalR;

namespace Clio.Demo.OrderCrudServer
{
    public class OrderCrudHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            Log.Info(this, $"Client '{Context.ConnectionId}' connected");
            await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} connected");
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Log.Info(this, $"Client '{Context.ConnectionId}' disconnected");
            await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} disconnected");
        }

        public async Task NewMessage(string user, string message)
        {
            Log.Info(this, $"Client '{Context.ConnectionId}:{user}' sent message {message}");
            await Clients.All.SendAsync("messageReceived", user, $"{message} Who do you think you are?");
        }
    }
}
