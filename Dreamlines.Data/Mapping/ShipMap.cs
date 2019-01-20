using Dreamlines.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dreamlines.Data.Mapping
{
    public partial class ShipMap : EntityTypeConfiguration<Ship>
    {
        public override void Configure(EntityTypeBuilder<Ship> builder)
        {
            builder.ToTable(nameof(Ship));
            builder.HasKey(ship => ship.Id);
            builder.Property(ship => ship.Name).IsRequired().HasMaxLength(100);
            builder.Property(salesunit => salesunit.Id).ValueGeneratedNever();

            builder.HasMany(ship => ship.Bookings).WithOne(booking => booking.Ship)
                .HasForeignKey(foreignkey => foreignkey.ShipId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(ship => ship.SalesUnit)
                .WithMany(salesunit => salesunit.Ships)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(foreignkey => foreignkey.SalesUnitId);

            base.Configure(builder);
        }
    }
}
