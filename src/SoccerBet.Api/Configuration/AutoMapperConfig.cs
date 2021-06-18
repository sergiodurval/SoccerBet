using AutoMapper;
using SoccerBet.Api.ViewModels;
using SoccerBet.Business.Models;

namespace SoccerBet.Api.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<League, MatchViewModel>();
        }
    }
}
