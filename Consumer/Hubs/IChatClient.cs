using Common.Messages;

namespace Consumer.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(string message);
        Task ReceiveMessage1(MessageType1 message);
        Task ReceiveMessage2(MessageType2 message);
    }
}
