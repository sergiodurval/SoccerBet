using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoccerBet.App.Interfaces;
using SoccerBet.App.Models;

namespace SoccerBet.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILeagueService _leagueService;

        public HomeController(ILogger<HomeController> logger, ILeagueService leagueService)
        {
            _logger = logger;
            _leagueService = leagueService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _leagueService.GetAllLeagues();
            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Match(Guid id)
        {
            var result = await _leagueService.GetMatchByLeagueId(id);
            return View(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
