using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoccerBet.Business.Interfaces;

namespace SoccerBet.Api.Controllers
{
    
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LeagueController : ControllerBase
    {
        private readonly ILeagueService _leagueService;

        public LeagueController(ILeagueService leagueService)
        {
            _leagueService = leagueService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var league = await _leagueService.GetAll();
            return Ok(league);
        }

        [HttpGet]
        [Route("match/{leagueId:guid}")]
        public async Task<IActionResult> GetMatchByLeagueId(Guid leagueId)
        {
            var league = await _leagueService.GetAllMatchs(leagueId);
            if (league == null)
                return NotFound();

            return Ok(league);
        }
    }
}
