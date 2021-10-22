using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SoccerBet.Api.ViewModels;
using SoccerBet.Business.Interfaces;

namespace SoccerBet.Api.Controllers
{
    
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LeagueController : MainController
    {
        private readonly ILeagueService _leagueService;
        private readonly IMapper _mapper;
        public LeagueController(INotification notification, ILeagueService leagueService, IMapper mapper , IUser user) : base(notification , user)
        {
            _leagueService = leagueService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var league = await _leagueService.GetAll();
            return CustomResponse(league);
        }

        [HttpGet]
        [Route("match/{leagueId:guid}")]
        public async Task<IActionResult> GetMatchByLeagueId(Guid leagueId)
        {
            var leagueMatchs = _mapper.Map<MatchViewModel>(await _leagueService.GetAllMatchs(leagueId));
            if (leagueMatchs == null)
                return NotFound();

            return Ok(leagueMatchs);
        }
    }
}
