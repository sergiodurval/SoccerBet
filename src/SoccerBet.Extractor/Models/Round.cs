using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerBet.Extractor.Models
{
    public class Round
    {
        public List<Match> Matchs { get; set; }
        public int RoundNumber { get; set; }
    }
}
