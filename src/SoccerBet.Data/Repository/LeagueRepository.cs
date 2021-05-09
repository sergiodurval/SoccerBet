using Microsoft.EntityFrameworkCore;
using SoccerBet.Business.Interfaces;
using SoccerBet.Business.Models;
using SoccerBet.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerBet.Data.Repository
{
    public class LeagueRepository : Repository<League> , ILeagueRepository
    {
        public LeagueRepository(SoccerBetDbContext context):base(context){}

        public async Task<League> GetLeagueByName(string leagueName)
        {
            return await SearchBy(x => x.Name == leagueName);
        }

        public async Task<IEnumerable<Match>> GetMatchsByLeagueId(Guid id)
        {
            return await Db.Match.AsNoTracking().Include(f => f.League).ToListAsync();
        }

        
    }
}
