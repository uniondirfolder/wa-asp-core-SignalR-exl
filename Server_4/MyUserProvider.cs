using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server_4
{
    public class MyUserProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            var httpContext = connection.GetHttpContext();

            if (httpContext.Request.Query.ContainsKey("name"))
                return httpContext.Request.Query["name"].First();

            return "Anonymous";
        }
    }
}
