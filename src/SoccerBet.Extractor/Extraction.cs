using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SoccerBet.Extractor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SoccerBet.Extractor
{
    public class Extraction
    {
        private string Url { get; set; }
        private IWebDriver driver;
        private List<LeagueExtractModel> Leagues { get; set; }
        public Extraction()
        {
            Url = ExtractConfiguration.Url;
            Leagues = ExtractConfiguration.Leagues;
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

        }

        public List<RoundExtractModel> ExtractRounds()
        {
            var roundsQuantity = GetRounds();
            var rounds = new List<RoundExtractModel>();

            for(int i = roundsQuantity[0]; i <= roundsQuantity[roundsQuantity.Count - 1]; i++)
            {
                var round = new RoundExtractModel();
                round.RoundNumber = i;
                var matchs = new List<MatchExtractModel>();
                var roundsHtmlElement = driver.FindElements(By.CssSelector("div[class='event__round event__round--static']"));
                IWebElement currentRoundElement = roundsHtmlElement.Where(x => x.Text.Contains(i.ToString())).FirstOrDefault();
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

        public string GetEventTime(IWebElement element)
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

            return eventTime;
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
    }
}
