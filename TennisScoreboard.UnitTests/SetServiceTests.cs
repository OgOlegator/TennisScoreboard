using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisScoreboard.Domain.Enums;
using TennisScoreboard.Infrastructure.Services;

namespace TennisScoreboard.UnitTests
{
    public class SetServiceTests
    {
        [Fact]
        public void AddPointForPlayer_Score66_IsTieBreakSuccess()
        {
            var set = new SetService(1);

            for(var i = 1; i <= 6; i++)
            {
                AddWinInGameForPlayer(set, WinPlayer.First);
                AddWinInGameForPlayer(set, WinPlayer.Second);
            }

            Assert.Equal(6, set.ScorePlayer1);
            Assert.Equal(6, set.ScorePlayer2);
            Assert.False(set.IsFinished);
            Assert.IsType<TieBreakService>(set.Game);
        }

        [Fact]
        public void AddPointForPlayer_Score64_IsFinishedSuccess()
        {
            var set = new SetService(1);

            for (var i = 1; i <= 4; i++)
            {
                AddWinInGameForPlayer(set, WinPlayer.First);
                AddWinInGameForPlayer(set, WinPlayer.Second);
            }

            AddWinInGameForPlayer(set, WinPlayer.First);
            AddWinInGameForPlayer(set, WinPlayer.First);

            Assert.Equal(6, set.ScorePlayer1);
            Assert.Equal(4, set.ScorePlayer2);
            Assert.True(set.IsFinished);
        }

        private void AddWinInGameForPlayer(SetService set, WinPlayer winner)
        {
            set.AddPointForPlayer(winner);
            set.AddPointForPlayer(winner);
            set.AddPointForPlayer(winner);
            set.AddPointForPlayer(winner);
        }
    }
}
