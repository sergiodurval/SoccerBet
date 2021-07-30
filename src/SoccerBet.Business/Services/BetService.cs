using SoccerBet.Business.Interfaces;
using SoccerBet.Business.Models;
using SoccerBet.Business.Models.Validations;
using System;
using System.Collections.Generic;
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
                
           var betList  = await _betRepository.GetBetByUserId(userId);
           return await ValidateUserHitBet(betList);
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

        private async Task<List<Bet>> ValidateUserHitBet(List<Bet> betList)
        {
            var updatedListBet = new List<Bet>();

            foreach(var bet in betList)
            {
                if(bet.HitBet == null)
                {
                    var match = await GetMatchById(bet.Match.Id);
                    if(match.HomeScoreBoard.HasValue && match.AwayScoreBoard.HasValue)
                    {
                        if(bet.Match.HomeScoreBoard.Value == match.HomeScoreBoard.Value && bet.Match.AwayScoreBoard.Value == match.AwayScoreBoard.Value)
                        {
                            bet.HitBet = true;
                            await _betRepository.UpdateBet(bet.Id, true);
                        }
                        else
                        {
                            bet.HitBet = false;
                            await _betRepository.UpdateBet(bet.Id, false);
                        }
                    }
                }
                updatedListBet.Add(bet);
            }

            return updatedListBet;
        }

        
    }
}
