using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SoccerBet.Data.Context;
using Microsoft.EntityFrameworkCore;
using SoccerBet.Business.Interfaces;
using SoccerBet.Data.Repository;

namespace SoccerBet.Extractor
{
    class Program
    {
        static void Main(string[] args)
        {
            BuildConfig();
            var extraction = new Extraction();
            extraction.ExtractMatch();
        }

        static void BuildConfig()
        {
            IConfiguration Config = new ConfigurationBuilder()
           .AddJsonFile(ExtractConfiguration.GetRootPath("appsettings.json"))
           .Build();

            var services = new ServiceCollection()
                .AddScoped<IMatchRepository, MatchRepository>()
                .AddScoped<ILeagueRepository, LeagueRepository>()
                .AddScoped<IRoundRepository, RoundRepository>();
                

            services.AddDbContext<SoccerBetDbContext>(options =>
            {
                options.UseSqlServer(Config.GetConnectionString("DefaultConnection"));
            });
        }


    }
}
