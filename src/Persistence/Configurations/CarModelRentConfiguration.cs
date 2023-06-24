using Domain.Management.CarBrand.Entities;
using Domain.Sales.CarModelRent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations;
internal class CarModelRentConfiguration : IEntityTypeConfiguration<CarModelRent>
{
    public void Configure(EntityTypeBuilder<CarModelRent> builder)
    {
        builder.ToTable(TableNames.CarModelRents, SchemaNames.Sales);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ValidFrom)
            .IsRequired(true);

        builder.Property(x => x.ValidUntil)
            .IsRequired(true);

        builder.Property(x => x.PricePerDay)
            .IsRequired(true)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.Discount)
            .IsRequired(true)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.IsVisible)
            .HasDefaultValue(false);

        //builder.HasOne<CarModel>()
        //    .WithMany()
        //    .HasForeignKey(x => x.CarModelId)
        //    .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Reservations)
            .WithOne()
            .HasForeignKey(x => x.CarModelRentId);
    }
}
