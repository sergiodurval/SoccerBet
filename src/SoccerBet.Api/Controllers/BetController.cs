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
        public BetController(INotification notification, IBetService betService) : base(notification)
        {
            _betService = betService;
        }

        [HttpPost]
        [Route("sendBet")]
        public async Task<IActionResult> SendBet(Bet bet)
        {
            return CustomResponse(await _betService.SendBet(bet));
        }

        [HttpGet]
        [Route("findBet/{userId}")]
        public async Task<IActionResult> FindBet(string userId)
        {
            var result = await _betService.GetBetByUserId(userId);
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
    }
}
