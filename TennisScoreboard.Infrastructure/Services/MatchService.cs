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
        
        /// <summary>
        /// Счет в сете игрока 1
        /// </summary>
        public int ScorePlayer1 { get; private set; } = 0;

        /// <summary>
        /// Счет в сете игрока 2
        /// </summary>
        public int ScorePlayer2 { get; private set; } = 0;

        /// <summary>
        /// Счет по сетам игрока 1
        /// </summary>
        public int ScoreSetPlayer1 { get; private set; } = 0;

        /// <summary>
        /// Счет по сетам игрока 2
        /// </summary>
        public int ScoreSetPlayer2 { get; private set; } = 0;

        public int WinnerId { get; private set; } = -1;

        public MatchService(int idPlayer1, int idPlayer2)
        {
            IdPlayer1 = idPlayer1;
            IdPlayer2 = idPlayer2;
        }

        public static string GetNewMatchId()
            => Guid.NewGuid().ToString("D");

        public void PointForPlayer(int playerId)
        {
            if (playerId == IdPlayer1)
                ScorePlayer1 += 15;
            else if (playerId == IdPlayer2)
                ScorePlayer2 += 15;
            else
                throw new ArgumentException();
        }
    }
}
