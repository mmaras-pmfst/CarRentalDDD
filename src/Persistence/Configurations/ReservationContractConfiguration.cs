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

        builder.Property(r => r.DriverFirstName)
            .HasMaxLength(30)
            .IsRequired(true);

        builder.Property(r => r.DriverLastName)
            .HasMaxLength(30)
            .IsRequired(true);

        builder.Property(r => r.DriverLicenceNumber)
            .HasMaxLength(30)
            .IsRequired(false);

        builder.Property(r => r.DriverIdentificationNumber)
            .HasMaxLength(30)
            .IsRequired(false);

        builder.Property(r => r.CardType)
            .HasConversion<string>()
            .IsRequired(false);

        builder.Property(r => r.PaymentMethod)
            .HasConversion<string>()
            .IsRequired(false);

        builder.Property(r => r.CardName)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(r => r.CardNumber)
            .HasMaxLength(16)
            .IsRequired(false);

        builder.Property(r => r.CVV)
            .HasMaxLength(3)
            .IsRequired(false);

        builder.Property(r => r.CardDateExpiration)
            .HasMaxLength(2)
            .IsRequired(false);

        builder.Property(r => r.CardYearExpiration)
            .HasMaxLength(4)
            .IsRequired(false);

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
