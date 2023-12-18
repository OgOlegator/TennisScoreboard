using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisScoreboard.Domain.Enums;
using TennisScoreboard.Infrastructure.Services;

namespace TennisScoreboard.UnitTests
{
    public class TieBreakServiceTests
    {
        [Fact]
        public void AddPointForPlayer_Score76_IsNotFinished()
        {
            var tieBreak = new TieBreakService();

            for (var i = 1; i <= 6; i++)
            {
                tieBreak.AddPointForPlayer(WinPlayer.First);
                tieBreak.AddPointForPlayer(WinPlayer.Second);
            }

            tieBreak.AddPointForPlayer(WinPlayer.First);

            Assert.Equal(7, tieBreak.ScorePlayer1);
            Assert.Equal(6, tieBreak.ScorePlayer2);
            Assert.False(tieBreak.IsFinished);
        }

        [Fact]
        public void AddPointForPlayer_Score1210_IsFinishedSuccess()
        {
            var tieBreak = new TieBreakService();

            for (var i = 1; i <= 10; i++)
            {
                tieBreak.AddPointForPlayer(WinPlayer.First);
                tieBreak.AddPointForPlayer(WinPlayer.Second);
            }

            tieBreak.AddPointForPlayer(WinPlayer.First);
            tieBreak.AddPointForPlayer(WinPlayer.First);

            Assert.Equal(12, tieBreak.ScorePlayer1);
            Assert.Equal(10, tieBreak.ScorePlayer2);
            Assert.True(tieBreak.IsFinished);
        }
    }
}
