using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SoccerBet.Api.Configuration;
using SoccerBet.Business.Interfaces;
using SoccerBet.Business.Notifications;
using SoccerBet.Business.Services;
using SoccerBet.Data.Connection;
using SoccerBet.Data.Interfaces;
using SoccerBet.Data.Repository;

namespace SoccerBet.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IConnectionFactory, DefaultSqlConnectionFactory>();
            services.AddScoped<ILeagueRepository, LeagueRepository>();
            services.AddScoped<ILeagueService, LeagueService>();
            services.AddScoped<INotification, Notifier>();
            services.AddScoped<IBetRepository, BetRepository>();
            services.AddScoped<IBetService, BetService>();
            services.AddIdentityConfiguration(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
