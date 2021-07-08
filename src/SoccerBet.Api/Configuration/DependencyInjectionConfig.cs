using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SoccerBet.Business.Interfaces;
using SoccerBet.Business.Notifications;
using SoccerBet.Business.Services;
using SoccerBet.Data.Connection;
using SoccerBet.Data.Interfaces;
using SoccerBet.Data.Repository;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SoccerBet.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IConnectionFactory, DefaultSqlConnectionFactory>();
            services.AddScoped<ILeagueRepository, LeagueRepository>();
            services.AddScoped<ILeagueService, LeagueService>();
            services.AddScoped<INotification, Notifier>();
            services.AddScoped<IBetRepository, BetRepository>();
            services.AddScoped<IBetService, BetService>();
            services.AddScoped<IMatchRepository, MatchRepository>();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }
    }
}
