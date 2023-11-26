using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisScoreboard.Application.Common.Interfaces;

namespace TennisScoreboard.Infrastructure.Services
{
    public class MatchService
    {
        public readonly int IdPlayer1;
        public readonly int IdPlayer2;

        private int CurrentSetNumber = 1;

        public List<SetService> Sets { get; private set; } = new List<SetService>();

        public int WinnerId { get; private set; } = -1;

        public MatchService(int idPlayer1, int idPlayer2)
        {
            IdPlayer1 = idPlayer1;
            IdPlayer2 = idPlayer2;

            Sets.Add(new SetService(1));
            Sets.Add(new SetService(2));
            Sets.Add(new SetService(3));
        }

        public static string GetNewMatchId()
            => Guid.NewGuid().ToString("D");

        public void AddPointForPlayer(int playerId)
        {
            
        }
    }
}
