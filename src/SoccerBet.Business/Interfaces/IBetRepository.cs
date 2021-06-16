using SoccerBet.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerBet.Business.Interfaces
{
    public interface IBetRepository
    {
        Task<Bet> Add(Bet bet);
        Task<List<Bet>> GetBetByUserId(string userId);
    }
}
