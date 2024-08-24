using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Net.Http;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace PoCPoC.Orchestration.Orchestrator.WorkFlows.Steps
{
    public class ApiCallAsync : StepBodyAsync
    {
        public required HttpMethod Method { get; set; }
        public required string Url { get; set; }
        public required string ApiKey { get; set; }
        public required string BearerToken { get; set; }
        public IDictionary<string, string>? AdditionalHeaders { get; set; }
        public HttpContent? HttpContent { get; set; }
        public string? ResponseContent { get; set; } = null;

        private readonly HttpClient httpClient = new HttpClient();

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            switch (this.Method)
            {
                case HttpMethod m when m == HttpMethod.Get:
                    await this.HttpGetAsync();
                    break;
                case HttpMethod m when m == HttpMethod.Post:
                    await this.HttpPostAsync();
                    break;
                default:
                    break;
            }

            return ExecutionResult.Next();
        }

        private async Task HttpGetAsync()
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, this.Url);
            httpRequest.Headers.Add("accept", "application/json");

            if (!string.IsNullOrWhiteSpace(this.BearerToken))
                httpRequest.Headers.Add("Authorization", $"Bearer {this.BearerToken}");

            if (this.HttpContent != null)
                httpRequest.Content = this.HttpContent;

            var response = await this.httpClient.SendAsync(httpRequest);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            this.ResponseContent = content;
        }

        private async Task HttpPostAsync()
        {
            HttpContent? httpContent = null;
            if (this.HttpContent != null)
                httpContent = this.HttpContent;

            if (this.AdditionalHeaders != null && httpContent != null)
                foreach (var header in this.AdditionalHeaders)
                    httpContent!.Headers.Add(header.Key, header.Value);

            var response = await this.httpClient.PostAsync(this.Url, httpContent);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            this.ResponseContent = content;
        }
    }
}
