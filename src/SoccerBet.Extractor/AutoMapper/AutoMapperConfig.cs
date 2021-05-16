﻿using AutoMapper;
using SoccerBet.Business.Models;
using SoccerBet.Extractor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerBet.Extractor.AutoMapper
{
    public class AutoMapperConfig : Profile
    {

        public class SetLeagueIdValue : IMappingAction<LeagueExtractModel, League>
        {
            public void Process(LeagueExtractModel source, League destination, ResolutionContext context)
            {
                if (source.Id == Guid.Empty)
                    destination.Id = Guid.NewGuid();
            }
        }
        public AutoMapperConfig()
        {
            CreateMap<League, LeagueExtractModel>();
            CreateMap<LeagueExtractModel, League>()
                .ForMember(dest => dest.Rounds, opt => opt.Ignore())
                .AfterMap<SetLeagueIdValue>();
                
            CreateMap<Match, MatchExtractModel>().ReverseMap()
                .ForMember(d => d.HomeTeam, opt => opt.MapFrom(s => s.HomeTeam.Name))
                .ForMember(d => d.AwayTeam, opt => opt.MapFrom(s => s.AwayTeam.Name));

            CreateMap<Round, RoundExtractModel>()
                .ForMember(d => d.Matchs, opt => opt.MapFrom(s => s.Matchs));



            CreateMap<RoundExtractModel, Round>()
                 .ForMember(d => d.Number, opt => opt.MapFrom(s => s.RoundNumber));
                //.ForMember(dest => dest.Matchs, opt => opt.Ignore());

            
        }
    }
}
