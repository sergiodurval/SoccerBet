using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerBet.App.ViewModels
{
    public class MatchViewModel
    {
        [JsonProperty("sucess")]
        public bool Sucess { get; set; }

        [JsonProperty("matchs")]
        public List<Match> Matchs { get; set; }
    }

    public class Match
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("leagueId")]
        public Guid LeagueId { get; set; }

        [JsonProperty("homeTeam")]
        public string HomeTeam { get; set; }

        [JsonProperty("awayTeam")]
        public string AwayTeam { get; set; }

        [JsonProperty("homeScoreBoard")]
        public int? HomeScoreBoard { get; set; }

        [JsonProperty("awayScoreBoard")]
        public int? AwayScoreBoard { get; set; }

        [JsonProperty("matchDate")]
        public DateTime MatchDate { get; set; }

        [JsonProperty("round")]
        public RoundViewModel Round { get; set; }

        public string CompleteScoreBoard
        {
            get { return GetCompleteScoreBoard(HomeTeam, HomeScoreBoard, AwayTeam, AwayScoreBoard); }
        }

        public bool RenderBetButton
        {
            get { return HasRenderButton(); }
        }

        private string GetCompleteScoreBoard(string homeTeam , int? homeScoreBoard , string awayTeam , int? awayScoreBoard)
        {
            string completeScoreBoard = string.Empty;

            if(homeScoreBoard.HasValue && awayScoreBoard.HasValue)
            {
                completeScoreBoard = $"{homeTeam} {homeScoreBoard.Value} X {awayScoreBoard.Value} {awayTeam}";
                return completeScoreBoard;
            }

            completeScoreBoard = $"{homeTeam} X {awayTeam}";
            return completeScoreBoard;
        }

        private bool HasRenderButton()
        {
            if (HomeScoreBoard.HasValue && AwayScoreBoard.HasValue)
                return false;

            return true;
        }

        
    }

    public class RoundViewModel
    {
        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}
