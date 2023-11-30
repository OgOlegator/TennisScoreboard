using TennisScoreboard.Domain.Entities;

namespace TennisScoreboard.WebApp.Models
{
    public class MatchHistoryViewModel
    {
        public List<Match> Matches { get; set; }  

        public PageViewModel PageInfo { get; set; }
    }
}
