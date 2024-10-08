﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PoC.Orchestration.Api.Hubs;
using System.Text.Json;

namespace PoC.Orchestration.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsListsController : ControllerBase
    {
        private readonly ILogger<ShowsListsController> logger;
        private readonly IHubContext<ShowsHub> hubContext;

        public ShowsListsController(ILogger<ShowsListsController> logger,
                                    IHubContext<ShowsHub> hubContext)
        {
            this.logger = logger;
            this.hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> ReceiveShowsLists([FromBody] object body)
        {
            string connectionId = this.HttpContext.Request.Headers["connectionId"].FirstOrDefault()!,
                   actionName = this.HttpContext.Request.Headers["actionName"].FirstOrDefault()!;

            await this.hubContext.Clients.Client(connectionId).SendAsync(actionName, ((JsonElement)body).ToString());

            this.logger.LogInformation($"'ReceiveShowsLists' was invoked for ConnectionId '{connectionId}' and Action '{actionName}'");

            return Ok();
        }
    }
}
