using Domain.Car;
using Domain.CarBrand;
using Domain.CarBrand.Entities;
using Domain.Color;
using Domain.Office;
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
