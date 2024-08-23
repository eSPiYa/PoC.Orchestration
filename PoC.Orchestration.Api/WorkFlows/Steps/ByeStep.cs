using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace PoC.Orchestration.Api.WorkFlows.Steps
{
    public class ByeStep : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("Bye!");
            return ExecutionResult.Next();
        }
    }
}
