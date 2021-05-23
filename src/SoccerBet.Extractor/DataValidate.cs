using AutoMapper;
using Microsoft.Extensions.Logging;
using SoccerBet.Business.Interfaces;
using SoccerBet.Business.Models;
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
        private readonly ILogger _logger;
        public DataValidate(IRoundRepository roundRepository, 
                            IMapper mapper, 
                            IMatchRepository matchRepository, 
                            ILogger<DataValidate> logger)
        {
            _roundRepository = roundRepository;
            _mapper = mapper;
            _matchRepository = matchRepository;
            _logger = logger;
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

            return RemovingRoundsNotBeExtracted(roundExtractModels);
        }

        private List<RoundExtractModel> RemovingRoundsNotBeExtracted(List<RoundExtractModel> rounds)
        {
            var validRoundsToBeExtracted = rounds
                                           .OrderBy(x => x.RoundNumber)
                                           .Where(x => x.Matchs.Any(y => y.MatchDate <= DateTime.Now 
                                           && y.HomeTeam.HomeScoreBoard == null && y.AwayTeam.AwayScoreBoard == null));

            return validRoundsToBeExtracted.ToList();
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

        public async Task UpdateMatchs(List<RoundExtractModel> rounds)
        {
            foreach(var round in rounds)
            {
                var matchs = await _matchRepository.GetMatchByRound(round.Id);
                
                foreach(var match in matchs)
                {
                    var updateMatch = round.Matchs.Where(x => x.HomeTeam.Name == match.HomeTeam && x.AwayTeam.Name == match.AwayTeam).FirstOrDefault();
                    
                    if(updateMatch != null)
                    {
                        updateMatch.Id = match.Id;
                        await _matchRepository.Update(_mapper.Map<Match>(updateMatch));
                        _logger.LogInformation($"rodada:{round.RoundNumber} atualizada - {updateMatch.HomeTeam.Name} {updateMatch.HomeTeam.HomeScoreBoard} X {updateMatch.AwayTeam.Name} {updateMatch.AwayTeam.AwayScoreBoard}");
                    }
                    
                }
            }
        }
    }
}
