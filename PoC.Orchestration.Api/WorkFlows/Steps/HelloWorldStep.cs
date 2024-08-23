using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace PoC.Orchestration.Api.WorkFlows.Steps
{
    public class HelloWorldStep : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("Hello world");
            return ExecutionResult.Next();
        }
    }
}
