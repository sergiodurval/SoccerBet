using SoccerBet.Data.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace SoccerBet.Data.Connection
{
    public class DefaultSqlConnectionFactory : IConnectionFactory
    {
        public IDbConnection Connection()
        {
            IConfiguration Config = new ConfigurationBuilder()
            .AddJsonFile(GetRootPath("appsettings.json"))
            .Build();

            return new SqlConnection(Config.GetConnectionString("DefaultConnection"));
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
