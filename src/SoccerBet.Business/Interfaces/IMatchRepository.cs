using SoccerBet.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoccerBet.Business.Interfaces
{
    public interface IMatchRepository : IRepository<Match>
    {
        Task<IEnumerable<Match>> GetMatchByLeagueId(Guid leagueId);
        Task<IEnumerable<Match>> GetMatchByRound(Guid roundId);
    }
}
