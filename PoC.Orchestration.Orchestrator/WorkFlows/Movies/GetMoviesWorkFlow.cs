using PoC.Orchestration.Common.WorkFlows;
using PoC.Orchestration.Orchestrator.WorkFlows.Movies.DataModels;
using PoC.Orchestration.Orchestrator.WorkFlows.Steps;
using PoCPoC.Orchestration.Orchestrator.WorkFlows.Steps;
using System.Net.Http;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace PoC.Orchestration.Orchestrator.WorkFlows.Movies
{
    public class GetMoviesWorkFlow : IWorkflow<GetMoviesSaga>
    {
        private readonly IConfiguration configuration;
        private readonly string ApiKey;
        private readonly string ApiReadAccessToken;

        public GetMoviesWorkFlow(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.ApiKey = configuration.GetValue<string>("tmdb:api:key") ?? string.Empty;
            this.ApiReadAccessToken = configuration.GetValue<string>("tmdb:api:ReadAccessToken") ?? string.Empty;
        }

        #region IWorkflow
        public string Id => nameof(WorkFlowsEnum.GetMoviesWorkFlow);
        public int Version => 1;

        public void Build(IWorkflowBuilder<GetMoviesSaga> builder)
        {
            builder
                .StartWith<GetConnectionId>()
                    .Output(data => data.ConnectionId, step => step.ConnectionId)
                .Then<ApiCallAsync>()
                    .Input(step => step.Method, data => HttpMethod.Get)
                    .Input(step => step.Url, data => $"https://api.themoviedb.org/3/movie/{data.FetchType}?language={data.LanguageCode}&page={data.Page}")
                    .Input(step => step.ApiKey, data => this.ApiKey)
                    .Input(step => step.BearerToken, data => this.ApiReadAccessToken)
                    .Output(data => data.GetMoviesHttpResponse, step => step.ResponseContent)
                .Then<ApiCallAsync>()
                    .Input(step => step.Method, data => HttpMethod.Post)
                    .Input(step => step.Url, data => $"http://poc.orchestration.api:8080/api/movieslist")
                    .Input(step => step.AdditionalHeaders, data => new Dictionary<string, string>
                    {
                        { "connectionId", data.ConnectionId! }
                    })
                    .Input(step => step.HttpContent, data => new StringContent(data.GetMoviesHttpResponse!, Encoding.UTF8, "application/json"))
                .EndWorkflow();
        }
        #endregion
    }
}
