using AutoMapper;
using SoccerBet.Business.Interfaces;
using SoccerBet.Extractor.Interfaces;
using SoccerBet.Extractor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerBet.Extractor
{
    public class DataValidate : IDataValidate
    {
        private readonly IRoundRepository _roundRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IMapper _mapper;
        public DataValidate(IRoundRepository roundRepository, IMapper mapper , IMatchRepository matchRepository)
        {
            _roundRepository = roundRepository;
            _mapper = mapper;
            _matchRepository = matchRepository;
        }

        public async Task<List<RoundExtractModel>> GetRoundByLeagueName(string leagueName)
        {
           var roundExtractModels = _mapper.Map<List<RoundExtractModel>>(await _roundRepository.GetRoundByLeagueName(leagueName));
           if(roundExtractModels != null)
           {
                foreach(var roundExtractModel in roundExtractModels)
                {
                    roundExtractModel.Matchs = _mapper.Map<List<MatchExtractModel>>(await _matchRepository.GetMatchByRound(roundExtractModel.Id));
                }
           }

            return roundExtractModels;
        }

        public async Task<bool> RoundsExist(Guid leagueId,int roundNumber)
        {
            IEnumerable<RoundExtractModel> roundsExtractModel = _mapper.Map<IEnumerable<RoundExtractModel>>(await _roundRepository.GetRoundByLeagueId(leagueId));
            
            if(roundsExtractModel != null && roundsExtractModel.Any(x => x.RoundNumber == roundNumber))
            {
                return true;
            }

            return false;
        }

        
    }
}
