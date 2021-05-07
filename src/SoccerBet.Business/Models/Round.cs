using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerBet.Business.Models
{
    public class Round : Entity
    {
        public Guid LeagueId { get; set; }
        public int Number { get; set; }

        /* EF Relations */
        public IEnumerable<Match> Matchs { get; set; }
    }
}
