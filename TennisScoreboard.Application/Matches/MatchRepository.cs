using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisScoreboard.Application.Common.Interfaces;
using TennisScoreboard.Domain.Entities;

namespace TennisScoreboard.Application.Matches
{
    public class MatchRepository : IMatchRepository
    {
        private readonly IApplicationDbContext _context;

        public MatchRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(int playerId1, int playerId2, int winnerId)
        {
            var match = new Match
            {
                IdPlayer1 = playerId1,
                IdPlayer2 = playerId2,
                Winner = winnerId,
            };

            try
            {
                await _context.Matches.AddAsync(match);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Ошибка при сохранении");
            }
        }
    }
}
