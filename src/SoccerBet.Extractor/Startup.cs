using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SoccerBet.Business.Interfaces;
using SoccerBet.Data.Context;
using SoccerBet.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

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

            IConfiguration Configuration = new ConfigurationBuilder()
           .AddJsonFile(ExtractConfiguration.GetRootPath("appsettings.json"))
           .Build();

            services.AddTransient<IDataConsistency, DataConsistency>();
            services.AddHostedService<Extraction>();
            services.AddTransient<IMatchRepository, MatchRepository>();
            services.AddTransient<ILeagueRepository, LeagueRepository>();
            services.AddTransient<IRoundRepository, RoundRepository>();
            services.AddDbContext<SoccerBetDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }, ServiceLifetime.Transient);
            services.AddAutoMapper(typeof(DataConsistency).Assembly);
        }
    }
}
