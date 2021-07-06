using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SoccerBet.App.Helpers;
using SoccerBet.App.Interfaces;
using SoccerBet.App.Models;
using SoccerBet.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
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

        public async Task<User> Login(LoginViewModel login)
        {
                var result = await HttpConnection.ExecuteRequest<User, LoginViewModel>
                (
                    $"{Configurations.ApiUrl}/account/login", 
                    RestSharp.Method.POST, 
                    login
                );

            return result;
        }

        public async Task<User> Register(RegisterUserViewModel registerUser)
        {
            var result = await HttpConnection.ExecuteRequest<User, RegisterUserViewModel>
                (   $"{Configurations.ApiUrl}/account/register",
                    RestSharp.Method.POST,
                    registerUser
                );

            return result;
        }
    }
}
