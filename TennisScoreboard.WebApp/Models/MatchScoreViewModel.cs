using TennisScoreboard.Domain.Entities;
using TennisScoreboard.Infrastructure.Services;

namespace TennisScoreboard.WebApp.Models
{
    public class MatchScoreViewModel
    {
        public int IdPointWinner { get; set; }

        public MatchService Match { get; set; } 

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }
    }
}
