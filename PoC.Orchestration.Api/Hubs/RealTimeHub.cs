using Microsoft.AspNetCore.SignalR;
using PoC.Orchestration.Api.WorkFlows;
using WorkflowCore.Interface;

namespace PoC.Orchestration.Api.Hubs
{
    public class RealTimeHub: Hub
    {
        private readonly IWorkflowHost workflowHost;

        public RealTimeHub(IWorkflowHost workflowHost)
        {
            this.workflowHost = workflowHost;
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);

            this.workflowHost.StartWorkflow(HelloWorldWorkflow.ID, 1, null);
        }
    }
}
