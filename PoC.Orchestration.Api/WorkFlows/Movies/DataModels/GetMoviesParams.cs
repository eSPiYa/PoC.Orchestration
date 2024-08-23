namespace PoC.Orchestration.Api.WorkFlows.Movies.DataModels
{
    public class GetMoviesParams
    {
        public string FetchType { get; set; } = FetchTypesList.NowPlaying;
        public int Page { get; set; } = 1;
        public string LanguageCode { get; set; } = "en-US";
        public string GetMoviesHttpResponse { get; set; }

        public GetMoviesParams() { }

        public static class FetchTypesList
        {
            public static readonly string NowPlaying = "now_playing";
            public static readonly string Popular = "popular";
            public static readonly string TopRated = "top_rated";
            public static readonly string Upcoming = "upcoming";
        }
    }
}
