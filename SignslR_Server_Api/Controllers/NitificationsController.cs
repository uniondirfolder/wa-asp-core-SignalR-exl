using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SignalR_Common;
using SignslR_Server_Api.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignslR_Server_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NitificationsController : ControllerBase
    {

        private readonly ILogger<NitificationsController> _logger;

        public NitificationsController(ILogger<NitificationsController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Push([FromBody]Message message, [FromServices] IHubContext<NotificationHub, INotificationClient> hubContext) 
        {
            await hubContext.Clients.All.Send(message);

            return Ok();
        }
    }
}
