using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;


namespace Server_4.Hubs
{
    public class MessageHub : Hub<IMessageClient>
    {
        public Task SendToOthers(Message message)
        {
            var messageForClient = NewMessage.Create(Context.UserIdentifier, message);
            return Clients.Others.Send(messageForClient);
        }

        public Task Subscribe()
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, "Subscribers");
        }

        public Task Unsubscribe()
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, "Subscribers");
        }
    }
}
