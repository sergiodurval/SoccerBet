using SoccerBet.App.Models;
using SoccerBet.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerBet.App.Interfaces
{
    public interface IBetService
    {
        Task<BaseResponse> SendBet(BetViewModel betViewModel);
        Task<BetListUserViewModel> GetBets(string token);
    }
}
