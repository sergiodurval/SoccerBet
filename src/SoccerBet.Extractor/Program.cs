using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SoccerBet.Data.Context;
using Microsoft.EntityFrameworkCore;

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
            var builder = new ConfigurationBuilder()
               .AddJsonFile($"appsettings.json", true, true);

            var config = builder.Build();

            var services = new ServiceCollection();

            services.AddDbContext<SoccerBetDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
        }


    }
}
