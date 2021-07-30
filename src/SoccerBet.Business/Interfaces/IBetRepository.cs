using SoccerBet.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerBet.Business.Interfaces
{
    public interface IBetRepository
    {
        Task<Bet> Add(Bet bet);
        Task<List<Bet>> GetBetByUserId(string userId);
        Task UpdateBet(Guid id, bool hitBet);
    }
}
