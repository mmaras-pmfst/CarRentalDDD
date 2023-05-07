using Domain.Office;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    internal class OfficeConfiguration : IEntityTypeConfiguration<Office>
    {
        public void Configure(EntityTypeBuilder<Office> builder)
        {
            builder.ToTable(TableNames.Offices, SchemaNames.Catalog);
            builder.HasKey(x => x.Id);

            builder.Property(x => x.City)
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(x => x.Country)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.StreetName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.StreetNumber)
                .HasMaxLength(3)
                .IsRequired();

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(x => x.OpeningTime)
                .HasColumnType("datetime2")
                .IsRequired(false);

            builder.Property(x => x.ClosingTime)
                .HasColumnType("datetime2")
                .IsRequired(false);
        }
    }
}
