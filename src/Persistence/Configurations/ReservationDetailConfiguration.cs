using Domain.Sales.Extras;
using Domain.Sales.Reservations.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations;
internal class ReservationDetailConfiguration : IEntityTypeConfiguration<ReservationItem>
{
    public void Configure(EntityTypeBuilder<ReservationItem> builder)
    {
        builder.ToTable(TableNames.ReservationDetails, SchemaNames.Sales);

        builder.HasKey(t => t.Id);

        builder.Property(x => x.Quantity)
            .IsRequired(true)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired(true);

        builder.HasOne<Extra>()
            .WithMany()
            .HasForeignKey(x => x.ExtrasId);


    }
}
