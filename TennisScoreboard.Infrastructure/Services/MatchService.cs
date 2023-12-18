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
    public class MatchService : AbstractMatchStage
    {
        public readonly int IdPlayer1;
        public readonly int IdPlayer2;

        private int CurrentSetNumber = 1;

        public List<SetService> Sets { get; private set; } = new List<SetService>();

        public int WinnerId { get; private set; }

        public MatchService(int idPlayer1, int idPlayer2)
        {
            IdPlayer1 = idPlayer1;
            IdPlayer2 = idPlayer2;

            Sets.Add(new SetService(1));
            Sets.Add(new SetService(2));
            Sets.Add(new SetService(3));
        }

        public override void AddPointForPlayer(WinPlayer winPlayer)
        {
            if (IsFinished)
                throw new Exception("Матч окончен");

            var set = GetCurrentSet();

            set.AddPointForPlayer(winPlayer);

            if (!set.IsFinished)
                return;

            if (winPlayer == WinPlayer.First)
                ScorePlayer1++;
            else
                ScorePlayer2++;

            CheckFinished();

            if(!IsFinished)
                CurrentSetNumber++;
        }

        private void CheckFinished()
        {
            if (ScorePlayer1 < 2 && ScorePlayer2 < 2)
                return;

            IsFinished = true;
            WinnerId = ScorePlayer1 == 2 ? IdPlayer1 : IdPlayer2;
        }

        public static string GetNewMatchId()
            => Guid.NewGuid().ToString("D");

        public SetService GetCurrentSet()
            => GetSetByNumber(CurrentSetNumber);

        public SetService GetSetByNumber(int number)
        {
            var set = Sets.FirstOrDefault(set => set.Number == number);

            if (set == null)
                throw new ArgumentException("Передан некорректный номер сета");

            return set;
        }

        public WinPlayer GetWinPlayerById(int playerId)
        {
            if (playerId == IdPlayer1)
                return WinPlayer.First;
            else if (playerId == IdPlayer2)
                return WinPlayer.Second;
            else
                throw new ArgumentException("Передан некорректный ID игрока");
        }
    }
}
