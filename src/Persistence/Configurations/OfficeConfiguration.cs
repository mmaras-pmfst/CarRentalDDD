using Domain.Management.Offices;
using Domain.Management.Offices.ValueObjects;
using Domain.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations;

internal class OfficeConfiguration : IEntityTypeConfiguration<Office>
{
    public void Configure(EntityTypeBuilder<Office> builder)
    {
        builder.ToTable(TableNames.Offices, SchemaNames.Catalog);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Address)
            .HasConversion(
                x => new {x.City, x.StreetName, x.StreetNumber, x.Country},
                v => Address.Create(
                    v.City,
                    v.StreetName,
                    v.StreetNumber,
                    v.Country
                    ).Value
            )
            .IsRequired(true);

        builder.Property(x => x.PhoneNumber)
            .HasConversion(x => x.Value, v => PhoneNumber.Create(v).Value)
            .IsRequired();

        builder.Property(x => x.OpeningTime)
            .HasColumnType("datetime2")
            .IsRequired(false);

        builder.Property(x => x.ClosingTime)
            .HasColumnType("datetime2")
            .IsRequired(false);

        builder.HasMany(x => x.Workers)
            .WithOne()
            .HasForeignKey(x => x.OfficeId);
    }
}
