namespace PoC.Orchestration.Orchestrator.WorkFlows.Shows.DataModels
{
    public class GetShowsListsSaga
    {
        public string? ConnectionId { get; set; }
        public string? ResponseContentNowPlaying { get; set; }
        public string? ResponseContentPopular { get; set; }
        public string? ResponseContentTopRated { get; set; }
        public string? ResponseContentUpcoming { get; set; }
    }
}
