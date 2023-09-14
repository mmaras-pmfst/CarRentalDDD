using Domain.Management.Cars;
using Domain.Management.Offices;
using Domain.Management.Workers;
using Domain.Sales.Contracts;
using Domain.Sales.Reservations;
using Domain.Sales.Reservations.ValueObjects;
using Domain.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
           .HasConversion(x => x.Value, v => FirstName.Create(v).Value)
           .HasMaxLength(FirstName.MaxLength)
           .IsRequired(true);

        builder.Property(x => x.DriverLastName)
           .HasConversion(x => x.Value, v => LastName.Create(v).Value)
           .HasMaxLength(LastName.MaxLength)
           .IsRequired(true);

        builder.Property(x => x.Email)
           .HasConversion(x => x.Value, v => Email.Create(v).Value)
           .IsRequired(true);

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

        //v2
        //builder.Property(x => x.Card)
        //    .HasConversion(
        //        v => new {v.CardName,v.CardNumber, v.CVV,v.CardDateExpiration, v.CardYearExpiration},
        //        v => Card.Create(
        //            v.CardName,
        //            v.CardNumber,
        //            v.CVV,
        //            v.CardDateExpiration,
        //            v.CardYearExpiration).Value
        //    )
        //    .IsRequired(false);

        //v1:
        builder.OwnsOne(x => x.Card, card =>
        {
            card.Property(x => x.CardName)
            .HasColumnName("CardName")
            .IsRequired(false);

            card.Property(x => x.CardNumber)
            .HasColumnName("CardNumber")
            .IsRequired(false);

            card.Property(x => x.CVV)
            .HasColumnName("CVV")
            .IsRequired(false);

            card.Property(x => x.CardDateExpiration)
            .HasColumnName("CardDateExpiration")
            .IsRequired(false);

            card.Property(x => x.CardYearExpiration)
            .HasColumnName("CardYearExpiration")
            .IsRequired(false);
        });


        builder.HasOne(x => x.Reservation)
            .WithOne(x => x.Contract)
            .HasForeignKey<Contract>(x => x.ReservationId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne<Worker>(x => x.Worker)
            .WithMany(x => x.Contracts)
            .HasForeignKey(x => x.WorkerId)
            .OnDelete(DeleteBehavior.Restrict);



        builder.HasOne<Office>(x => x.PickUpOffice)
            .WithMany()
            .HasForeignKey(x => x.PickUpOfficeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Office>(x => x.DropDownOffice)
            .WithMany()
            .HasForeignKey(x => x.DropDownOfficeId)
            .OnDelete(DeleteBehavior.Restrict);
        

        builder.HasOne<Car>(x => x.Car)
            .WithMany(x => x.Contracts)
            .HasForeignKey(x => x.CarId)
            .OnDelete(DeleteBehavior.Restrict);


    }
}
