using Dreamlines.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dreamlines.Data.Mapping
{
    public partial class SalesUnitMap : EntityTypeConfiguration<SalesUnit>
    {
        public override void Configure(EntityTypeBuilder<SalesUnit> builder)
        {
            builder.ToTable(nameof(SalesUnit));
            builder.HasKey(salesunit => salesunit.Id);
            builder.Property(salesunit => salesunit.Name).IsRequired().HasMaxLength(100);
            builder.Property(salesunit => salesunit.Country).IsRequired().HasMaxLength(50);
            builder.Property(salesunit => salesunit.Id).ValueGeneratedNever();
            
            builder.HasMany(salesunit => salesunit.Ships)
                .WithOne(ship => ship.SalesUnit)
                .HasForeignKey(foreignkey => foreignkey.SalesUnitId)
                .OnDelete(DeleteBehavior.Restrict);

            base.Configure(builder);
        }
    }
}
