using Microsoft.AspNetCore.SignalR;
using SignalR_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SignslR_Server_Api.Hubs
{
    public class NotificationHub:Hub<INotificationClient>
    {
        public Task SendMessage(Message message) 
        {
            Debug.WriteLine(Context.ConnectionId);

            if (Context.Items.ContainsKey("user_name"))
                message.Title = $"Message from user: {Context.Items["user_name"]}";

            //return Clients.Others.SendAsync("Send", message);
            return Clients.Others.Send(message);
        }

        public Task SetName(string name) 
        {
            Context.Items.TryAdd("user_name", name);

            return Task.CompletedTask;
        }

        public override Task OnConnectedAsync()
        {
            Message m = new()
            {
                Title = $"new client connected {Context.ConnectionId}",
                Body = string.Empty
            };

            Clients.Others.Send(m);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Message m = new()
            {
                Title = $"new client disconnected {Context.ConnectionId}",
                Body = string.Empty
            };

            return base.OnDisconnectedAsync(exception);
        }

        protected override void Dispose(bool disposing)
        {
            Debug.WriteLine("Hub disposed");
            base.Dispose(disposing);
        }

    }
}
