using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PoC.Orchestration.Api.Hubs;
using System.Text.Json;

namespace PoC.Orchestration.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesListController : ControllerBase
    {
        private readonly IHubContext<ShowsHub> hubContext;

        public MoviesListController(IHubContext<ShowsHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> ReceiveMoviesList([FromBody] object body)
        {
            string connectionId = this.HttpContext.Request.Headers["connectionId"].FirstOrDefault()!;

            await this.hubContext.Clients.Client(connectionId).SendAsync("receiveMoviesList", ((JsonElement)body).ToString());

            return Ok();
        }
    }
}
