using PoC.Orchestration.Common.WorkFlows;
using PoC.Orchestration.Orchestrator.WorkFlows.Shows.DataModels;
using PoC.Orchestration.Orchestrator.WorkFlows.Steps;
using PoCPoC.Orchestration.Orchestrator.WorkFlows.Steps;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace PoC.Orchestration.Orchestrator.WorkFlows.Shows
{
    public class GetShowsListsWorkFlow : IWorkflow<GetShowsListsSaga>
    {
        private readonly IConfiguration configuration;
        private readonly string ApiKey;
        private readonly string ApiReadAccessToken;
        private readonly string signalRServerUrl;

        public GetShowsListsWorkFlow(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.ApiKey = configuration.GetValue<string>("tmdb:api:key") ?? string.Empty;
            this.ApiReadAccessToken = configuration.GetValue<string>("tmdb:api:ReadAccessToken") ?? string.Empty;
            this.signalRServerUrl = configuration.GetValue<string>("signalr:server:url")!;
        }

        #region IWorkflow
        public string Id => nameof(WorkFlowsEnum.GetShowsListsWorkFlow);
        public int Version => 1;

        public void Build(IWorkflowBuilder<GetShowsListsSaga> builder)
        {
            builder
                .StartWith<GetConnectionId>()
                    .Output(data => data.ConnectionId, step => step.ConnectionId)
                .Parallel()
                    .Do(then => 
                        then.StartWith<ApiCallAsync>()
                                .Input(step => step.Method, data => HttpMethod.Get)
                                .Input(step => step.Url, data => $"https://api.themoviedb.org/3/movie/now_playing?language=en-US&page={1}")
                                .Input(step => step.ApiKey, data => this.ApiKey)
                                .Input(step => step.BearerToken, data => this.ApiReadAccessToken)
                                .Output(data => data.ResponseContentNowPlaying, step => step.ResponseContent)
                            .Then<ApiCallAsync>()
                                .Input(step => step.Method, data => HttpMethod.Post)
                                .Input(step => step.Url, data => $"{this.signalRServerUrl}/api/showslists")
                                .Input(step => step.AdditionalHeaders, data => new Dictionary<string, string>
                                {
                                    { "connectionId", data.ConnectionId! },
                                    { "actionName", "receiveMoviesListNowPlaying"}
                                })
                                .Input(step => step.HttpContent, data => new StringContent(data.ResponseContentNowPlaying!, Encoding.UTF8, "application/json"))
                    )
                    .Do(then =>
                        then.StartWith<ApiCallAsync>()
                                .Input(step => step.Method, data => HttpMethod.Get)
                                .Input(step => step.Url, data => $"https://api.themoviedb.org/3/movie/popular?language=en-US&page={1}")
                                .Input(step => step.ApiKey, data => this.ApiKey)
                                .Input(step => step.BearerToken, data => this.ApiReadAccessToken)
                                .Output(data => data.ResponseContentPopular, step => step.ResponseContent)
                            .Then<ApiCallAsync>()
                                .Input(step => step.Method, data => HttpMethod.Post)
                                .Input(step => step.Url, data => $"{this.signalRServerUrl}/api/showslists")
                                .Input(step => step.AdditionalHeaders, data => new Dictionary<string, string>
                                {
                                    { "connectionId", data.ConnectionId! },
                                    { "actionName", "receiveMoviesListPopular"}
                                })
                                .Input(step => step.HttpContent, data => new StringContent(data.ResponseContentPopular!, Encoding.UTF8, "application/json"))
                    )
                    .Do(then =>
                        then.StartWith<ApiCallAsync>()
                                .Input(step => step.Method, data => HttpMethod.Get)
                                .Input(step => step.Url, data => $"https://api.themoviedb.org/3/movie/top_rated?language=en-US&page={1}")
                                .Input(step => step.ApiKey, data => this.ApiKey)
                                .Input(step => step.BearerToken, data => this.ApiReadAccessToken)
                                .Output(data => data.ResponseContentTopRated, step => step.ResponseContent)
                            .Then<ApiCallAsync>()
                                .Input(step => step.Method, data => HttpMethod.Post)
                                .Input(step => step.Url, data => $"{this.signalRServerUrl}/api/showslists")
                                .Input(step => step.AdditionalHeaders, data => new Dictionary<string, string>
                                {
                                    { "connectionId", data.ConnectionId! },
                                    { "actionName", "receiveMoviesListTopRated"}
                                })
                                .Input(step => step.HttpContent, data => new StringContent(data.ResponseContentTopRated!, Encoding.UTF8, "application/json"))
                    )
                    .Do(then =>
                        then.StartWith<ApiCallAsync>()
                                .Input(step => step.Method, data => HttpMethod.Get)
                                .Input(step => step.Url, data => $"https://api.themoviedb.org/3/movie/upcoming?language=en-US&page={1}")
                                .Input(step => step.ApiKey, data => this.ApiKey)
                                .Input(step => step.BearerToken, data => this.ApiReadAccessToken)
                                .Output(data => data.ResponseContentUpcoming, step => step.ResponseContent)
                            .Then<ApiCallAsync>()
                                .Input(step => step.Method, data => HttpMethod.Post)
                                .Input(step => step.Url, data => $"{this.signalRServerUrl}/api/showslists")
                                .Input(step => step.AdditionalHeaders, data => new Dictionary<string, string>
                                {
                                    { "connectionId", data.ConnectionId! },
                                    { "actionName", "receiveMoviesListUpcoming"}
                                })
                                .Input(step => step.HttpContent, data => new StringContent(data.ResponseContentUpcoming!, Encoding.UTF8, "application/json"))
                    )
                .Join()
                .Then(then => {
                    Thread.Sleep(5000);
                    return ExecutionResult.Next();
                })
                .Parallel()
                    .Do(then =>
                        then.StartWith<ApiCallAsync>()
                                .Input(step => step.Method, data => HttpMethod.Get)
                                .Input(step => step.Url, data => $"https://api.themoviedb.org/3/tv/airing_today?language=en-US&page={1}")
                                .Input(step => step.ApiKey, data => this.ApiKey)
                                .Input(step => step.BearerToken, data => this.ApiReadAccessToken)
                                .Output(data => data.ResponseContentTVAiringToday, step => step.ResponseContent)
                            .Then<ApiCallAsync>()
                                .Input(step => step.Method, data => HttpMethod.Post)
                                .Input(step => step.Url, data => $"{this.signalRServerUrl}/api/showslists")
                                .Input(step => step.AdditionalHeaders, data => new Dictionary<string, string>
                                {
                                    { "connectionId", data.ConnectionId! },
                                    { "actionName", "receiveTVListAiringToday"}
                                })
                                .Input(step => step.HttpContent, data => new StringContent(data.ResponseContentTVAiringToday!, Encoding.UTF8, "application/json"))
                    )
                    .Do(then =>
                        then.StartWith<ApiCallAsync>()
                                .Input(step => step.Method, data => HttpMethod.Get)
                                .Input(step => step.Url, data => $"https://api.themoviedb.org/3/tv/on_the_air?language=en-US&page={1}")
                                .Input(step => step.ApiKey, data => this.ApiKey)
                                .Input(step => step.BearerToken, data => this.ApiReadAccessToken)
                                .Output(data => data.ResponseContentTVOnTheAir, step => step.ResponseContent)
                            .Then<ApiCallAsync>()
                                .Input(step => step.Method, data => HttpMethod.Post)
                                .Input(step => step.Url, data => $"{this.signalRServerUrl}/api/showslists")
                                .Input(step => step.AdditionalHeaders, data => new Dictionary<string, string>
                                {
                                    { "connectionId", data.ConnectionId! },
                                    { "actionName", "receiveTVListOnTheAir"}
                                })
                                .Input(step => step.HttpContent, data => new StringContent(data.ResponseContentTVOnTheAir!, Encoding.UTF8, "application/json"))
                    )
                    .Do(then =>
                        then.StartWith<ApiCallAsync>()
                                .Input(step => step.Method, data => HttpMethod.Get)
                                .Input(step => step.Url, data => $"https://api.themoviedb.org/3/tv/popular?language=en-US&page={1}")
                                .Input(step => step.ApiKey, data => this.ApiKey)
                                .Input(step => step.BearerToken, data => this.ApiReadAccessToken)
                                .Output(data => data.ResponseContentTVPopular, step => step.ResponseContent)
                            .Then<ApiCallAsync>()
                                .Input(step => step.Method, data => HttpMethod.Post)
                                .Input(step => step.Url, data => $"{this.signalRServerUrl}/api/showslists")
                                .Input(step => step.AdditionalHeaders, data => new Dictionary<string, string>
                                {
                                    { "connectionId", data.ConnectionId! },
                                    { "actionName", "receiveTVListPopular"}
                                })
                                .Input(step => step.HttpContent, data => new StringContent(data.ResponseContentTVPopular!, Encoding.UTF8, "application/json"))
                    )
                    .Do(then =>
                        then.StartWith<ApiCallAsync>()
                                .Input(step => step.Method, data => HttpMethod.Get)
                                .Input(step => step.Url, data => $"https://api.themoviedb.org/3/tv/top_rated?language=en-US&page={1}")
                                .Input(step => step.ApiKey, data => this.ApiKey)
                                .Input(step => step.BearerToken, data => this.ApiReadAccessToken)
                                .Output(data => data.ResponseContentTVTopRated, step => step.ResponseContent)
                            .Then<ApiCallAsync>()
                                .Input(step => step.Method, data => HttpMethod.Post)
                                .Input(step => step.Url, data => $"{this.signalRServerUrl}/api/showslists")
                                .Input(step => step.AdditionalHeaders, data => new Dictionary<string, string>
                                {
                                    { "connectionId", data.ConnectionId! },
                                    { "actionName", "receiveTVListTopRated"}
                                })
                                .Input(step => step.HttpContent, data => new StringContent(data.ResponseContentTVTopRated!, Encoding.UTF8, "application/json"))
                    )                    
                .Join()
                .EndWorkflow();
        }
        #endregion
    }
}
