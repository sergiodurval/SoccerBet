using Microsoft.Extensions.Hosting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SoccerBet.Extractor.Interfaces;
using SoccerBet.Extractor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerBet.Extractor
{
    public sealed class Extraction : IHostedService
    {
        private string Url { get; set; }
        private IWebDriver driver;
        private readonly IDataConsistency _dataConsistency;
        private List<LeagueExtractModel> Leagues { get; set; }
        public Extraction(IDataConsistency dataConsistency)
        {
            Url = ExtractConfiguration.Url;
            Leagues = ExtractConfiguration.Leagues;
            _dataConsistency = dataConsistency;
        }

        public void ExtractMatch()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            
            foreach(var league in Leagues)
            {
                var formattedUrl = $"{Url}{league.Country}/{league.Name}/calendario/";
                driver.Navigate().GoToUrl(formattedUrl);
                league.Rounds = ExtractRounds();
            }

            Thread.Sleep(1000);
            driver.Quit();

            _dataConsistency.ConsistencyRule(Leagues);
        }

        public List<RoundExtractModel> ExtractRounds()
        {
            var roundsQuantity = GetRounds();
            var rounds = new List<RoundExtractModel>();

            foreach(var indice in roundsQuantity)
            {
                var round = new RoundExtractModel();
                round.RoundNumber = indice;
                var matchs = new List<MatchExtractModel>();
                var roundsHtmlElement = driver.FindElements(By.CssSelector("div[class='event__round event__round--static']"));
                IWebElement currentRoundElement = roundsHtmlElement.Where(x => x.Text.Contains(indice.ToString())).FirstOrDefault();
                IWebElement nextElement = currentRoundElement.FindElement(By.XPath("following-sibling::*"));
                IWebElement eventTime = nextElement.FindElement(By.CssSelector("div[class='event__time']"));
                IWebElement homeTeam = nextElement.FindElement(By.CssSelector("div[class='event__participant event__participant--home']"));
                IWebElement awayTeam = nextElement.FindElement(By.CssSelector("div[class='event__participant event__participant--away']"));

                var match = new MatchExtractModel();
                match.MatchDate = GetEventTime(eventTime);
                match.HomeTeam = GetTeam(homeTeam);
                match.AwayTeam = GetTeam(awayTeam);
                matchs.Add(match);


                if (IsLastMatch(nextElement))
                {
                    round.Matchs = matchs;
                    rounds.Add(round);
                    continue;
                }
                else
                {
                    round.Matchs = matchs;
                    round.Matchs.AddRange(GetNextMatch(nextElement));
                    rounds.Add(round);
                }
            }

            return rounds;
        }

        public List<MatchExtractModel> GetNextMatch(IWebElement element)
        {
            List<MatchExtractModel> matchs = new List<MatchExtractModel>();
            bool hasNextMatch = false;
            IWebElement currentElement = GetNexElement(element);
            while(!hasNextMatch)
            {
                IWebElement nextElement = currentElement;
                IWebElement eventTime = currentElement.FindElement(By.CssSelector("div[class='event__time']"));
                IWebElement homeTeam = currentElement.FindElement(By.CssSelector("div[class='event__participant event__participant--home']"));
                IWebElement awayTeam = currentElement.FindElement(By.CssSelector("div[class='event__participant event__participant--away']"));

                var match = new MatchExtractModel()
                {
                    MatchDate = GetEventTime(eventTime),
                    HomeTeam = GetTeam(homeTeam),
                    AwayTeam = GetTeam(awayTeam)
                };

                matchs.Add(match);

                if(IsLastMatch(nextElement))
                {
                    break;
                }
                else
                {
                    currentElement = GetNexElement(nextElement);
                }
            }

            return matchs;
            
        }

        public IWebElement GetNexElement(IWebElement currentElement)
        {
            IWebElement nextElement = currentElement.FindElement(By.XPath("following-sibling::*"));
            return nextElement;
        }

        public List<int> GetRounds()
        {
            var roundsHtmlElement = driver.FindElements(By.CssSelector("div[class='event__round event__round--static']"));
            var listRounds = new List<int>();

            foreach (var round in roundsHtmlElement)
            {
                int roundNumber = Convert.ToInt32(round.Text.Remove(0, 6).Trim());
                listRounds.Add(roundNumber);
            }

            return listRounds;
        }

        public DateTime GetEventTime(IWebElement element)
        {
            string eventTime = string.Empty;

            try
            {
               eventTime = element.FindElement(By.ClassName("lineThrough")).Text;
            }
            catch (NoSuchElementException ex)
            {
                eventTime = element.Text;
            }
            
            return FormatDate(eventTime);
        }

        public TeamExtractModel GetTeam(IWebElement teamElement)
        {
            var team = new TeamExtractModel
            {
                Name = teamElement.Text
            };

            return team;
        }

        public bool IsLastMatch(IWebElement element)
        {
            string classAttribute = element.GetAttribute("class");
            return String.Equals(classAttribute.Trim(), "event__match event__match--static event__match--oneLine event__match--scheduled event__match--last");
        }

        public DateTime FormatDate(string matchDate)
        {
            int lastIndex = matchDate.LastIndexOf('.');
            string date = matchDate.Substring(0, lastIndex) + $".{DateTime.Now.Year}";
            string hour = matchDate.Remove(0, lastIndex).Replace('.',' ').Trim();
            string dateComplete = $"{date} { hour}";
            DateTime dateFormatted = DateTime.ParseExact(dateComplete, "dd.MM.yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            return dateFormatted;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                ExtractMatch();
                Console.WriteLine($"Término da extração{DateTime.Now}");
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu o seguinte erro: " + ex);
                StartAsync(cancellationToken);
                throw;
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
