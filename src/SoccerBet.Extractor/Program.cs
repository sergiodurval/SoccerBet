using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SoccerBet.Data.Context;
using Microsoft.EntityFrameworkCore;
using SoccerBet.Business.Interfaces;
using SoccerBet.Data.Repository;
using AutoMapper;
using Microsoft.Extensions.Hosting;

namespace SoccerBet.Extractor
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var extraction = new Extraction();      
            extraction.ExtractMatch();
        }

        static void BuildConfig()
        {
            IConfiguration Config = new ConfigurationBuilder()
           .AddJsonFile(ExtractConfiguration.GetRootPath("appsettings.json"))
           .Build();
        }


    }
}
