using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerBet.App.Models
{
    public class UserToken
    {
        [JsonProperty("sucess")]
        public bool Success { get; set; }

        [JsonProperty("data")]
        public string Token { get; set; }

        [JsonProperty("errors")]
        public List<string> Errors { get; set; }
    }
}
