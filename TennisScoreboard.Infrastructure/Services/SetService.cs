using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisScoreboard.Domain.Enums;

namespace TennisScoreboard.Infrastructure.Services
{
    public class SetService
    {
        public readonly int Number;

        private bool IsTieBreak = false;

        public bool IsFinished { get; private set; } = false;

        public int ScorePlayer1 { get; private set; } = 0;
        public int ScorePlayer2 { get; private set; } = 0;

        public GameService Game { get; private set; } = new GameService();

        public TieBreakService TieBreak { get; private set; }

        public SetService(int number)
        {
            Number = number;
        }

        public void AddPointForPlayer(WinPlayer winPlayer)
        {
            if (IsFinished)
                throw new Exception("Сет окончен");

            if (IsTieBreak)
                AddPointInTieBreak(winPlayer);
            else
                AddPoint(winPlayer);

            CheckFinished();
        }

        /// <summary>
        /// Добавление выигранного очка в обычной стадии розыгрыша сэта
        /// </summary>
        /// <param name="winPlayer"></param>
        public void AddPoint(WinPlayer winPlayer)
        {
            Game.AddPointForPlayer(winPlayer);

            if (!Game.IsFinished)
                return;

            AddPointToScoreGame(winPlayer);

            Game = new GameService();
        }

        /// <summary>
        /// Добавление выигранного очка если играется Тай-брейк
        /// </summary>
        /// <param name="winPlayer">Игрок выигравший очко</param>
        public void AddPointInTieBreak(WinPlayer winPlayer)
        {
            TieBreak.AddPointForPlayer(winPlayer);

            if (!TieBreak.IsFinished)
                return;

            AddPointToScoreGame(winPlayer);
        }

        /// <summary>
        /// Добавление очка выигранного гейма
        /// </summary>
        /// <param name="winPlayer">Игрок выигравший очко</param>
        private void AddPointToScoreGame(WinPlayer winPlayer)
        {
            if (winPlayer == WinPlayer.First)
                ScorePlayer1 += 1;
            else if (winPlayer == WinPlayer.Second)
                ScorePlayer2 += 1;
        }

        private void CheckFinished()
        {
            if (ScorePlayer1 == 7 || ScorePlayer2 == 7)
                IsFinished = true;

            if (ScorePlayer1 == 6 && ScorePlayer2 == 6)
            {
                IsTieBreak = true;
                TieBreak = new TieBreakService();
            }

            if (ScorePlayer1 >= 6 && ScorePlayer1 - 2 >= ScorePlayer2)
                IsFinished = true;

            if (ScorePlayer2 >= 6 && ScorePlayer2 - 2 >= ScorePlayer1)
                IsFinished = true;
        }
    }
}
