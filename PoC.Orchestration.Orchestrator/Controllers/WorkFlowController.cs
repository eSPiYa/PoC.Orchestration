﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoC.Orchestration.Orchestrator.WorkFlows.Shows.DataModels;
using System.Text.Json;
using System.Text.Json.Nodes;
using WorkflowCore.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PoC.Orchestration.Orchestrator.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class WorkFlowController : ControllerBase
    {
        private readonly ILogger<WorkFlowController> logger;
        private readonly IWorkflowHost workflowHost;

        public WorkFlowController(ILogger<WorkFlowController> logger,
                                  IWorkflowHost workflowHost)
        {
            this.logger = logger;
            this.workflowHost = workflowHost;
        }

        [HttpPost]
        public async Task<IActionResult> Execute([FromBody] object body)
        {
            var data = body as GetMoviesSaga;
            string workflowId = this.HttpContext.Request.Headers["workflowId"].FirstOrDefault()!,
                   connectionId = this.HttpContext.Request.Headers["connectionId"].FirstOrDefault()!;

            await Task.Run(() =>
            {
                this.workflowHost.StartWorkflow(workflowId, 1, data, connectionId);
            });

            this.logger.LogInformation($"'Execute' of 'WorfLowController' was invoked for WorkFlow '{workflowId}' for ConnectionId '{connectionId}'");

            return Ok();
        }
    }
}
