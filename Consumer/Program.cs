using Common.Messages;
using Consumer.DataService;
using Consumer.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Consumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            builder.Services.Configure<MessageQueueOptions>(configuration.GetSection(MessageQueueOptions.Section));
            // Add services to the container.
            builder.Services.AddMessageQueues(configuration);
            builder.Services.AddSingleton<SharedDb>();
            builder.Services.AddSignalR();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();


            app.MapPost("broadcast", async (string message, IHubContext<ChatHub, IChatClient> context) =>
            {
                await context.Clients.All.ReceiveMessage(message);

                return Results.NoContent();
            });

            app.MapPost("broadcastMessage1", async (MessageType1 message, IHubContext<ChatHub, IChatClient> context) =>
            {
                await context.Clients.All.ReceiveMessage1(message);

                return Results.NoContent();
            });

            app.MapPost("broadcastMessage2", async (MessageType2 message, IHubContext<ChatHub, IChatClient> context) =>
            {
                await context.Clients.All.ReceiveMessage2(message);

                return Results.NoContent();
            });

            app.UseHttpsRedirection();

            app.MapHub<ChatHub>("chat-hub");

            app.Run();
        }
    }
}
