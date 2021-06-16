using SoccerBet.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoccerBet.Business.Interfaces
{
    public interface IBetService
    {
        Task<Bet> SendBet(Bet bet);
        Task<List<Bet>> GetBetByUserId(string userId);
    }
}
