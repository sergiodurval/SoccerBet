using Bogus;
using SoccerBet.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoccerBet.Test.Builders
{
    public class LeagueBuilder
    {
        Faker fake;
        private readonly League _league;
        public LeagueBuilder()
        {
            fake = new Faker();
        }

        public static LeagueBuilder New()
        {
            return new LeagueBuilder();
        }
        public List<League> ListLeagues()
        {
            var leagues = new List<League>();
            int leagueCount = 4;//fake.Random.Number(1, 4);

            for(int i = 0; i <= leagueCount; i++)
            {
                var league = GenerateLeague();
                var round = GenerateRound(league.Id);
                var match = GenerateMatch(league.Id, round);
                var matchs = new List<Match>();
                matchs.Add(match);
                var rounds = new List<Round>();
                rounds.Add(round);
                league.Rounds = rounds;
                league.Matchs = matchs;
                leagues.Add(league);
            }

            return leagues;
        }

        public Round GenerateRound(Guid leagueId)
        {
            var round = new Round()
            {
                Id = Guid.NewGuid(),
                LeagueId = leagueId,
                Number = fake.Random.Number()
            };

            return round;
        }

        public League GenerateLeague()
        {
            var league = new League()
            {
                Id = Guid.NewGuid(),
                Country = fake.Random.Word(),
                Name = fake.Name.Random.Word()
            };

            return league;
        }

        public Match GenerateMatch(Guid leagueId , Round round)
        {
            var match = new Match()
            {
                Id = Guid.NewGuid(),
                LeagueId = leagueId,
                Round = round,
                AwayScoreBoard = fake.Random.Number(),
                HomeScoreBoard = fake.Random.Number(),
                AwayTeam = fake.Name.Random.Word(),
                HomeTeam = fake.Name.Random.Word(),
                MatchDate = fake.Date.Soon()
            };

            return match;
        }

        public List<League> Build()
        {
            return ListLeagues();
        }
    }
}
