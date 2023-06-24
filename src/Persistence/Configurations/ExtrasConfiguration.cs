using Domain.Sales.Extras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations;
internal class ExtrasConfiguration : IEntityTypeConfiguration<Extras>
{
    public void Configure(EntityTypeBuilder<Extras> builder)
    {
        builder.ToTable(TableNames.Extras, SchemaNames.Sales);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired(true);

        builder.Property(x => x.Description)
            .HasMaxLength(250)
            .IsRequired(false);

        builder.Property(x => x.PricePerDay)
            .HasColumnType("decimal(18,2)")
            .IsRequired(true);



    }
}
