using Dreamlines.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dreamlines.Data.Mapping
{
    //public partial class CountryMap : EntityTypeConfiguration<Country>
    //{
    //    public override void Configure(EntityTypeBuilder<Country> builder)
    //    {
    //        builder.ToTable(nameof(Country));
    //        builder.HasKey(country => country.Id);
    //        builder.Property(country => country.Name).IsRequired().HasMaxLength(50);

    //        builder.HasMany(country => country.SalesUnits).WithOne(salesunit => salesunit.Country)
    //            .HasForeignKey(foreignkey => foreignkey.CountryId)
    //            .OnDelete(DeleteBehavior.SetNull);

    //        base.Configure(builder);
    //    }
    //}
}
