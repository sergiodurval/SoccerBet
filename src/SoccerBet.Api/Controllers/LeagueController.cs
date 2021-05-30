using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoccerBet.Business.Interfaces;

namespace SoccerBet.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeagueController : ControllerBase
    {
        private readonly ILeagueService _leagueService;

        public LeagueController(ILeagueService leagueService)
        {
            _leagueService = leagueService;
        }

        public async Task<IActionResult> Index()
        {
            var league = await _leagueService.GetAll();
            return Ok(league);
        }
    }
}
