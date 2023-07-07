using Domain.Common.ValueObjects;
using Domain.Management.CarBrand.ValueObjects;
using Domain.Management.CarCategory;
using Domain.Management.CarCategory.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations;

internal class CarCategoryConfiguration : IEntityTypeConfiguration<CarCategory>
{
    public void Configure(EntityTypeBuilder<CarCategory> builder)
    {
        builder.ToTable(TableNames.CarCategories, SchemaNames.Catalog);

        builder.HasKey(c => c.Id);

        builder.Property(x => x.Name)
            .HasConversion(x => x.Value, v => CarCategoryName.Create(v).Value)
            .HasMaxLength(CarCategoryName.MaxLength)
            .IsRequired(true);

        builder.Property(x => x.ShortName)
            .HasConversion(x => x.Value, v => CarCategoryShortName.Create(v).Value)
            .HasMaxLength(CarCategoryShortName.MaxLength)
            .IsRequired(true);

        builder.Property(x => x.Description)
            .HasConversion(x => x.Value, v => Description.Create(v).Value)
            .HasMaxLength(Description.MaxLength)
            .IsRequired(true);
    }
}
