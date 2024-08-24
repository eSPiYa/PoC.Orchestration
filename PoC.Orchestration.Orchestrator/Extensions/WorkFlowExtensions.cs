using PoC.Orchestration.Orchestrator.WorkFlows.Movies;
using PoC.Orchestration.Orchestrator.WorkFlows.Movies.DataModels;
using WorkflowCore.Interface;

namespace PoC.Orchestration.Orchestrator.Extensions
{
    public static class WorkFlowExtensions
    {
        public static void RegisterWorkFlows(this IServiceProvider serviceProvider)
        {
            var host = serviceProvider.GetService<IWorkflowHost>();
            host!.RegisterWorkflow<GetMoviesWorkFlow, GetMoviesSaga>();
            host.Start();
        }
    }
}
