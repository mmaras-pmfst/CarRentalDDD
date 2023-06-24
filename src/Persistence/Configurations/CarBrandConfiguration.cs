using Domain.Management.CarBrand;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations;

internal class CarBrandConfiguration : IEntityTypeConfiguration<CarBrand>
{
    public void Configure(EntityTypeBuilder<CarBrand> builder)
    {
        builder.ToTable(TableNames.CarBrands, SchemaNames.Catalog);

        builder.HasKey(c => c.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(15)
            .IsRequired(true);

        builder.HasMany(x => x.CarModels)
            .WithOne()
            .HasForeignKey(x => x.CarBrandId);
    }
}
