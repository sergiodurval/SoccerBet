using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerBet.Extractor.Models
{
    public class LeagueExtractModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public List<RoundExtractModel> Rounds { get; set; } = new List<RoundExtractModel>();
    }
}
