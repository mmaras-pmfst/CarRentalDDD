using Domain.Common.Models;
using Domain.Management.CarModels;
using Domain.Management.Offices;
using Domain.Sales.Contracts;
using Domain.Sales.Extras;
using Domain.Sales.Reservations.Entities;
using Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Sales.Reservations;

public sealed class Reservation : AggregateRoot, IAuditableEntity
{
    private readonly List<ReservationItem> _reservationItems = new();

    public FirstName DriverFirstName { get; set; }
    public LastName DriverLastName { get; set; }
    public Email Email { get; set; }
    public DateTime PickUpDate { get; private set; }
    public DateTime DropDownDate { get; private set; }
    public decimal RentalPrice { get; private set; }
    public decimal TotalPrice { get; private set; }
    public Guid PickUpOfficeId { get; private set; }
    public Guid DropDownOfficeId { get; private set; }
    public Guid CarModelId { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public CarModel CarModel { get; private set; }
    public Office PickUpOffice { get; private set; }
    public Office DropDownOffice { get; private set; }
    public Contract Contract { get; private set; }

    public IReadOnlyCollection<ReservationItem> ReservationItems => _reservationItems;

    

    private Reservation()
    {

    }
    private Reservation(FirstName driverFirstName, LastName driverLastName, Email email, DateTime pickUpDate, DateTime dropDownDate, decimal totalPrice, Guid pickUpLocationId, Guid dropDownLocationId, Guid carModelId, decimal rentalPrice)
    {
        DriverFirstName = driverFirstName;
        DriverLastName = driverLastName;
        Email = email;
        PickUpDate = pickUpDate;
        DropDownDate = dropDownDate;
        TotalPrice = totalPrice;
        PickUpOfficeId = pickUpLocationId;
        DropDownOfficeId = dropDownLocationId;
        CarModelId = carModelId;
        RentalPrice = rentalPrice;
    }

    public static Reservation Create(FirstName driverFirstName, LastName driverLastName, Email email, DateTime pickUpDate, DateTime dropDownDate, Guid pickUpLocationId, Guid dropDownLocationId, CarModel carModel)
    {
        var duration = (decimal)dropDownDate.Subtract(pickUpDate).TotalDays;

        var rentalPrice = duration * carModel.PricePerDay - duration * carModel.PricePerDay * carModel.Discount;

        var totalPrice = rentalPrice;

        return new Reservation(driverFirstName, driverLastName, email, pickUpDate, dropDownDate, totalPrice, pickUpLocationId, dropDownLocationId, carModel.Id, rentalPrice);
    }

    public ReservationItem AddItem(decimal quantity, Extra extras)
    {
        var detailElement = _reservationItems.Where(x => x.ExtrasId == extras.Id).SingleOrDefault();
        if (detailElement is not null)
        {
            _reservationItems.RemoveAll(x => x.ExtrasId == extras.Id);
            TotalPrice -= detailElement.Price;
        }
        var reservationDetail = ReservationItem.Create(Guid.NewGuid(), quantity, extras, this);
        TotalPrice += reservationDetail.Price;
        _reservationItems.Add(reservationDetail);
        return reservationDetail;
    }

    public void RemoveItem(Extra extra)
    {
        var detailElement = _reservationItems.Where(x => x.ExtrasId == extra.Id).SingleOrDefault();
        if (detailElement is not null)
        {
            _reservationItems.RemoveAll(x => x.ExtrasId == extra.Id);
            TotalPrice -= detailElement.Price;
        }
    }
}
