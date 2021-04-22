using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerBet.Extractor.Models
{
    public class League
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public List<Round> Rounds { get; set; }
    }
}
