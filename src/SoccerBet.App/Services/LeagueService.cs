using Newtonsoft.Json;
using SoccerBet.App.Helpers;
using SoccerBet.App.Interfaces;
using SoccerBet.App.ViewModels;
using System;
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

        public async Task<MatchViewModel> GetMatchByLeagueId(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{Configurations.ApiUrl}/league/match/{id}");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<MatchViewModel>(json);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
