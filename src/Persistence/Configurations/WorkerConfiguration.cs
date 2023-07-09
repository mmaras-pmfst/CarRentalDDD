using Domain.Common.ValueObjects;
using Domain.Management.CarBrand.ValueObjects;
using Domain.Management.Office;
using Domain.Management.Office.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations;
internal class WorkerConfiguration : IEntityTypeConfiguration<Worker>
{
    public void Configure(EntityTypeBuilder<Worker> builder)
    {
        builder.ToTable(TableNames.Workers, SchemaNames.Catalog);

        builder.HasKey(t => t.Id);
        builder.Property(x => x.FirstName)
            .HasConversion(x => x.Value, v => FirstName.Create(v).Value)
            .HasMaxLength(FirstName.MaxLength)
            .IsRequired(true);

        builder.Property(x => x.LastName)
            .HasConversion(x => x.Value, v => LastName.Create(v).Value)
            .IsRequired(true)
            .HasMaxLength(LastName.MaxLength);

        builder.Property(x => x.PhoneNumber)
            .HasConversion(x => x.Value, v => PhoneNumber.Create(v).Value)
            .IsRequired(true);

        builder.Property(x => x.Email)
            .HasConversion(x => x.Value, v => Email.Create(v).Value)
            .IsRequired(true);

        builder.Property(x => x.PersonalIdentificationNumber)
            .HasMaxLength(50)
            .IsRequired(true);
    }
}
