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
            try
            {
                var response = await _httpClient.GetAsync($"{Configurations.ApiUrl}/league");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<LeagueViewModel>(json);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            try
            {
                var response = await _httpClient.GetAsync($"{Configurations.ApiUrl}/league/match/{id}");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                JObject jsonObject = JObject.Parse(json);
                var matchsJson = jsonObject["data"]["matchs"];
                var result = JsonConvert.DeserializeObject<MatchViewModel>(json);
                result.Matchs = JsonConvert.DeserializeObject<List<Match>>(matchsJson.ToString());
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
