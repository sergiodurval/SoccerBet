using SoccerBet.Extractor.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoccerBet.Extractor.Interfaces
{
    public interface IDataValidate
    {
        Task<bool> RoundsExist(Guid leagueId, int roundNumber);
        Task<List<RoundExtractModel>> GetRoundByLeagueName(string leagueName);
    }
}
