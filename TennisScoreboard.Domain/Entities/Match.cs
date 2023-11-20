namespace TennisScoreboard.Domain.Entities
{
    public class Match
    {
        public int Id { get; set; }

        public int IdPlayer1 { get; set; }

        public int IdPlayer2 { get; set; }

        public int Winner { get; set; }

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

        public Player PlayerWinner { get; set; }
    }
}
