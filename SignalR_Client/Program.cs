
using Microsoft.AspNetCore.SignalR.Client;
using SignalR_Common;
using System;
using System.Threading.Tasks;

namespace SignalR_Client
{
    class Program
    {
        static HubConnection hubConnection;
        static async Task Main(string[] args)
        {
            await InitSignalRConnection();
            bool isExit = true;
            while (isExit)
            {
                Console.WriteLine("Enter your message or command: ");

                var userInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userInput))
                    continue;

                if(userInput == "exit") 
                {
                    isExit = false;
                }
                else if(userInput == "setname") 
                {
                    Console.WriteLine("Enter your name:");

                    var name = Console.ReadLine();

                    if (string.IsNullOrEmpty(name))
                        continue;

                    await hubConnection.SendAsync("SetName", name);
                    Console.WriteLine("Name saved");
                }
                else
                {
                    var message = new Message { Title = "simple message", Body = userInput };

                    await hubConnection.SendAsync("SendMessage", message);
                    Console.WriteLine("Message sent");
                }
                Console.ReadLine();
            }
            

            
        }

        private static Task InitSignalRConnection() 
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:32957/notification")
                .Build();

            hubConnection.On<Message>("Send", message =>
            {
                Console.WriteLine("New message from server");
                Console.WriteLine($"Titile: {message.Title}");
                Console.WriteLine($"Body: {message.Body}");
            });

            return hubConnection.StartAsync();
        }
    }
}
