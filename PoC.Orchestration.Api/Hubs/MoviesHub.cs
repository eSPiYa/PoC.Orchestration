using Microsoft.AspNetCore.SignalR;
using PoC.Orchestration.Api.WorkFlows.Movies;
using PoC.Orchestration.Api.WorkFlows.Movies.DataModels;
using WorkflowCore.Interface;
using static PoC.Orchestration.Api.WorkFlows.Movies.DataModels.GetMoviesParams;

namespace PoC.Orchestration.Api.Hubs
{
    public class MoviesHub : Hub
    {
        private readonly IWorkflowHost workflowHost;

        public MoviesHub(IWorkflowHost workflowHost)
        {
            this.workflowHost = workflowHost;
        }

        public async Task GetMoviesList()
        {
            var data = new GetMoviesParams
            {
                FetchType = FetchTypesList.Popular,
                Page = 1
            };

            await Task.Run(() =>
            {   
                this.workflowHost.StartWorkflow(GetMoviesWorkFlow.ID, 1, data, this.Context.ConnectionId);
            });            
        }
    }
}
