using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerBet.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerBet.Data.Mappings
{
    public class LeagueMapping : IEntityTypeConfiguration<League>
    {
        public void Configure(EntityTypeBuilder<League> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Country)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.ToTable("League");
        }
    }
}
