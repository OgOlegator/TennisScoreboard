using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisScoreboard.Domain.Enums;

namespace TennisScoreboard.Infrastructure.Services
{
    public class TieBreakService
    {
        public bool IsFinished { get; private set; } = false;

        public int ScorePlayer1 { get; private set; } = 0;
        public int ScorePlayer2 { get; private set; } = 0;

        public void AddPointForPlayer(WinPlayer winPlayer)
        {
            if (IsFinished)
                throw new Exception("Тай-брейк окончен");

            if (winPlayer == WinPlayer.First)
                ScorePlayer1 += 1;
            else if (winPlayer == WinPlayer.Second)
                ScorePlayer2 += 1;

            CheckFinished();    
        }

        private void CheckFinished()
        {
            if (ScorePlayer1 >= 7 && ScorePlayer1 - 2 >= ScorePlayer2)
                IsFinished = true;

            if (ScorePlayer2 >= 7 && ScorePlayer2 - 2 >= ScorePlayer1)
                IsFinished = true;
        }
    }
}
