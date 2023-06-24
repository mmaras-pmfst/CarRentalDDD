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
            .HasMaxLength(50)
            .IsRequired(true);

        builder.Property(x => x.LastName)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(15)
            .IsRequired(true);

        builder.Property(x => x.Email)
            .HasMaxLength(50)
            .IsRequired(true);

        builder.Property(x => x.PersonalIdentificationNumber)
            .HasMaxLength(50)
            .IsRequired(true);
    }
}
