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
        private readonly IMapper _mapper;
        public DataValidate(IRoundRepository roundRepository, IMapper mapper)
        {
            _roundRepository = roundRepository;
            _mapper = mapper;
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
