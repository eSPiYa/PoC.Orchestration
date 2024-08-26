using Microsoft.AspNetCore.SignalR;
using PoC.Orchestration.Api.Services;
using PoC.Orchestration.Common.Models;
using PoC.Orchestration.Common.WorkFlows;
using System.Text.Json;

namespace PoC.Orchestration.Api.Hubs
{
    public class ShowsHub : Hub
    {
        private readonly IConfiguration configuration;
        private readonly WebApiService webApiService;
        private readonly string orchestratorUrl;

        public ShowsHub(IConfiguration configuration, 
                        WebApiService webApiService)
        {
            this.configuration = configuration;
            this.webApiService = webApiService;

            this.orchestratorUrl = configuration.GetValue<string>("orchestrator:url")!;
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

            var result = await this.webApiService.PostAsync($"{this.orchestratorUrl}/api/workflow", JsonSerializer.Serialize(payload), additionalHeaders: headers);
        }

        public async Task GetShowsLists()
        {
            var headers = new Dictionary<string, string>()
            {
                { "connectionId", this.Context.ConnectionId },
                { "workflowId", nameof(WorkFlowsEnum.GetShowsListsWorkFlow) }
            };

            var result = await this.webApiService.PostAsync($"{this.orchestratorUrl}/api/workflow", "{ }", additionalHeaders: headers);
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
