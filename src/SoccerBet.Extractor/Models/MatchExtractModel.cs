using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerBet.Extractor.Models
{
    public class MatchExtractModel
    {
        public string MatchDate { get; set; }
        public TeamExtractModel HomeTeam { get; set; }
        public TeamExtractModel AwayTeam { get; set; }
        
    }
}
