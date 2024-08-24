using PoC.Orchestration.Common.Models;

namespace PoC.Orchestration.Orchestrator.WorkFlows.Shows.DataModels
{
    public class GetMoviesSaga: GetMoviesModel
    {
        public string? ConnectionId { get; set; }
        public string? GetMoviesHttpResponse { get; set; }
    }
}
