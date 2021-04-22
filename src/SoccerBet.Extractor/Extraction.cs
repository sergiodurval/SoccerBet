using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SoccerBet.Extractor.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace SoccerBet.Extractor
{
    public class Extraction
    {
        public string Url { get; set; }
        private IWebDriver driver;
        public Extraction()
        {
            var appSettings = ConfigurationManager.AppSettings;
            Url = appSettings["URL"];
        }

        public void OpenSite()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(Url);
            ExtractMatchs();
            Thread.Sleep(1000);
            driver.Quit();
        }

        public void ExtractMatchs()
        {
            var roundsQuantity = GetRounds();
            var rounds = new List<Round>();

            for(int i = roundsQuantity[0]; i <= roundsQuantity[roundsQuantity.Count - 1]; i++)
            {
                var round = new Round();
                round.RoundNumber = i;
                var matchs = new List<Match>();
                var roundsHtmlElement = driver.FindElements(By.CssSelector("div[class='event__round event__round--static']"));
                IWebElement currentRoundElement = roundsHtmlElement.Where(x => x.Text.Contains(i.ToString())).FirstOrDefault();
                IWebElement nextElement = currentRoundElement.FindElement(By.XPath("following-sibling::*"));
                IWebElement eventTime = nextElement.FindElement(By.CssSelector("div[class='event__time']"));
                IWebElement homeTeam = nextElement.FindElement(By.CssSelector("div[class='event__participant event__participant--home']"));
                IWebElement awayTeam = nextElement.FindElement(By.CssSelector("div[class='event__participant event__participant--away']"));

                var match = new Match();
                match.MatchDate = GetEventTime(eventTime);
                match.Teams = GetTeams(homeTeam, awayTeam);
                matchs.Add(match);
                rounds.Add(round);

                
            }
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

        public List<Teams> GetTeams(IWebElement homeTeam , IWebElement awayTeam)
        {
            var teams = new List<Teams>()
            {
                new Teams{Name = homeTeam.Text},
                new Teams{Name = awayTeam.Text}
            };

            return teams;
        }

        public bool IsLastMatch(IWebElement element)
        {
            string classAttribute = element.GetAttribute("class");
            return String.Equals(classAttribute.Trim(), "event__match event__match--static event__match--oneLine event__match--last");
        }
    }
}
