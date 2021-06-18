using SoccerBet.Business.Models;
using System.Collections.Generic;

namespace SoccerBet.Api.ViewModels
{
    public class MatchViewModel
    {
        public IEnumerable<Match> Matchs { get; set; }
    }
}
