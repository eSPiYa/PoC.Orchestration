﻿using Microsoft.AspNetCore.SignalR;
using PoC.Orchestration.Api.Services;
using PoC.Orchestration.Common.Models;
using PoC.Orchestration.Common.WorkFlows;
using System.Text.Json;

namespace PoC.Orchestration.Api.Hubs
{
    public class MoviesHub : Hub
    {
        private readonly WebApiService webApiService;

        public MoviesHub(WebApiService webApiService)
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
    }
}
