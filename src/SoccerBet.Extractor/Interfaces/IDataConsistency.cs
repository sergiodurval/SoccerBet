using SoccerBet.Extractor.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoccerBet.Extractor.Interfaces
{
    public interface IDataConsistency
    {
        public Task ConsistencyRule(List<LeagueExtractModel> leagues);
    }
}
