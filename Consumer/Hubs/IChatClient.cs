namespace Consumer.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(string message);
    }
}
