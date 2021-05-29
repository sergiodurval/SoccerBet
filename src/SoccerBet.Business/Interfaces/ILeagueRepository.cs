using SoccerBet.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoccerBet.Business.Interfaces
{
    public interface ILeagueRepository
    {
        Task<League> Add(League league);
        Task<League> GetById(Guid id);
        Task<IEnumerable<League>> GetAll();
        Task Update(League league);
        Task Delete(Guid id);
        Task<League> SearchBy(string leagueName , string country);
    }
}
