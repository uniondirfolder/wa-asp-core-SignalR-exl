using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Hub
{
    [Authorize]
    public class MessageHub:Hub<IMessageClient>
    {
        private readonly ILogger<MessageHub> logger;

        public MessageHub(ILogger<MessageHub> logger)
        {
            this.logger = logger;
        }

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

            logger.LogInformation("Client with ConnectionId {ConnectionId} changed name to {Name}", Context.ConnectionId, name);

            return Task.CompletedTask;
        }
        [Authorize(Policy = "MyPolicy")]
        public Task<string> GetMyName() 
        {
            if (Context.Items.ContainsKey("Name")) return Task.FromResult(Context.Items["Name"] as string);

            return Task.FromResult("Anonymous");
        }

#pragma warning disable CS8424 // The EnumeratorCancellationAttribute will have no effect. The attribute is only effective on a parameter of type CancellationToken in an async-iterator method returning IAsyncEnumerable
        public async IAsyncEnumerator<int> DownloadStream([EnumeratorCancellation] CancellationToken cancellation)
#pragma warning restore CS8424 // The EnumeratorCancellationAttribute will have no effect. The attribute is only effective on a parameter of type CancellationToken in an async-iterator method returning IAsyncEnumerable
        {
            int iteration = 0;
            while (iteration<10 && !cancellation.IsCancellationRequested)
            {
                yield return iteration;

                iteration += 1;

                await Task.Delay(1000, cancellation);
            }
        }

        public async Task UploadStream(IAsyncEnumerable<int> asyncEnumerable)
        {
            await foreach (var element in asyncEnumerable)
            {
                Debug.WriteLine(element);
            }

            Debug.WriteLine("Stream from client completed");
        }
    }
}
