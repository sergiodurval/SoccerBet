using Dapper;
using SoccerBet.Business.Interfaces;
using SoccerBet.Business.Models;
using SoccerBet.Data.Interfaces;
using System;
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
    }
}
