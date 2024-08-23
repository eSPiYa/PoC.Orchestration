using PoC.Orchestration.Api.WorkFlows;
using PoC.Orchestration.Api.WorkFlows.Movies;
using PoC.Orchestration.Api.WorkFlows.Movies.DataModels;
using PoC.Orchestration.Api.WorkFlows.Steps;
using WorkflowCore.Interface;

namespace PoC.Orchestration.Api.Extensions
{
    public static class WorkFlowExtensions
    {
        public static void RegisterWorkFlowSteps(this IServiceCollection services)
        {
            services.AddTransient(typeof(SignalRCallAsync<>));
        }

        public static void RegisterWorkFlows(this IServiceProvider serviceProvider)
        {
            var host = serviceProvider.GetService<IWorkflowHost>();
            host!.RegisterWorkflow<HelloWorldWorkflow>();
            host!.RegisterWorkflow<GetMoviesWorkFlow, GetMoviesParams>();
            host.Start();
        }
    }
}
