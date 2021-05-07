using SoccerBet.Business.Interfaces;
using SoccerBet.Business.Models;
using SoccerBet.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerBet.Data.Repository
{
    public class MatchRepository : Repository<Match> , IMatchRepository
    {
        public MatchRepository(SoccerBetDbContext context):base(context){}
    }
}
