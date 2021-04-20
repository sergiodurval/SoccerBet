using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerBet.Extractor.Models
{
    public class Match
    {
        public int Number { get; set; }
        public string MatchDate { get; set; }
        public Teams[] Teams { get; set; }
    }
}
