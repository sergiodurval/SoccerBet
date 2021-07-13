using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoccerBet.Business.Interfaces;
using SoccerBet.Business.Models;

namespace SoccerBet.Api.Controllers
{
    
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BetController : MainController
    {
        private readonly IBetService _betService;
        public BetController(INotification notification, IBetService betService , IUser user) : base(notification , user)
        {
            _betService = betService;
        }

        [HttpPost]
        [Route("sendBet")]
        public async Task<IActionResult> SendBet(Bet bet)
        {
            bet.UserId = GetUserId();
            return CustomResponse(await _betService.SendBet(bet));
        }

        [HttpGet]
        [Route("findBet")]
        public async Task<IActionResult> FindBet()
        {
            var result = await _betService.GetBetByUserId(GetUserId());
            if (result == null || result.Count == 0)
                return NotFound();

            return CustomResponse(result);
        }

        [HttpGet]
        [Route("findMatch/{matchId}")]
        public async Task<IActionResult> FindMatch(Guid matchId)
        {
            var result = await _betService.GetMatchById(matchId);
            if (result == null)
                return NotFound();

            return CustomResponse(result);
        }

        private string GetUserId()
        {
            return AppUser.GetUserId().ToString();
        }
    }
}
