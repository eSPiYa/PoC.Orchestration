using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.Orchestration.Common.Models
{
    public class GetMoviesModel
    {
        public string FetchType { get; set; } = FetchTypesList.NowPlaying;
        public int Page { get; set; } = 1;
        public string LanguageCode { get; set; } = "en-US";

        public static class FetchTypesList
        {
            public static readonly string NowPlaying = "now_playing";
            public static readonly string Popular = "popular";
            public static readonly string TopRated = "top_rated";
            public static readonly string Upcoming = "upcoming";
        }
    }
}
