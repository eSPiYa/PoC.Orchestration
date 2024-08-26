namespace PoC.Orchestration.Orchestrator.WorkFlows.Shows.DataModels
{
    public class GetShowsListsSaga
    {
        public string? ConnectionId { get; set; }

        #region movies-related responses
        public string? ResponseContentNowPlaying { get; set; }
        public string? ResponseContentPopular { get; set; }
        public string? ResponseContentTopRated { get; set; }
        public string? ResponseContentUpcoming { get; set; }
        #endregion

        #region tv-related response
        public string? ResponseContentTVAiringToday { get; set; }
        public string? ResponseContentTVOnTheAir { get; set; }
        public string? ResponseContentTVPopular { get; set; }
        public string? ResponseContentTVTopRated { get; set; }
        #endregion
    }
}
