using Domain.CarCategory;
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
            .HasMaxLength(15)
            .IsRequired(true);

        builder.Property(x => x.ShortName)
            .HasMaxLength(6)
            .IsRequired(true);

        builder.Property(x => x.Description)
            .HasMaxLength(60)
            .IsRequired(true);
    }
}
