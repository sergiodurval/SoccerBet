using Microsoft.EntityFrameworkCore;
using SoccerBet.Business.Interfaces;
using SoccerBet.Business.Models;
using SoccerBet.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoccerBet.Data.Repository
{
    public class RoundRepository : Repository<Round> , IRoundRepository 
    {
        public RoundRepository(SoccerBetDbContext context):base(context){}

        public async Task<IEnumerable<Round>> GetRoundByLeagueId(Guid id)
        {
            return await Search(p => p.LeagueId == id);
        }
    }
}
