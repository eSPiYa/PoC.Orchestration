using PoC.Orchestration.Api.WorkFlows;
using WorkflowCore.Interface;

namespace PoC.Orchestration.Api.Extensions
{
    public static class WorkFlowExtensions
    {
        public static void RegisterWorkFlows(this IServiceProvider serviceProvider)
        {
            var host = serviceProvider.GetService<IWorkflowHost>();
            host!.RegisterWorkflow<HelloWorldWorkflow>();
            host.Start();
        }
    }
}
