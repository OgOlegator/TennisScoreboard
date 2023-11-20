using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisScoreboard.Application.Common.Interfaces;

namespace TennisScoreboard.Infrastructure.Services
{
    public class MatchService
    {
        private readonly int _idPlayer1;
        private readonly int _idPlayer2;
        
        private int _scorePlayer1 = 0;
        private int _scorePlayer2 = 0;

        public MatchService(int idPlayer1, int idPlayer2)
        {
            _idPlayer1 = idPlayer1;
            _idPlayer2 = idPlayer2;
        }
    }
}
