using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerBet.App.Models
{
    public class BaseResponse
    {
        [JsonProperty("sucess")]
        public bool Success { get; set; }

        [JsonProperty("errors")]
        public List<string> Errors { get; set; }
    }
}
