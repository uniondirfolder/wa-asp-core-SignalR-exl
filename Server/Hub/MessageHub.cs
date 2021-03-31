using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Hub
{
    [Authorize]
    public class MessageHub:Hub<IMessageClient>
    {
        [Authorize(Policy="MyPolicy")]
        public Task SendToOthers(Message message) 
        {
            var msgForClient = NewMessage.Create(Context.Items["Name"] as string, message);
            return Clients.Others.Send(msgForClient);
        }

        [Authorize(Policy = "MyPolicy")]
        public Task SetMyName(string name) 
        {
            if (string.IsNullOrWhiteSpace(name)) return Task.CompletedTask;

            if (Context.Items.ContainsKey("Name")) Context.Items["Name"] = name;
            else Context.Items.Add("Name", name);

            return Task.CompletedTask;
        }
        [Authorize(Policy = "MyPolicy")]
        public Task<string> GetMyName() 
        {
            if (Context.Items.ContainsKey("Name")) return Task.FromResult(Context.Items["Name"] as string);

            return Task.FromResult("Anonymous");
        }
    }
}
