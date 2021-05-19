using SoccerBet.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerBet.Business.Interfaces
{
    public interface IRoundRepository
    {
        Task<Round> Add(Round round);
        Task<Round> GetById(Guid id);
        Task<IEnumerable<Round>> GetAll();
        Task Update(Round round);
        Task Delete(Guid id);
        Task<IEnumerable<Round>> GetRoundByLeagueId(Guid id);
        Task<IEnumerable<Round>> GetRoundByLeagueName(string leagueName);
    }
}
