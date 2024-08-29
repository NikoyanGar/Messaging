using Common.Messages;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

namespace HubClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("wss://localhost:7172/chat-hub") // Replace with your hub URL
                .Build();

            connection.On<string>("ReceiveMessage", message =>
            {
                Console.WriteLine(message);
            });
            connection.On<MessageType1>("ReceiveMessage1", message =>
            {
                string messageJson = JsonConvert.SerializeObject(message);
                Console.WriteLine(messageJson);
            });
            connection.On<MessageType2>("ReceiveMessage2", message =>
            {
                string messageJson = JsonConvert.SerializeObject(message);
                Console.WriteLine(messageJson);
            });
            try
            {
                await connection.StartAsync();
                Console.WriteLine("Connection started");

                while (true)
                {
                    var message = Console.ReadLine();
                    await connection.InvokeAsync("SendMessage", message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
