using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerBet.Business.Models
{
    public class League : Entity
    {
        public string Country { get; set; }
        public string Name { get; set; }

        /* EF Relations */
        public IEnumerable<Round> Rounds { get; set; }
    }
}
