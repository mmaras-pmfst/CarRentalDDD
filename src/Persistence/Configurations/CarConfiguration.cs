using Domain.Management.Car;
using Domain.Management.CarBrand.Entities;
using Domain.Management.Color;
using Domain.Management.Office;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations;

internal class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.ToTable(TableNames.Cars, SchemaNames.Catalog);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.NumberPlate)
            .HasMaxLength(10)
            .IsRequired(true);

        builder.Property(x => x.Name)
            .IsRequired(true)
            .HasMaxLength(20);

        builder.Property(x => x.Kilometers)
            .HasColumnType("decimal(18,1)")
            .IsRequired(true);

        builder.Property(x => x.Image)
            .IsRequired(false);

        builder.Property(r => r.Status)
            .HasConversion<string>()
            .IsRequired(true);

        builder.Property(r => r.FuelType)
            .HasConversion<string>()
            .IsRequired(true);

        builder.HasOne<CarModel>()
            .WithMany()
            .HasForeignKey(x => x.CarModelId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Color>()
            .WithMany()
            .HasForeignKey(x => x.ColorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Office>()
            .WithMany()
            .HasForeignKey(x => x.OfficeId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
