using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace SoccerBet.Data.Context
{
    public class SoccerBetContextFactory : IDesignTimeDbContextFactory<SoccerBetDbContext>
    {
        public SoccerBetDbContext CreateDbContext(string[] args)
        {
             IConfiguration Config = new ConfigurationBuilder()
            .AddJsonFile(GetRootPath("appsettings.json"))
            .Build();

            var optionsBuilder = new DbContextOptionsBuilder<SoccerBetDbContext>();
            optionsBuilder.UseSqlServer(Config.GetConnectionString("DefaultConnection"));

            return new SoccerBetDbContext(optionsBuilder.Options);
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
