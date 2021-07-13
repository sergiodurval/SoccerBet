using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerBet.Business.Models
{
    public class Bet : Entity
    {
        public string UserId { get; set; }
        public Guid MatchId { get; set; }
        public int HomeScoreBoard { get; set; }
        public int AwayScoreBoard { get; set; }
        public Match Match { get; set; }
    }
}
