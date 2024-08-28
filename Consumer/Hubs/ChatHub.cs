using Microsoft.AspNetCore.SignalR;

namespace Consumer.Hubs
{
    public class ChatHub : Hub
    {
        public async Task JoinSpecificChat(UserConnection connection)
        {
            await Clients.All.SendAsync("ReceiveMessage", connection.UserName, $"{connection.UserName} connected to chat");
        }

        public async Task JoinSpecificChatRoom(UserConnection connection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, connection.ChatRoom);
            await Clients.Group(connection.ChatRoom).SendAsync("ReceiveMessage", "System", $"{connection.UserName} has joined the chat");
        }
    }
}
