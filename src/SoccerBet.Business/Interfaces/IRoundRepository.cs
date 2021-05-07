using SoccerBet.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerBet.Business.Interfaces
{
    public interface IRoundRepository : IRepository<Round>
    {
        Task<IEnumerable<Round>> GetRoundByLeagueId(Guid id);
    }
}
