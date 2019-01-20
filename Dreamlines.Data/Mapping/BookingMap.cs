using Dreamlines.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dreamlines.Data.Mapping
{
    public partial class BookingMap : EntityTypeConfiguration<Booking>
    {
        public override void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable(nameof(Booking));
            builder.HasKey(booking => booking.Id);
            builder.Property(booking => booking.Id).ValueGeneratedNever();
            builder.HasOne(booking => booking.Ship)
                .WithMany(ship => ship.Bookings)
                .HasForeignKey(foreignkey => foreignkey.ShipId)
                .OnDelete(DeleteBehavior.Restrict);

            base.Configure(builder);
        }
    }
}
