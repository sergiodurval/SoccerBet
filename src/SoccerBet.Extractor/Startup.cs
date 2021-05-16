using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SoccerBet.Business.Interfaces;
using SoccerBet.Data.Connection;
using SoccerBet.Data.Interfaces;
using SoccerBet.Data.Repository;
using SoccerBet.Extractor.Interfaces;

namespace SoccerBet.Extractor
{
    public class Startup
    {
       public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConnectionFactory, DefaultSqlConnectionFactory>();
            services.AddSingleton<IDataConsistency, DataConsistency>();
            services.AddHostedService<Extraction>();
            services.AddSingleton<ILeagueRepository, LeagueRepository>();
            services.AddAutoMapper(typeof(DataConsistency).Assembly);
        }
    }
}
