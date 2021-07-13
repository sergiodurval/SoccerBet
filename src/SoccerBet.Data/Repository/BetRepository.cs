using Dapper;
using SoccerBet.Business.Interfaces;
using SoccerBet.Business.Models;
using SoccerBet.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerBet.Data.Repository
{
    public class BetRepository : IBetRepository
    {
        private readonly IConnectionFactory _connection;

        public BetRepository(IConnectionFactory connection)
        {
          _connection = connection;
        }

        public async Task<Bet> Add(Bet bet)
        {
            string sql = @$"insert into [SoccerBet].[dbo].[Bet]
                                        (
                                        Id,
                                        UserId,
                                        MatchId,
                                        HomeScoreBoard,
                                        AwayScoreBoard,
                                        CreatedAt)
                                        values(@Id,@UserId,@MatchId,@HomeScoreBoard,@AwayScoreBoard,@CreatedAt)";

            using(var connectionDb = _connection.Connection())
            {
                connectionDb.Open();

                await connectionDb.ExecuteAsync(sql, new
                {
                    Id = bet.Id,
                    UserId = bet.UserId,
                    MatchId = bet.MatchId,
                    HomeScoreBoard = bet.HomeScoreBoard,
                    AwayScoreBoard = bet.AwayScoreBoard,
                    CreatedAt = DateTime.Now
                });

                return bet;
            }
        }

        public async Task<List<Bet>> GetBetByUserId(string userId)
        {
            string sql = @$"SELECT b.*,
                           m.hometeam,
                           m.awayteam
                           FROM   bet b
                           INNER JOIN matchs m
                           ON b.matchid = m.id
                           WHERE b.userid = '{userId}'";

            using (var connectionDb = _connection.Connection())
            {
                connectionDb.Open();

                var result = await connectionDb.QueryAsync<Bet, Match, Bet>(sql,map: (bet, match) =>
                    {
                        bet.Match = match;
                        return bet;
                    }, splitOn: "Id,MatchId");

                return result.AsList();
            }
        }
    }
}
