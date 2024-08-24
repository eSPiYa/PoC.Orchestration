using Microsoft.AspNetCore.SignalR;
using PoC.Orchestration.Api.Services;
using PoC.Orchestration.Common.Models;
using PoC.Orchestration.Common.WorkFlows;
using System.Text.Json;

namespace PoC.Orchestration.Api.Hubs
{
    public class ShowsHub : Hub
    {
        private readonly WebApiService webApiService;

        public ShowsHub(WebApiService webApiService)
        {
            this.webApiService = webApiService;
        }

        public async Task GetMoviesList()
        {
            var payload = new GetMoviesModel
            {
                FetchType = GetMoviesModel.FetchTypesList.Popular,
                Page = 1
            };

            var headers = new Dictionary<string, string>()
            {
                { "connectionId", this.Context.ConnectionId },
                { "workflowId", nameof(WorkFlowsEnum.GetMoviesWorkFlow) }
            };

            var result = await this.webApiService.PostAsync("http://poc.orchestration.orchestrator:8080/api/workflow", JsonSerializer.Serialize(payload), additionalHeaders: headers);
        }

        public async Task GetShowsLists()
        {
            var headers = new Dictionary<string, string>()
            {
                { "connectionId", this.Context.ConnectionId },
                { "workflowId", nameof(WorkFlowsEnum.GetShowsListsWorkFlow) }
            };

            var result = await this.webApiService.PostAsync("http://poc.orchestration.orchestrator:8080/api/workflow", "{ }", additionalHeaders: headers);
        }

        #region Hub

        public override async Task OnConnectedAsync()
        {
            await Clients.Client(this.Context.ConnectionId).SendAsync("receiveServerName", Environment.MachineName);
            await base.OnConnectedAsync();
        }

        #endregion
    }
}
