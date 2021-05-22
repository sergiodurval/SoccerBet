using SoccerBet.Business.Interfaces;
using SoccerBet.Business.Models;
using SoccerBet.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
namespace SoccerBet.Data.Repository
{
    public class MatchRepository : IMatchRepository
    {
        private readonly IConnectionFactory connection;

        public MatchRepository(IConnectionFactory connection)
        {
            this.connection = connection;
        }

        public async Task<Match> Add(Match match)
        {
            string sql = "insert into [SoccerBet].[dbo].[Matchs] " +
                "(Id," +
                "LeagueId," +
                "RoundId," +
                "HomeTeam," +
                "AwayTeam," +
                "HomeScoreBoard," +
                "AwayScoreBoard," +
                "MatchDate," +
                "CreatedAt,UpdatedAt)" +
                " values (" +
                "@Id," +
                "@LeagueId," +
                "@RoundId," +
                "@HomeTeam," +
                "@AwayTeam," +
                "@HomeScoreBoard," +
                "@AwayScoreBoard," +
                "@MatchDate," +
                "@CreatedAt," +
                "@UpdatedAt)";

            using(var connectionDb = connection.Connection())
            {
                connectionDb.Open();

                await connectionDb.ExecuteAsync(sql, new
                {
                    Id = match.Id,
                    LeagueId = match.LeagueId,
                    RoundId = match.RoundId,
                    HomeTeam = match.HomeTeam,
                    AwayTeam = match.AwayTeam,
                    HomeScoreBoard = match.HomeScoreBoard,
                    AwayScoreBoard = match.AwayScoreBoard,
                    MatchDate = match.MatchDate,
                    CreatedAt = match.CreatedAt,
                    UpdatedAt = match.UpdatedAt
                });

                return match;
            }



        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Match>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Match> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Match>> GetMatchByRound(Guid roundId)
        {
            string sql = $"select * from Matchs where RoundId = '{roundId}'";

            using(var connectionDb = connection.Connection())
            {
                connectionDb.Open();

                var result = await connectionDb.QueryAsync<Match>(sql, new { RoundId = roundId });
                return result;
            }
        }

        public async Task Update(Match match)
        {
            DateTime UpdatedAt = DateTime.Now;
            string sql = $"update Matchs set HomeScoreBoard = @HomeScoreBoard , AwayScoreBoard= @AwayScoreBoard , UpdatedAt= @UpdatedAt where Id= @Id";

            using(var connectionDb = connection.Connection())
            {
                connectionDb.Open();

                var result = await connectionDb.QueryAsync(sql, new
                {
                    HomeScoreBoard = match.HomeScoreBoard,
                    AwayScoreBoard = match.AwayScoreBoard,
                    UpdatedAt = UpdatedAt,
                    Id = match.Id
                });
            }
        }
    }
}
