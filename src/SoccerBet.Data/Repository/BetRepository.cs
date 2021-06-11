using SoccerBet.Business.Interfaces;
using SoccerBet.Business.Models;
using System;
using System.Threading.Tasks;

namespace SoccerBet.Data.Repository
{
    public class BetRepository : IBetRepository
    {
        public Task<Bet> Add(Bet bet)
        {
            throw new NotImplementedException();
        }
    }
}
