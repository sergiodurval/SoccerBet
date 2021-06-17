using SoccerBet.Business.Interfaces;
using SoccerBet.Business.Models;
using SoccerBet.Business.Models.Validations;
using SoccerBet.Business.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoccerBet.Business.Services
{
    public class BetService : BaseService , IBetService
    {
        private readonly IBetRepository _betRepository;
        public BetService(INotification notification, IBetRepository betRepository) : base(notification)
        {
            _betRepository = betRepository;
        }

        public async Task<List<Bet>> GetBetByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return null;
                
          return await _betRepository.GetBetByUserId(userId);
        }

        public async Task<Bet> SendBet(Bet bet)
        {
            if (!ExecuteValidation(new BetValidation(), bet))
                return bet;
            

           return await _betRepository.Add(bet);
        }

        
    }
}
