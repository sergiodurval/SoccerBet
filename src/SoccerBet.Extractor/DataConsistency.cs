using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SoccerBet.Business.Interfaces;
using SoccerBet.Business.Models;
using SoccerBet.Extractor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SoccerBet.Data.Repository;
using SoccerBet.Data.Context;
using System.Linq;
using System;
using Microsoft.Extensions.Logging;

namespace SoccerBet.Extractor
{
    public interface IDataConsistency
    {
        public void ConsistencyRule(List<LeagueExtractModel> leagues);
    }

    public class DataConsistency : IDataConsistency
    {
        
        private readonly IMapper _mapper;
        private readonly ILeagueRepository _leagueRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IRoundRepository _roundRepository;
        private ILogger _logger;
        public DataConsistency(IMapper mapper , 
                               ILeagueRepository leagueRepository , 
                               IMatchRepository matchRepository , 
                               IRoundRepository roundRepository,
                               ILogger<DataConsistency> logger)
        {
            _mapper = mapper;
            _leagueRepository = leagueRepository;
            _matchRepository = matchRepository;
            _roundRepository = roundRepository;
            _logger = logger;
        }

        public void ConsistencyRule(List<LeagueExtractModel> leagues)
        {
            foreach(var league in leagues)
            {
                _logger.LogInformation($"Liga:{league.Name} - Quantidade de rodadas:{league.Rounds.Count}");
                DecoupleData(league);
            }
        }

        private League ValidateLeagueExists(string leagueName)
        {
            var leagueModel =  _leagueRepository.GetLeagueByName(leagueName).Result;
            return leagueModel;
        }

        private async Task<LeagueExtractModel> AddLeague(LeagueExtractModel leagueExtractModel)
        {
            var league = ValidateLeagueExists(leagueExtractModel.Name);
            
            if(league == null)
            {
                league = await Task.FromResult(_leagueRepository.Add(_mapper.Map<League>(leagueExtractModel)).Result);
                return _mapper.Map<LeagueExtractModel>(league);
            }

            return _mapper.Map<LeagueExtractModel>(league);
            
        }

        private async Task AddRound(List<Round> rounds)
        {
            foreach (var round in rounds)
            {
                _logger.LogInformation($"rodada:{round.Number} - quantidade de partidas:{round.Matchs.Count()}");
                var matchsRound = round.Matchs;
                round.Matchs = null;
                await _roundRepository.Add(round);
                
                foreach(var match in matchsRound)
                {
                    _logger.LogInformation($"rodada:{round.Number} data partida:{match.MatchDate} - {match.HomeTeam} x {match.AwayTeam}");
                    match.LeagueId = round.LeagueId;
                    match.RoundId = round.Id;
                    match.CreatedAt = DateTime.Now;
                    match.UpdatedAt = DateTime.Now;
                    await AddMatch(match);
                }
            }
        }

        private async Task AddMatch(Match match)
        {
            await _matchRepository.Add(match);
        }

        private async void DecoupleData(LeagueExtractModel leagueExtractModel)
        {
            var rounds = _mapper.Map<IEnumerable<Round>>(leagueExtractModel.Rounds).ToList();
            var model  = await AddLeague(leagueExtractModel);
            rounds.Select(c => { c.LeagueId = model.Id; return c; }).ToList();
            await AddRound(rounds);

            
        }

        

    }
}
