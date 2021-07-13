using SoccerBet.App.Helpers;
using SoccerBet.App.Interfaces;
using SoccerBet.App.Models;
using SoccerBet.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerBet.App.Services
{
    public class BetService : IBetService
    {
        public async Task<BetListUserViewModel> GetBets(string token)
        {
                var result = await HttpConnection.ExecuteRequest<BetListUserViewModel>
                (
                    $"{Configurations.ApiUrl}/bet/findBet", 
                    RestSharp.Method.GET, 
                    token
                );

            return result;
        }

        public async Task<BaseResponse> SendBet(BetViewModel betViewModel)
        {
            var result = await HttpConnection.ExecuteRequest<BaseResponse,BetViewModel>
            (
                $"{Configurations.ApiUrl}/bet/sendBet", 
                RestSharp.Method.POST,
                betViewModel,
                betViewModel.Token
            );

            return result;
        }
    }
}
