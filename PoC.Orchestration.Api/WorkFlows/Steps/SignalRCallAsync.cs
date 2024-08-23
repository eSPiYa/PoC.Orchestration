using Microsoft.AspNetCore.SignalR;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace PoC.Orchestration.Api.WorkFlows.Steps
{
    public class SignalRCallAsync<THub> : StepBodyAsync where THub : Hub
    {
        public required string Method { get; set; }
        public required string Message { get; set; }

        private readonly IHubContext<THub> hubContext;

        public SignalRCallAsync(IHubContext<THub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            await this.hubContext.Clients.Client(context.Workflow.Reference).SendAsync(this.Method, this.Message);
            return ExecutionResult.Next();
        }
    }
}
