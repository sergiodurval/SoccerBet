using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoccerBet.App.Interfaces;
using SoccerBet.App.ViewModels;

namespace SoccerBet.App.Controllers
{
    [Authorize]
    public class BetController : Controller
    {
        private readonly ILeagueService _leagueService;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IBetService _betService;
        public BetController(ILeagueService leagueService, IHttpContextAccessor httpContextAccessor, IBetService betService)
        {
            _leagueService = leagueService;
            _httpContextAccessor = httpContextAccessor;
            _betService = betService;
        }

        public async Task<IActionResult> Index(Guid id)
        {
            var result = await _leagueService.GetMatchById(id, GetUserToken());
            return View(result);
        }

        public async Task<IActionResult> SendBet(MatchViewModel matchViewModel)
        {
            if (!ModelState.IsValid)
                return View();

            var bet = new BetViewModel()
            {
                MatchId = matchViewModel.Match.Id,
                HomeScoreBoard = matchViewModel.Match.HomeScoreBoard.Value,
                AwayScoreBoard = matchViewModel.Match.AwayScoreBoard.Value,
                Token = GetUserToken()
            };

            var result = await _betService.SendBet(bet);

            if (result.Success)
                return RedirectToAction("Index", "Home");

            return View();
        }

        public async Task<IActionResult> ListBet()
        {
            var result = await _betService.GetBets(GetUserToken());

            return View(result);
        }

        private string GetUserToken()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst("Token").Value;
        }
    }
}
