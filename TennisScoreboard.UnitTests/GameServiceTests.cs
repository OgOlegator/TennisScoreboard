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

            Assert.Equal(game.ScorePlayer1, 0);
            Assert.Equal(game.ScorePlayer2, 0);
            Assert.Equal(game.IsFinished, false);

            game.AddPointForPlayer(WinPlayer.First);

            Assert.Equal(game.IsFinished, false);
            Assert.Equal(game.ScorePlayer1, 1);
        }

        public void AddPointForPlayer_Score040_Finished()
        {
            var game = new GameService();

            game.AddPointForPlayer(WinPlayer.First);
            game.AddPointForPlayer(WinPlayer.First);
            game.AddPointForPlayer(WinPlayer.First);
            game.AddPointForPlayer(WinPlayer.First);

            Assert.Equal(game.IsFinished, true);
        }
    }
}