using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SoccerBet.App.Helpers;
using SoccerBet.App.Interfaces;
using SoccerBet.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SoccerBet.App.Services
{
    public class LeagueService : ILeagueService
    {
        private readonly HttpClient _httpClient;

        public LeagueService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LeagueViewModel> GetAllLeagues()
        {
            var result = await HttpConnection.ExecuteRequest<LeagueViewModel>
            (
                $"{Configurations.ApiUrl}/league",
                RestSharp.Method.GET
            );

            return result;
        }

        public async Task<MatchViewModel> GetMatchById(Guid id, string token)
        {
                var result = await HttpConnection.ExecuteRequest<MatchViewModel>
                (
                    $"{Configurations.ApiUrl}/bet/findMatch/{id}", 
                    RestSharp.Method.GET, 
                    token
                );

            return result;
        }

        public async Task<MatchViewModel> GetMatchByLeagueId(Guid id)
        {
            var result = await HttpConnection.ExecuteRequest<MatchViewModel>
                (
                    $"{Configurations.ApiUrl}/league/match/{id}",
                    RestSharp.Method.GET
                );

            return result;
        }
    }
}
