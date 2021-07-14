using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SoccerBet.Extractor.Interfaces;
using SoccerBet.Extractor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerBet.Extractor
{
    public class ExtractionResult
    {
        private string Url { get; set; }
        private IWebDriver driver;
        private readonly IDataValidate _dataValidate;
        private List<LeagueExtractModel> Leagues { get; set; }
        public ExtractionResult(IDataValidate dataValidate)
        {
            Url = ExtractConfiguration.Url;
            Leagues = ExtractConfiguration.Leagues;
            _dataValidate = dataValidate;
        }

        public async void ExtractResult()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            foreach (var league in Leagues)
            {
                var formattedUrl = $"{Url}{league.Country}/{league.Name}/resultados";
                driver.Navigate().GoToUrl(formattedUrl);
                if (HasTodayMatch())
                {
                    league.Rounds = ExtractTodayMatch();
                }

                league.Rounds.AddRange(await ExtractResultsRounds(league.Name));
                await UpdateMatchs(league);
            }

            Thread.Sleep(1000);
            driver.Quit();
            
        }

        public async Task<List<RoundExtractModel>> ExtractResultsRounds(string leagueName)
        {
            var rounds = await GetRounds(leagueName);

            if (rounds == null || rounds.Count == 0)
                return rounds;

            foreach(var round in rounds)
            {
                var matchs = new List<MatchExtractModel>();
                var roundsHtmlElement = driver.FindElements(By.CssSelector("div[class='event__round event__round--static']"));
                IWebElement currentRoundElement = roundsHtmlElement.Where(x => x.Text.Contains(round.RoundNumber.ToString())).FirstOrDefault();
                if (currentRoundElement == null)
                    continue;

                IWebElement nextElement = currentRoundElement.FindElement(By.XPath("following-sibling::*"));
                IWebElement eventTime = nextElement.FindElement(By.CssSelector("div[class='event__time']"));
                IWebElement homeTeam = GetHomeTeamElement(nextElement);
                IWebElement awayTeam = GetAwayTeamElement(nextElement);
                IWebElement eventScore = nextElement.FindElement(By.CssSelector("div[class='event__scores fontBold']"));

                var match = new MatchExtractModel();
                match.MatchDate = GetEventTime(eventTime);
                match.HomeTeam = GetTeam(homeTeam);
                match.HomeTeam.HomeScoreBoard = GetHomeScoreBoard(eventScore);
                match.AwayTeam = GetTeam(awayTeam);
                match.AwayTeam.AwayScoreBoard = GetAwayScoreBoard(eventScore);

                matchs.Add(match);


                if (IsLastMatch(nextElement))
                {
                    round.Matchs = matchs;
                    continue;
                }
                else
                {
                    round.Matchs = matchs;
                    round.Matchs.AddRange(GetNextMatch(nextElement));
                }

            }

            return rounds;
        }

        public async Task UpdateMatchs(LeagueExtractModel league)
        {
            if(league.Rounds != null && league.Rounds.Count > 0)
            {
                await _dataValidate.UpdateMatchs(league.Rounds);
            }            
        }

        public List<MatchExtractModel> GetNextMatch(IWebElement element)
        {
            List<MatchExtractModel> matchs = new List<MatchExtractModel>();
            bool hasNextMatch = false;
            IWebElement currentElement = GetNexElement(element);
            while (!hasNextMatch)
            {
                IWebElement nextElement = currentElement;
                IWebElement eventTime = currentElement.FindElement(By.CssSelector("div[class='event__time']"));
                IWebElement homeTeam = GetHomeTeamElement(currentElement);
                IWebElement awayTeam = GetAwayTeamElement(currentElement);
                IWebElement eventScore = nextElement.FindElement(By.CssSelector("div[class='event__scores fontBold']"));

                var match = new MatchExtractModel();
                match.MatchDate = GetEventTime(eventTime);
                match.HomeTeam = GetTeam(homeTeam);
                match.HomeTeam.HomeScoreBoard = GetHomeScoreBoard(eventScore);
                match.AwayTeam = GetTeam(awayTeam);
                match.AwayTeam.AwayScoreBoard = GetAwayScoreBoard(eventScore);

                matchs.Add(match);

                if (IsLastMatch(nextElement))
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


        public async Task<List<RoundExtractModel>> GetRounds(string leagueName)
        {
            var roundExtractModel = await _dataValidate.GetRoundByLeagueName(leagueName);
            return roundExtractModel;
        }


        public bool HasTodayMatch()
        {
            var eventStageElement = driver.FindElements(By.CssSelector("div[class='event__stage']"));
            var todayMatchElement = driver.FindElements(By.CssSelector("div[class='tabs__ear']"));

            if (todayMatchElement.Any(x => x.Text == "Jogos de hoje") && eventStageElement != null)
            {
                return true;
            }

            return false;
        }

        public List<RoundExtractModel> ExtractTodayMatch()
        {
            var roundsExtractModel = new List<RoundExtractModel>();
            var todayMatchs = driver.FindElements(By.CssSelector("div[class='event__match event__match--oneLine event__match--last ']"));
            var matchs = new List<MatchExtractModel>();

            foreach (var divMatchElement in todayMatchs)
            {
                IWebElement homeTeam = GetHomeTeamElement(divMatchElement);
                IWebElement awayTeam = GetAwayTeamElement(divMatchElement);
                IWebElement eventScore = divMatchElement.FindElement(By.CssSelector("div[class='event__scores fontBold']"));

                var match = new MatchExtractModel();
                match.HomeTeam = GetTeam(homeTeam);
                match.HomeTeam.HomeScoreBoard = GetHomeScoreBoard(eventScore);
                match.AwayTeam = GetTeam(awayTeam);
                match.AwayTeam.AwayScoreBoard = GetAwayScoreBoard(eventScore);

                Console.WriteLine($"{match.HomeTeam.Name} {match.HomeTeam.HomeScoreBoard} x {match.AwayTeam.AwayScoreBoard} {match.AwayTeam.Name}");

                matchs.Add(match);
            }

            var round = new RoundExtractModel();
            round.RoundNumber = 0;
            round.Matchs = matchs;
            roundsExtractModel.Add(round);
            return roundsExtractModel;
        }

        public TeamExtractModel GetTeam(IWebElement teamElement)
        {
            var team = new TeamExtractModel
            {
                Name = teamElement.Text
            };

            return team;
        }

        public int GetHomeScoreBoard(IWebElement eventScore)
        {
            var scores = eventScore.FindElements(By.TagName("span"));
            return Convert.ToInt32(scores[0].Text.Trim());
        }

        public int GetAwayScoreBoard(IWebElement eventScore)
        {
            var scores = eventScore.FindElements(By.TagName("span"));
            return Convert.ToInt32(scores[1].Text.Trim());
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

        public bool IsLastMatch(IWebElement element)
        {
            string classAttribute = element.GetAttribute("class");
            return String.Equals(classAttribute.Trim(), "event__match event__match--static event__match--oneLine event__match--last");
        }

        public DateTime FormatDate(string matchDate)
        {
            int lastIndex = matchDate.LastIndexOf('.');
            string date = matchDate.Substring(0, lastIndex) + $".{DateTime.Now.Year}";
            string hour = matchDate.Remove(0, lastIndex).Replace('.', ' ').Trim();
            string dateComplete = $"{date} { hour}";
            DateTime dateFormatted = DateTime.ParseExact(dateComplete, "dd.MM.yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            return dateFormatted;
        }

        public IWebElement GetHomeTeamElement(IWebElement currentElement)
        {
            IWebElement homeTeam;
            try
            {
                homeTeam = currentElement.FindElement(By.CssSelector("div[class='event__participant event__participant--home']"));
            }
            catch (NoSuchElementException ex)
            {
                homeTeam = currentElement.FindElement(By.CssSelector("div[class='event__participant event__participant--home fontBold']"));
                return homeTeam;
            }

            return homeTeam;
        }

        public IWebElement GetAwayTeamElement(IWebElement currentElement)
        {
            IWebElement awayTeam;
            try
            {
                awayTeam = currentElement.FindElement(By.CssSelector("div[class='event__participant event__participant--away']"));
            }
            catch (NoSuchElementException ex)
            {
                awayTeam = currentElement.FindElement(By.CssSelector("div[class='event__participant event__participant--away fontBold']"));
                return awayTeam;
            }

            return awayTeam;
        }
    }
}
