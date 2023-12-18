using TennisScoreboard.Domain.Enums;
using TennisScoreboard.Infrastructure.Services;

namespace TennisScoreboard.UnitTests
{
    public class GameServiceTests
    {
        [Fact]
        public void AddPointForPlayer_Score4040_NotFinished()
        {
            var game = new GameService();

            game.AddPointForPlayer(WinPlayer.First);
            game.AddPointForPlayer(WinPlayer.First);
            game.AddPointForPlayer(WinPlayer.First);
            game.AddPointForPlayer(WinPlayer.Second);
            game.AddPointForPlayer(WinPlayer.Second);
            game.AddPointForPlayer(WinPlayer.Second);

            Assert.Equal(0, game.ScorePlayer1);
            Assert.Equal(0, game.ScorePlayer2);
            Assert.False(game.IsFinished);

            game.AddPointForPlayer(WinPlayer.First);

            Assert.False(game.IsFinished);
            Assert.Equal(1, game.ScorePlayer1);
        }

        public void AddPointForPlayer_Score040_Finished()
        {
            var game = new GameService();

            game.AddPointForPlayer(WinPlayer.First);
            game.AddPointForPlayer(WinPlayer.First);
            game.AddPointForPlayer(WinPlayer.First);
            game.AddPointForPlayer(WinPlayer.First);

            Assert.True(game.IsFinished);
        }
    }
}