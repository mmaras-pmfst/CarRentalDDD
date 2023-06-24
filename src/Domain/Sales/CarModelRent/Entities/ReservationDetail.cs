using Domain.Common.Models;
using Domain.Sales.Contract.Entities;
using Domain.Sales.Extras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Sales.CarModelRent.Entities;
public sealed class ReservationDetail : Entity
{
    public decimal Quantity { get; private set; }
    public decimal Price { get; private set; }
    public Guid ExtrasId { get; private set; }
    public Guid ReservationId { get; private set; }

    private ReservationDetail()
    {

    }
    private ReservationDetail(Guid id, decimal quantity, decimal price, Guid extrasId, Guid reservationId)
        :base(id)
    {
        Quantity = quantity;
        Price = price;
        ExtrasId = extrasId;
        ReservationId = reservationId;
    }

    public static ReservationDetail Create(Guid id, decimal quantity, Extras.Extra extras, Reservation reservation)
    {
        var duration = (decimal)reservation.DropDownDate.Subtract(reservation.PickUpDate).TotalDays;

        var price = duration * extras.PricePerDay * quantity;

        return new ReservationDetail(id, quantity, price, extras.Id, reservation.Id);
    }
}
