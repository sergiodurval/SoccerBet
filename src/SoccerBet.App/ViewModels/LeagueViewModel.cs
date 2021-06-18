using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SoccerBet.App.ViewModels
{
    
    public class LeagueViewModel
    {
        [JsonProperty("sucess")]
        public bool Sucess { get; set; }

        [JsonProperty("data")]
        public List<Result> Leagues { get; set; }

    }

    public class Result
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
