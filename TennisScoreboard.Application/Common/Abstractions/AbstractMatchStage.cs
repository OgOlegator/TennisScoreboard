using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisScoreboard.Domain.Enums;

namespace TennisScoreboard.Application.Common.Abstractions
{
    /// <summary>
    /// Абстрактный этап матча в теннис
    /// </summary>
    public abstract class AbstractMatchStage
    {
        /// <summary>
        /// Этап матча окончен?
        /// </summary>
        public bool IsFinished { get; protected set; } = false;

        /// <summary>
        /// Счет игрока 1
        /// </summary>
        public int ScorePlayer1 { get; protected set; } = 0;

        /// <summary>
        /// Счет игрока 2
        /// </summary>
        public int ScorePlayer2 { get; protected set; } = 0;

        /// <summary>
        /// Добавить выигранное очко игроку
        /// </summary>
        /// <param name="winPlayer"></param>
        public abstract void AddPointForPlayer(WinPlayer winPlayer);
    }
}
