using Domain.Car;
using Domain.Office;
using Domain.ReservationContract;
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

        builder.HasKey(x => x.Id);

        builder.Property(x => x.DriverFirstName)
            .HasMaxLength(30)
            .IsRequired(true);

        builder.Property(x => x.DriverLastName)
            .HasMaxLength(30)
            .IsRequired(true);

        builder.Property(x => x.Email)
            .HasMaxLength(40)
            .IsRequired(true);

        builder.Property(x => x.PickUpDate)
            .HasColumnType("datetime2")
            .IsRequired(true);

        builder.Property(x => x.DropDownDate)
            .HasColumnType("datetime2")
            .IsRequired(true);

        builder.Property(x => x.TotalPrice)
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

        builder.HasOne<Car>()
            .WithMany()
            .HasForeignKey(x => x.CarId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.ContractMade)
            .IsRequired(true);

        builder.Property(x => x.DriverLicenceNumber)
            .IsRequired(false);

        builder.Property(x => x.DriverIdentificationNumber)
            .IsRequired(false);

        builder.Property(r => r.CardType)
            .HasConversion<string>()
            .IsRequired(false);

        builder.Property(r => r.PaymentMethod)
            .HasConversion<string>()
            .IsRequired(false);

        builder.Property(x => x.CardName)
            .IsRequired(false);

        builder.Property(x => x.CardNumber)
            .IsRequired(false);

        builder.Property(x => x.CVV)
            .IsRequired(false);

        builder.Property(x => x.CardDateExpiration)
            .IsRequired(false);

        builder.Property(x => x.CardYearExpiration)
            .IsRequired(false);




    }
}
