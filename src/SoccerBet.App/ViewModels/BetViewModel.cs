using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerBet.App.ViewModels
{
    public class BetViewModel
    {
        public string Token { get; set; }
        public Guid MatchId { get; set; }
        public int HomeScoreBoard { get; set; }
        public int AwayScoreBoard { get; set; }
    }
}
