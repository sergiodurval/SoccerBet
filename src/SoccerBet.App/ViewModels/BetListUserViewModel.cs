using Newtonsoft.Json;
using SoccerBet.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerBet.App.ViewModels
{
    public class BetListUserViewModel : BaseResponse
    {
        [JsonProperty("data")]
        public List<Bet> ListBets { get; set; }
    }

    public class Bet
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("match")]
        public Match Matchs { get; set; }
    }

    
}
