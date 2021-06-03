using Dapper;
using SoccerBet.Business.Interfaces;
using SoccerBet.Business.Models;
using SoccerBet.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerBet.Data.Repository
{
    public class LeagueRepository : ILeagueRepository
    {
        private readonly IConnectionFactory connection;

        public LeagueRepository(IConnectionFactory connection)
        {
            this.connection = connection;
        }

        public async Task<League> Add(League league)
        {
            string sql = "insert into [SoccerBet].dbo.[League] ([Id],[Country],[Name]) values (@Id,@Country,@Name)";

            using(var connectionDb = connection.Connection())
            {
                connectionDb.Open();

                await connectionDb.ExecuteAsync(sql, new
                {
                    Id = league.Id,
                    Country = league.Country,
                    Name = league.Name
                });
            }

            return league;
        }

        public async Task Delete(Guid id)
        {
            string sql = "Delete [SoccerBet].[dbo].[League] where [Id] = @Id";

            using(var connectionDb = connection.Connection())
            {
                connectionDb.Open();

                await connectionDb.QueryAsync<League>(sql, new
                {
                    Id = id
                });
            }
        }

        public async Task<IEnumerable<League>> GetAll()
        {
            string sql = "select [Id],[Country],[Name] from [SoccerBet].dbo.[League]";
            IList<League> leagues = new List<League>();

            using (var connectionDb = connection.Connection())
            {
                connectionDb.Open();
                var result = await connectionDb.QueryAsync<dynamic>(sql);

                if (result.Any())
                {
                    foreach (var item in result.ToList())
                    {
                        var league = new League
                        {
                            Id = item.Id,
                            Country = item.Country,
                            Name = item.Name
                        };

                        leagues.Add(league);
                    }
                }
            }

            return leagues;
        }

        
        public async Task<League> GetAllMatchs(Guid leagueId)
        {
            var league = new League();
            string sql = $@"SELECT m.*,
                                   r.*
                           FROM   matchs m
                                  INNER JOIN rounds r
                                        ON m.roundid = r.id
                           WHERE  m.leagueid = '{leagueId}'
                           ORDER  BY r.number";

            using (var connectionDb = connection.Connection())
            {
                connectionDb.Open();
                var matchs = await connectionDb.QueryAsync<Match,Round,Match>(sql,(match , rounds) =>
                {
                    match.Round = rounds;
                    return match;
                },splitOn:"LeagueId,RoundId,Id");

                if (matchs.Count() == 0)
                    return null;

                league.Matchs = matchs;
                return league;
            }
        }

        public async Task<League> GetById(Guid id)
        {
            string sql = "Select * from [SoccerBet].[dbo].[League] where [Id] = @id";

            using(var connectionDb = connection.Connection())
            {
                connectionDb.Open();
                var result = await connectionDb.QueryFirstOrDefaultAsync<League>(sql, new
                {
                    Id = id
                });

                return result;
            }
        }

        public async Task<League> SearchBy(string leagueName , string country)
        {
            string sql = $"select [Id],[Country],[Name] from [SoccerBet].dbo.[League] where [Name] = '{leagueName}' and [Country] = '{country}'";

            using (var connectionDb = connection.Connection())
            {
                connectionDb.Open();

                var result = await connectionDb.QueryFirstOrDefaultAsync<League>(sql, new { Name = leagueName , Country = country });

                return result;
            }
        }

        public async Task Update(League league)
        {
            string sql = "Update [SoccerBet].[dbo].[League] set [Country] = @Country , [Name] = @Name where [Id] = @Id";

            using(var connectionDb = connection.Connection())
            {
                connectionDb.Open();

                await connectionDb.ExecuteAsync(sql, new
                {
                    Country = league.Country,
                    Name = league.Name,
                    Id = league.Id
                });
            }
        }
    }

}
