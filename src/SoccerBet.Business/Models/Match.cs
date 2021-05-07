using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerBet.Business.Models
{
    public class Match : Entity
    {
        public Guid LeagueId { get; set; }
        public Guid RoundId { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public int? HomeScoreBoard { get; set; }
        public int? AwayScoreBoard { get; set; }
        public DateTime MatchDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        /* EF Relations */
        public Round Round { get; set; }
    }
}
