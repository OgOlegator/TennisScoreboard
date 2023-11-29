using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisScoreboard.Application.Common.Abstractions;
using TennisScoreboard.Application.Common.Interfaces;
using TennisScoreboard.Domain.Enums;

namespace TennisScoreboard.Infrastructure.Services
{
    public class TieBreakService : AbstractMatchStage
    {
        public override void AddPointForPlayer(WinPlayer winPlayer)
        {
            if (IsFinished)
                throw new Exception("Тай-брейк окончен");

            if (winPlayer == WinPlayer.First)
                ScorePlayer1++;
            else if (winPlayer == WinPlayer.Second)
                ScorePlayer2++;

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
