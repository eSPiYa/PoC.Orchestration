using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace PoC.Orchestration.Orchestrator.WorkFlows.Steps
{
    public class GetConnectionId : StepBody
    {
        public string? ConnectionId { get; set; } = null;

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            this.ConnectionId = context.Workflow.Reference;

            return ExecutionResult.Next();
        }
    }
}
