using Microsoft.EntityFrameworkCore;
using TennisScoreboard.Domain.Entities;

namespace TennisScoreboard.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Player> Players { get; }

        DbSet<Match> Matches { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
