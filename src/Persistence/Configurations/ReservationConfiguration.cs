using Domain.Management.Offices;
using Domain.Sales.Reservations;
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
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(x => x.DriverLastName)
            .HasMaxLength(50)
            .IsRequired(true);

        builder.Property(x => x.Email)
            .IsRequired(true)
            .HasMaxLength(50);

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

        builder.HasOne<Office>()
            .WithMany()
            .HasForeignKey(x => x.PickUpLocationId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne<Office>()
            .WithMany()
            .HasForeignKey(x => x.DropDownLocationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.ReservationItems)
            .WithOne()
            .HasForeignKey(x => x.ReservationId);



    }
}
