using Domain.Management.Car;
using Domain.Management.Office;
using Domain.Management.Office.Entities;
using Domain.Sales.CarModelRent.Entities;
using Domain.Sales.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations;
internal class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.ToTable(TableNames.Contracts, SchemaNames.Sales);

        builder.HasKey(x => x.Id);


        builder.Property(x => x.DriverFirstName)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(x => x.DriverLastName)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(x => x.Email)
            .IsRequired(true)
            .HasMaxLength(50);
        builder.Property(x => x.PickUpDate)
            .IsRequired(true);
        builder.Property(x => x.DropDownDate)
            .IsRequired(true);

        builder.Property(x => x.RentalPrice)
            .IsRequired(true)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.TotalPrice)
            .IsRequired(true)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.DriverLicenceNumber)
            .IsRequired(true)
            .HasMaxLength(20);

        builder.Property(x => x.DriverIdentificationNumber)
            .IsRequired(true)
            .HasMaxLength(20);

        builder.Property(r => r.CardType)
            .HasConversion<string>()
            .IsRequired(false);

        builder.Property(r => r.PaymentMethod)
            .HasConversion<string>()
            .IsRequired(false);

        builder.Property(x => x.CardName)
            .IsRequired(false)
            .HasMaxLength(50);

        builder.Property(x => x.CardNumber)
            .IsRequired(false)
            .HasMaxLength(15);

        builder.Property(x => x.CVV)
            .IsRequired(false);

        builder.Property(x => x.CardDateExpiration)
            .HasMaxLength(2)
            .IsRequired(false);

        builder.Property(x => x.CardYearExpiration)
            .HasMaxLength(2)
            .IsRequired(false);


        builder.HasOne<Reservation>()
            .WithMany()
            .HasForeignKey(x => x.ReservationId);

        builder.HasOne<Worker>()
            .WithMany()
            .HasForeignKey(x => x.WorkerId);


        builder.HasOne<Office>()
            .WithMany()
            .HasForeignKey(x => x.PickUpLocationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Office>()
            .WithMany()
            .HasForeignKey(x => x.DropDownLocationId)
            .OnDelete(DeleteBehavior.Restrict);
        ;

        builder.HasOne<Car>()
            .WithMany()
            .HasForeignKey(x => x.CarId);

    }
}
