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
            var betlist = new List<Bet>();
            string sql = @$"SELECT b.Id,b.UserId,b.MatchId,b.HomeScoreBoard,b.AwayScoreBoard,b.CreatedAt,b.HitBet,
                            m.HomeTeam,
                            m.AwayTeam
                            from Bet b
                            inner join Matchs m
                            on m.Id = b.MatchId
                            WHERE b.userid = @userId";

            using (var connectionDb = _connection.Connection())
            {
                connectionDb.Open();
                var queryResult = await connectionDb.QueryAsync<QueryResult>(sql, new { UserId = userId });
                if(queryResult.Any())
                {
                    return ConvertToBusinessModel(queryResult.ToList());
                }

                return betlist;
            }
        }

        public async Task UpdateBet(Guid id, bool hitBet)
        {
            string sql = "Update Bet set HitBet = @hitbet where Id = @id";

            using(var connectionDb = _connection.Connection())
            {
                connectionDb.Open();

                await connectionDb.ExecuteAsync(sql, new { HitBet = hitBet, Id = id });
            }
        }

        private List<Bet> ConvertToBusinessModel(List<QueryResult> queryResult)
        {
            var listBet = new List<Bet>();
            foreach(var item in queryResult)
            {
                var bet = new Bet()
                {
                    Id = item.Id,
                    HomeScoreBoard = item.HomeScoreBoard,
                    AwayScoreBoard = item.AwayScoreBoard,
                    HitBet = item.HitBet,
                    MatchId = item.MatchId,
                    UserId = item.UserId,
                    Match = new Match()
                    {
                        HomeTeam = item.HomeTeam,
                        AwayTeam = item.AwayTeam
                    }
                };

                listBet.Add(bet);
            }

            return listBet;

        }
    }

    public class QueryResult
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid MatchId { get; set; }
        public int HomeScoreBoard { get; set; }
        public int AwayScoreBoard { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool? HitBet { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
    }

    
}
