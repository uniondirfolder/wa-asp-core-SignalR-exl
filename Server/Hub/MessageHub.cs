﻿using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Hub
{
    public class MessageHub:Hub<IMessageClient>
    {
        public Task SendToOther(Message message) 
        {
            var msgForClient = NewMessage.Create(Context.Items["Name"] as string, message);
            return Clients.Others.Send(msgForClient);
        }

        public Task SetMyName(string name) 
        {
            if (string.IsNullOrWhiteSpace(name)) return Task.CompletedTask;

            if (Context.Items.ContainsKey("Name")) Context.Items["Name"] = name;
            else Context.Items.Add("Name", name);

            return Task.CompletedTask;
        }
        
        public Task<string> GetMyName() 
        {
            if (Context.Items.ContainsKey("Name")) return Task.FromResult(Context.Items["Name"] as string);

            return Task.FromResult("Anonymous");
        }
    }
}
