using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace ConApp_Client
{
    class Program
    {
        static HubConnection hubConnection; 
        static async Task Main(string[] args)
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:17910/notification")
                .Build();

            hubConnection.On<string>("Send", message => Console.WriteLine($"Message from server: {message}"));

            await hubConnection.StartAsync();

            bool isExit = false;

            while (!isExit)
            {
                var message = Console.ReadLine();

                if (message != "exit")
                    await hubConnection.SendAsync("SendMessage", message);
                else
                    isExit = true;
            }

            Console.ReadLine();
        }
    }
}
