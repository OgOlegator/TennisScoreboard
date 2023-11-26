using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisScoreboard.Domain.Enums;

namespace TennisScoreboard.Infrastructure.Services
{
    public class GameService
    {
        /// <summary>
        /// В гейме этап больше/меньше?
        /// </summary>
        private bool IsMoreLess = false;

        public bool IsFinished { get; private set; } = false;

        public int ScorePlayer1 { get; private set; } = 0;
        public int ScorePlayer2 { get; private set; } = 0;

        public void AddPointForPlayer(WinPlayer winPlayer)
        {
            if (IsFinished)
                throw new Exception("Гейм окончен");

            if(IsMoreLess)
                AddPointInMoreLessStage(winPlayer);
            else
                AddPoint(winPlayer);

            CheckFinished();
        }

        /// <summary>
        /// Добавить выигранное очко
        /// </summary>
        /// <param name="winPlayer"></param>
        /// <exception cref="ArgumentException"></exception>
        private void AddPoint(WinPlayer winPlayer)
        {
            if (winPlayer == WinPlayer.First)
                ScorePlayer1 += GetPoint(ScorePlayer1);
            else if (winPlayer == WinPlayer.Second)
                ScorePlayer2 += GetPoint(ScorePlayer2);

            int GetPoint(int pointsPlayer)
            {
                if (pointsPlayer <= 15)
                    return 15;
                else if (pointsPlayer == 30)
                    return 10;
                else if (pointsPlayer == 40)
                    return 100;
                else
                    throw new ArgumentException("Некорректный счет у игроков");
            }
        }

        /// <summary>
        /// Добавить выигранное очко при больше/меньше
        /// </summary>
        /// <param name="winPlayer"></param>
        private void AddPointInMoreLessStage(WinPlayer winPlayer)
        {
            if (winPlayer == WinPlayer.First)
            {
                if (ScorePlayer2 == 1)
                    ScorePlayer2 -= 1;
                else
                    ScorePlayer1 += 1;
            }
            else if (winPlayer == WinPlayer.Second)
            {
                if (ScorePlayer1 == 1)
                    ScorePlayer1 -= 1;
                else
                    ScorePlayer2 += 1;
            }
        }

        private void CheckFinished()
        {
            if(IsMoreLess)
            {
                if (ScorePlayer1 == 2 || ScorePlayer2 == 2)
                    IsFinished = true;
            }
            else
            {
                if (ScorePlayer1 == 40 && ScorePlayer2 == 40 && ScorePlayer1 == ScorePlayer2)
                {
                    IsMoreLess = true;
                    ScorePlayer1 = 0;
                    ScorePlayer2 = 0;
                    return;
                }

                if (ScorePlayer1 > 40 || ScorePlayer2 > 40)
                    IsFinished = true;
            }
        }
    }
}
