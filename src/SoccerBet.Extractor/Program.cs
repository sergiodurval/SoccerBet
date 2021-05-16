using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using SoccerBet.Extractor.Extensions;

namespace SoccerBet.Extractor
{
    class Program
    {
        
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await host.RunAsync();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseStartup<Startup>();

    }
}
