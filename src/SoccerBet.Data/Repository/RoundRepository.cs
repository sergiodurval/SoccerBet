using SoccerBet.Business.Interfaces;
using SoccerBet.Business.Models;
using SoccerBet.Data.Interfaces;
using System;
using System.Collections.Generic;
using Dapper;
using System.Threading.Tasks;
using System.Linq;

namespace SoccerBet.Data.Repository
{
    public class RoundRepository : IRoundRepository
    {
        private readonly IConnectionFactory connection;

        public RoundRepository(IConnectionFactory connection)
        {
            this.connection = connection;
        }

        public Task<Round> Add(Round round)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Round>> GetAll()
        {
            string sql = "select * from [SoccerBet].[dbo].[Rounds]";
            IList<Round> rounds = new List<Round>();

            using(var connectionDb = connection.Connection())
            {
                connectionDb.Open();
                var result = await connectionDb.QueryAsync<dynamic>(sql);

                if(result.Any())
                {
                    foreach(var item in result)
                    {
                        var round = new Round()
                        {
                            Id = item.Id,
                            LeagueId = item.LeagueId,
                            Number = item.Number
                        };

                        rounds.Add(round);
                    }
                }

                return rounds;
            }
            
        }

        public async Task<Round> GetById(Guid id)
        {
            string sql = "select * from [SoccerBet] where Id = @id";

            using(var connectionDb = connection.Connection())
            {
                connectionDb.Open();

                var result = await connectionDb.QueryFirstOrDefaultAsync<Round>(sql, new
                {
                    Id = id
                });

                return result;
            }
        }

        public Task<IEnumerable<Round>> GetRoundByLeagueId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Round round)
        {
            throw new NotImplementedException();
        }
    }
}
