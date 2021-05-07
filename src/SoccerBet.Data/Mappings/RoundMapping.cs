using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerBet.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerBet.Data.Mappings
{
    public class RoundMapping : IEntityTypeConfiguration<Round>
    {
        public void Configure(EntityTypeBuilder<Round> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Number)
                .IsRequired()
                .HasColumnType("int");

            builder.HasMany(f => f.Matchs)
                .WithOne(p => p.Round)
                .HasForeignKey(p => p.RoundId);

            builder.ToTable("Rounds");
        }
    }
}
