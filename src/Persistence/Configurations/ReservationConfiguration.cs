using Domain.Management.Offices;
using Domain.Sales.Reservations;
using Domain.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations;
internal class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable(TableNames.Reservations, SchemaNames.Sales);

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

        builder.Property(x=> x.RentalPrice)
            .IsRequired(true)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.TotalPrice)
            .IsRequired(true)
            .HasColumnType("decimal(18,2)");

        builder.HasOne<Office>(x => x.PickUpOffice)
            .WithMany()
            .HasForeignKey(x => x.PickUpOfficeId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne<Office>(x => x.DropDownOffice)
            .WithMany()
            .HasForeignKey(x => x.DropDownOfficeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.ReservationItems)
            .WithOne()
            .HasForeignKey(x => x.ReservationId);



    }
}
