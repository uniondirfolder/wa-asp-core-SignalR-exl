using SignalR_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignslR_Server_Api.Hubs
{
    public interface INotificationClient
    {
        Task Send(Message message);
    }
}
