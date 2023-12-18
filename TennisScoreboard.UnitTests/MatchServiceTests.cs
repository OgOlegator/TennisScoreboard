using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisScoreboard.Domain.Enums;
using TennisScoreboard.Infrastructure.Services;

namespace TennisScoreboard.UnitTests
{
    public class MatchServiceTests
    {
        [Fact]
        public void AddPointForPlayer_Score20_IsFinishedSuccess()
        {
            var match = new MatchService(1, 2);

            AddWinInSetForPlayer(match, WinPlayer.First);
            AddWinInSetForPlayer(match, WinPlayer.First);

            Assert.True(match.IsFinished);
            Assert.Equal(1, match.WinnerId);
        }

        [Fact]
        public void AddPointForPlayer_Score12_IsFinishedSuccess()
        {
            var match = new MatchService(1, 2);

            AddWinInSetForPlayer(match, WinPlayer.First);
            AddWinInSetForPlayer(match, WinPlayer.Second);
            AddWinInSetForPlayer(match, WinPlayer.Second);

            Assert.True(match.IsFinished);
            Assert.Equal(2, match.WinnerId);
        }

        [Fact]
        public void GetSetByNumber_GetSetNumber3_NotNull()
        {
            var match = new MatchService(1, 2);

            AddWinInSetForPlayer(match, WinPlayer.First);

            Assert.NotNull(match.GetSetByNumber(3));
        }

        [Fact]
        public void GetSetByNumber_GetSetNumber2_Success()
        {
            var match = new MatchService(1, 2);

            AddWinInSetForPlayer(match, WinPlayer.First);
            AddWinInSetForPlayer(match, WinPlayer.Second);
            AddWinInSetForPlayer(match, WinPlayer.Second);

            Assert.NotNull(match.GetSetByNumber(2));
        }

        [Fact]
        public void GetSetByNumber_GetSetNumber5_Exception()
        {
            var match = new MatchService(1, 2);

            AddWinInSetForPlayer(match, WinPlayer.First);

            Assert.Throws<ArgumentException>(() => match.GetSetByNumber(5));
        }

        [Fact]
        public void GetWinPlayerById_GetPlayerWithId2_Success()
        {
            var match = new MatchService(2, 1);

            Assert.Equal(WinPlayer.First, match.GetWinPlayerById(2));
        }

        [Fact]
        public void GetWinPlayerById_GetPlayerWithId11_Exception()
        {
            var match = new MatchService(2, 1);

            Assert.Throws<ArgumentException>(() => match.GetWinPlayerById(11));
        }

        [Fact]
        public void GetCurrentSet_GetSet2_NotNullSuccess()
        {
            var match = new MatchService(2, 1);

            AddWinInSetForPlayer(match, WinPlayer.First);

            var currentSet = match.GetCurrentSet();

            Assert.NotNull(currentSet);
            Assert.Equal(2, currentSet.Number);
        }

        private void AddWinInSetForPlayer(MatchService match, WinPlayer winner)
        {
            for(var i = 1; i <= 24; i++)
                match.AddPointForPlayer(winner);
        }
    }
}
