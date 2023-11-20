using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisScoreboard.Domain.Entities;

namespace TennisScoreboard.Application.Common.Interfaces
{
    public interface IPlayerRepository
    {
        Task<Player> GetByName(string name);

        Task<Player> GetByNameOrCreate(string name);
    }
}
