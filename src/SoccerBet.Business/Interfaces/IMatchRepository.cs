using SoccerBet.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoccerBet.Business.Interfaces
{
    public interface IMatchRepository
    {
        Task<Match> Add(Match match);
        Task<Match> GetById(Guid id);
        Task<IEnumerable<Match>> GetAll();
        Task Update(Match match);
        Task Delete(Guid id);
        Task<IEnumerable<Match>> GetMatchByRound(Guid roundId);
    }
}
