using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoccerBet.App.Interfaces;

namespace SoccerBet.App.Controllers
{
    [Authorize]
    public class BetController : Controller
    {
        private readonly ILeagueService _leagueService;
        private IHttpContextAccessor _httpContextAccessor;
        public BetController(ILeagueService leagueService, IHttpContextAccessor httpContextAccessor)
        {
            _leagueService = leagueService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index(Guid id)
        {
            var result = await _leagueService.GetMatchById(id, GetUserToken());
            return View(result);
        }

        private string GetUserToken()
        {
            string token = _httpContextAccessor.HttpContext.User.FindFirst("Token").Value;
            return token;
        }
    }
}
