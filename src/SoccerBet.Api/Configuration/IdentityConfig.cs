using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SoccerBet.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace SoccerBet.Api.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
