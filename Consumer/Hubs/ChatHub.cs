using Common.Messages;
using Microsoft.AspNetCore.SignalR;

namespace Consumer.Hubs
{
    public sealed class ChatHub : Hub<IChatClient>
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.ReceiveMessage($"{Context.ConnectionId} has joined");
        }

        public async Task SendMessage1(MessageType1 message)
        {
            await Clients.All.ReceiveMessage1(message);
        }
        public async Task SendMessage2(MessageType2 message)
        {
            await Clients.All.ReceiveMessage2(message);
        }
    }
}
