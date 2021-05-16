using SoccerBet.Extractor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerBet.Extractor.Interfaces
{
    public interface IDataConsistency
    {
        public void ConsistencyRule(List<LeagueExtractModel> leagues);
    }
}
