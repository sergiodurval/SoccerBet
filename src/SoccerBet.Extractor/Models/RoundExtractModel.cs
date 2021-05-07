using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerBet.Extractor.Models
{
    public class RoundExtractModel
    {
        public List<MatchExtractModel> Matchs { get; set; } = new List<MatchExtractModel>();
        public int RoundNumber { get; set; }
    }
}
