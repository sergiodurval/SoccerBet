using SoccerBet.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoccerBet.Business.Interfaces
{
    public interface ILeagueRepository : IRepository<League>
    {
        Task<IEnumerable<Match>> GetMatchsByLeagueId(Guid id);
    }
}
