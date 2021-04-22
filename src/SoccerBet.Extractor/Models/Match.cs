using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerBet.Extractor.Models
{
    public class Match
    {
        public string MatchDate { get; set; }
        public List<Teams> Teams { get; set; } = new List<Teams>();
    }
}
