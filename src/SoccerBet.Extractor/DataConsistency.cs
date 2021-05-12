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
        private IHost host;
        public DataConsistency(IMapper mapper , ILeagueRepository leagueRepository , IMatchRepository matchRepository , IRoundRepository roundRepository)
        {
            _mapper = mapper;
            _leagueRepository = leagueRepository;
            _matchRepository = matchRepository;
            _roundRepository = roundRepository;
            // host = CreateHostBuilder().Build();
            //_leagueRepository = host.Services.GetRequiredService<ILeagueRepository>();
            //_matchRepository = host.Services.GetRequiredService<IMatchRepository>();
            //_roundRepository = host.Services.GetRequiredService<IRoundRepository>();
            //_mapper = host.Services.GetRequiredService<IMapper>();
        }

        public void ConsistencyRule(List<LeagueExtractModel> leagues)
        {
            foreach(var league in leagues)
            {
                AddLeague(league);
            }
        }

        private bool ValidateLeagueExists(string leagueName)
        {
            var league = _leagueRepository.GetLeagueByName(leagueName).Result;

            if (league != null)
                return true;

            return false;

        }

        private async void AddLeague(LeagueExtractModel leagueExtractModel)
        {
            if(!ValidateLeagueExists(leagueExtractModel.Name))
            {
                await Task.FromResult(_leagueRepository.Add(_mapper.Map<League>(leagueExtractModel)));
            }
        }

       
    }
}
