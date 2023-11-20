using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisScoreboard.Application.Common.Interfaces;
using TennisScoreboard.Domain.Entities;

namespace TennisScoreboard.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IApplicationDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }

        public DbSet<Match> Matches { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Player>()
                .HasKey(player => player.Id);

            modelBuilder
                .Entity<Match>()
                .HasKey(match => match.Id);

            modelBuilder
                .Entity<Player>()
                .HasIndex(player => player.Name, "Player_Name");

            modelBuilder
                .Entity<Match>()
                .HasOne(match => match.Player1)
                .WithMany()
                .HasForeignKey(player => player.IdPlayer1)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<Match>()
                .HasOne(match => match.Player2)
                .WithMany()
                .HasForeignKey(player => player.IdPlayer2)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<Match>()
                .HasOne(match => match.PlayerWinner)
                .WithMany()
                .HasForeignKey(player => player.Winner)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
