using PoC.Orchestration.Api.WorkFlows.Steps;
using WorkflowCore.Interface;
using WorkflowCore.Services;

namespace PoC.Orchestration.Api.WorkFlows
{
    public class HelloWorldWorkflow : IWorkflow
    {
        public readonly static string ID = "HelloWorld";

        public string Id => ID;
        public int Version => 1;

        public void Build(IWorkflowBuilder<object> builder)
        {
            builder
                .StartWith<HelloWorldStep>()
                .Then<ByeStep>();
        }
    }
}
