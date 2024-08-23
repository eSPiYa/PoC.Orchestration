using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace PoC.Orchestration.Api.WorkFlows.Steps
{
    public class ApiCallAsync : StepBodyAsync
    {
        public required HttpMethod Method { get; set; }
        public required string Url { get; set; }
        public required string ApiKey { get; set; }
        public required string ApiReadAccessToken { get; set; }
        public HttpContent? HttpContent { get; set; }
        public string ResponseContent { get; set; }

        private readonly HttpClient httpClient = new HttpClient();

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            var httpRequest = new HttpRequestMessage(this.Method, this.Url);
            httpRequest.Headers.Add("accept", "application/json");
            httpRequest.Headers.Add("Authorization", $"Bearer {this.ApiReadAccessToken}");

            if (this.HttpContent != null)
                httpRequest.Content = this.HttpContent;
            
            var response = await this.httpClient.SendAsync(httpRequest);
            var content = await response.Content.ReadAsStringAsync();
            this.ResponseContent = content;

            return ExecutionResult.Next();
        }
    }
}
