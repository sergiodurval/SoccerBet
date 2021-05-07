using Microsoft.Extensions.Configuration;
using SoccerBet.Extractor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace SoccerBet.Extractor
{
    public static class ExtractConfiguration
    {
        static IConfiguration Config = new ConfigurationBuilder()
            .AddJsonFile(GetRootPath("appsettings.json"))
            .Build();


        public static string Url
        {
            get
            {
                return Config.GetSection("ExtractConfiguration")["Url"];
            }
        }

        public static List<LeagueExtractModel> Leagues
        {
            get 
            {
                return GetLeagues();
            }
        }

        


        public static string GetRootPath(string rootFileName)
        {
            string root;
            var rootDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            Regex matchPath = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = matchPath.Match(rootDir).Value;
            root = Path.Combine(appRoot, rootFileName);

            return root;
        }

        public static List<LeagueExtractModel> GetLeagues()
        {
            var leagues = Config.GetSection("ExtractConfiguration:League").Get<List<LeagueExtractModel>>();
            return leagues;
        }
    }
}
