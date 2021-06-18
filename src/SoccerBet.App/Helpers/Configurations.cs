using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SoccerBet.App.Helpers
{
    public static class Configurations
    {
        static IConfiguration Config = new ConfigurationBuilder()
            .AddJsonFile(GetRootPath("appsettings.json"))
            .Build();


        public static string ApiUrl
        {
            get
            {
                return Config.GetSection("Api")["Url"];
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
    }
}
