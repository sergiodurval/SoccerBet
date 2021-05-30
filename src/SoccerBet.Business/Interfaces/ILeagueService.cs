using SoccerBet.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerBet.Business.Interfaces
{
    public interface ILeagueService
    {
        Task<IEnumerable<League>> GetAll();
        Task<League> GetAllMatchs(Guid leagueId);
    }
}
