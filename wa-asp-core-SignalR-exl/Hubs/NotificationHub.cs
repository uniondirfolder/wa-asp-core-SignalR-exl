using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wa_asp_core_SignalR_exl.Hubs
{
    public class NotificationHub:Hub
    {
        public Task SendMessage(string message) 
        {
            return Clients.Others.SendAsync("Send", message);
        }
    }
}
