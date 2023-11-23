using TennisScoreboard.Domain.Entities;

namespace TennisScoreboard.Application.Common.Interfaces
{
    public interface IPlayerRepository
    {
        Task<Player> GetByName(string name);

        Task<Player> GetByNameOrCreate(string name);

        Task<Player> GetById(int id);
    }
}
