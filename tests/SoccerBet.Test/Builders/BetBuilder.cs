using Bogus;
using SoccerBet.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerBet.Test.Builders
{
    public class BetBuilder
    {
        Faker fake;

        public BetBuilder()
        {
            fake = new Faker();
        }

        public static BetBuilder New()
        {
            return new BetBuilder();
        }

        public Bet GenerateBet()
        {
            var bet = new Bet()
            {
                UserId = Guid.NewGuid().ToString(),
                MatchId = Guid.NewGuid(),
                HomeScoreBoard = fake.Random.Number(0, 10),
                AwayScoreBoard = fake.Random.Number(0, 10)
            };

            return bet;
        }

        public Bet GenerateInvalidBet()
        {
            var bet = new Bet()
            {
                MatchId = Guid.NewGuid(),
                HomeScoreBoard = fake.Random.Number(0, 10),
                AwayScoreBoard = fake.Random.Number(0, 10)
            };

            return bet;
        }

        public List<Bet> GenerateBetList()
        {
            var listBet = new List<Bet>()
            {
                new Bet
                {
                    MatchId = Guid.NewGuid(), 
                    HomeScoreBoard = fake.Random.Number(0,10), 
                    AwayScoreBoard = fake.Random.Number(0,10)
                }
            };

            return listBet;
        }

        public Bet Build()
        {
            return GenerateBet();
        }
    }
}
