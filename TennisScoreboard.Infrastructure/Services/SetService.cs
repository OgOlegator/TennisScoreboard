using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisScoreboard.Application.Common.Abstractions;
using TennisScoreboard.Domain.Enums;

namespace TennisScoreboard.Infrastructure.Services
{
    public class SetService : AbstractMatchStage
    {
        /// <summary>
        /// Номер сета
        /// </summary>
        public readonly int Number;

        /// <summary>
        /// Гейм сета. Если счет 6-6 по геймам, то здесь храниться экземпляр класса TieBreakService
        /// </summary>
        public AbstractMatchStage Game { get; private set; } = new GameService();

        public SetService(int number)
        {
            Number = number;
        }

        public override void AddPointForPlayer(WinPlayer winPlayer)
        {
            if (IsFinished)
                throw new Exception("Сет окончен");

            Game.AddPointForPlayer(winPlayer);

            if (!Game.IsFinished)
                return;

            if (winPlayer == WinPlayer.First)
                ScorePlayer1 += 1;
            else if (winPlayer == WinPlayer.Second)
                ScorePlayer2 += 1;

            Game = new GameService();

            if (ScorePlayer1 == 6 && ScorePlayer2 == 6)
                Game = new TieBreakService();

            CheckFinished();
        }

        private void CheckFinished()
        {
            if (ScorePlayer1 == 7 || ScorePlayer2 == 7)
                IsFinished = true;

            if (ScorePlayer1 >= 6 && ScorePlayer1 - 2 >= ScorePlayer2)
                IsFinished = true;

            if (ScorePlayer2 >= 6 && ScorePlayer2 - 2 >= ScorePlayer1)
                IsFinished = true;
        }
    }
}
