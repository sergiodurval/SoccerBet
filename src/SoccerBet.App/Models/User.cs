using Newtonsoft.Json;
using System.Collections.Generic;

namespace SoccerBet.App.Models
{
    public class User
    {
        [JsonProperty("sucess")]
        public bool Success { get; set; }

        [JsonProperty("data")]
        public string Token { get; set; }

        [JsonProperty("errors")]
        public List<string> Errors { get; set; }
        public string Email { get; set; }
    }
}
