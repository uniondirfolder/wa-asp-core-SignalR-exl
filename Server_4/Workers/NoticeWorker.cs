
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Server;
using Server_4.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Server_4.Workers
{
    public class NoticeWorker : BackgroundService
    {
        private readonly IHubContext<MessageHub, IMessageClient> hubContext;
        private readonly Message messageForAnonymous;
        private readonly Message messageForSubscribers;
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var newMessageForAnonymous = NewMessage.Create("System", messageForAnonymous);
                await hubContext.Clients.User("Anonymous").Send(newMessageForAnonymous);

                var newMessageForSubscribers = NewMessage.Create("System", messageForSubscribers);
                await hubContext.Clients.Group("Subscribers").Send(newMessageForSubscribers);

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
