using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoccerBet.Business.Interfaces;
using SoccerBet.Business.Models;

namespace SoccerBet.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
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
    }
}
