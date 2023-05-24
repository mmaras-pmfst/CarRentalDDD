using Domain.CarBrand.Entities;
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

internal class ReservationContractConfiguration : IEntityTypeConfiguration<ReservationContract>
{
    public void Configure(EntityTypeBuilder<ReservationContract> builder)
    {
        builder.ToTable(TableNames.ReservationContracts, SchemaNames.Data);

        builder.HasKey(r => r.Id);

        builder.Property(r => r.PickUpDate)
            .HasColumnType("datetime2")
            .IsRequired(true);

        builder.Property(r => r.DropDownDate)
            .HasColumnType("datetime2")
            .IsRequired(true);

        builder.Property(r => r.TotalPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired(true);

        builder.HasOne<Office>()
            .WithMany()
            .HasForeignKey(x => x.PickUpLocationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Office>()
            .WithMany()
            .HasForeignKey(x => x.DropDownLocationId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
