using PoC.Orchestration.Orchestrator.WorkFlows.Shows;
using PoC.Orchestration.Orchestrator.WorkFlows.Shows.DataModels;
using WorkflowCore.Interface;

namespace PoC.Orchestration.Orchestrator.Extensions
{
    public static class WorkFlowExtensions
    {
        public static void RegisterWorkFlows(this IServiceProvider serviceProvider)
        {
            var host = serviceProvider.GetService<IWorkflowHost>();
            host!.RegisterWorkflow<GetMoviesWorkFlow, GetMoviesSaga>();
            host!.RegisterWorkflow<GetShowsListsWorkFlow, GetShowsListsSaga>();
            host.Start();
        }
    }
}
