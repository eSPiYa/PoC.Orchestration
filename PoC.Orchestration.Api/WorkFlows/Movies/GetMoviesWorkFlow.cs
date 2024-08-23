using PoC.Orchestration.Api.Hubs;
using PoC.Orchestration.Api.WorkFlows.Movies.DataModels;
using PoC.Orchestration.Api.WorkFlows.Steps;
using WorkflowCore.Interface;

namespace PoC.Orchestration.Api.WorkFlows.Movies
{
    public class GetMoviesWorkFlow : IWorkflow<GetMoviesParams>
    {
        public readonly static string ID = "Movies";

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
        public string Id => ID;
        public int Version => 1;

        public void Build(IWorkflowBuilder<GetMoviesParams> builder)
        {
            builder
                .StartWith<ApiCallAsync>()
                    .Input(step => step.Method, data => HttpMethod.Get)
                    .Input(step => step.Url, data => $"https://api.themoviedb.org/3/movie/{data.FetchType}?language={data.LanguageCode}&page={data.Page}")
                    .Input(step => step.ApiKey, data => this.ApiKey)
                    .Input(step => step.ApiReadAccessToken, data => this.ApiReadAccessToken)
                    .Output(data => data.GetMoviesHttpResponse, step => step.ResponseContent)
                .Then<SignalRCallAsync<MoviesHub>>()
                    .Input(step => step.Method, data => "ReceiveMessage")
                    .Input(step => step.Message, data => data.GetMoviesHttpResponse)
                .EndWorkflow();
        }
        #endregion
    }
}
