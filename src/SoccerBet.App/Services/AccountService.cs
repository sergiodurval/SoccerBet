using Newtonsoft.Json;
using SoccerBet.App.Helpers;
using SoccerBet.App.Interfaces;
using SoccerBet.App.Models;
using SoccerBet.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SoccerBet.App.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserToken> Login(LoginViewModel login)
        {
            var result = await HttpConnection.ExecuteRequest<UserToken, LoginViewModel>($"{Configurations.ApiUrl}/account/login", RestSharp.Method.POST, login);

            return result;
        }
    }
}
