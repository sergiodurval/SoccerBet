using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerBet.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerBet.Data.Mappings
{
    public class MatchMapping : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.HomeTeam)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.AwayTeam)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.HomeScoreBoard)
                 .HasColumnType("int");

            builder.Property(c => c.AwayScoreBoard)
                .HasColumnType("int");

            builder.Property(c => c.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(c => c.MatchDate)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(c => c.UpdatedAt)
                .HasColumnType("datetime");

            builder.HasOne<Round>(s => s.Round)
                .WithMany(g => g.Matchs)
                .HasForeignKey(s => s.RoundId);

            builder.HasOne<League>(s => s.League)
                .WithMany(g => g.Matchs)
                .HasForeignKey(s => s.LeagueId);

            builder.ToTable("Matchs");

        }
    }
}
