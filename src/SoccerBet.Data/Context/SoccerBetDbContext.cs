using Microsoft.EntityFrameworkCore;
using SoccerBet.Business.Models;
using System.Linq;

namespace SoccerBet.Data.Context
{
    public class SoccerBetDbContext : DbContext
    {
        public SoccerBetDbContext(DbContextOptions<SoccerBetDbContext> options) : base(options) { }

        public DbSet<League> League { get; set; }
        public DbSet<Round> Round { get; set; }
        public DbSet<Match> Match { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SoccerBetDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

    }
}
