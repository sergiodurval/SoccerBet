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
        private readonly IMatchRepository _matchRepository;
        public BetService(INotification notification, IBetRepository betRepository , IMatchRepository matchRepository) : base(notification)
        {
            _betRepository = betRepository;
            _matchRepository = matchRepository;
        }

        public async Task<List<Bet>> GetBetByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return null;
                
          return await _betRepository.GetBetByUserId(userId);
        }

        public async Task<Match> GetMatchById(Guid id)
        {
            return await _matchRepository.GetById(id);
        }

        public async Task<Bet> SendBet(Bet bet)
        {
            if (!ExecuteValidation(new BetValidation(), bet))
                return bet;
            

           return await _betRepository.Add(bet);
        }

        
    }
}
