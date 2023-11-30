using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisScoreboard.Application.Common.Interfaces
{
    public interface IMatchRepository
    {

        Task AddAsync(int playerId1, int playerId2, int winnerId);

    }
}
