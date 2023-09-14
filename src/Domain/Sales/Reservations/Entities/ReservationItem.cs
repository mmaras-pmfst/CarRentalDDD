using Domain.Common.Models;
using Domain.Sales.Extras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Sales.Reservations.Entities;

public sealed class ReservationItem : Entity
{
    public decimal Quantity { get; private set; }
    public decimal Price { get; private set; }
    public Guid ExtrasId { get; private set; }
    public Guid ReservationId { get; private set; }

    public Extra Extra { get; private set; }

    private ReservationItem()
    {

    }
    private ReservationItem(Guid id, decimal quantity, decimal price, Guid extrasId, Guid reservationId)
        : base(id)
    {
        Quantity = quantity;
        Price = price;
        ExtrasId = extrasId;
        ReservationId = reservationId;
    }

    public static ReservationItem Create(Guid id, decimal quantity, Extras.Extra extras, Reservation reservation)
    {
        var duration = (decimal)reservation.DropDownDate.Subtract(reservation.PickUpDate).TotalDays;

        var price = duration * extras.PricePerDay * quantity;

        return new ReservationItem(id, quantity, price, extras.Id, reservation.Id);
    }
}
