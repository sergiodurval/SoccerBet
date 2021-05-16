using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerBet.Extractor.Models
{
    public class MatchExtractModel
    {
        public Guid Id { get; set; }
        public DateTime MatchDate { get; set; }
        public TeamExtractModel HomeTeam { get; set; }
        public TeamExtractModel AwayTeam { get; set; }
        
    }
}
