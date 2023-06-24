using Domain.Common.Models;
using Domain.Sales.Contract.Entities;
using Domain.Sales.Extras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Sales.CarModelRent.Entities;
public sealed class Reservation : Entity
{
    private readonly List<ReservationDetail> _reservationDetails = new();

    public string DriverFirstName { get; set; }
    public string DriverLastName { get; set; }
    public string Email { get; set; }
    public DateTime PickUpDate { get; private set; }
    public DateTime DropDownDate { get; private set; }
    public decimal RentalPrice { get; private set; }
    public decimal TotalPrice { get; private set; }
    public Guid PickUpLocationId { get; private set; }
    public Guid DropDownLocationId { get; private set; }
    public Guid CarModelRentId { get; private set; }
    public IReadOnlyCollection<ReservationDetail> ReservationDetails => _reservationDetails;


    private Reservation()
    {

    }
    private Reservation(string driverFirstName, string driverLastName, string email, DateTime pickUpDate, DateTime dropDownDate, decimal totalPrice, Guid pickUpLocationId, Guid dropDownLocationId, Guid carModelRentId, decimal rentalPrice)
    {
        DriverFirstName = driverFirstName;
        DriverLastName = driverLastName;
        Email = email;
        PickUpDate = pickUpDate;
        DropDownDate = dropDownDate;
        TotalPrice = totalPrice;
        PickUpLocationId = pickUpLocationId;
        DropDownLocationId = dropDownLocationId;
        CarModelRentId = carModelRentId;
        RentalPrice = rentalPrice;
    }

    public static Reservation Create(string driverFirstName, string driverLastName, string email, DateTime pickUpDate, DateTime dropDownDate, Guid pickUpLocationId, Guid dropDownLocationId, CarModelRent carModelRent)
    {
        var duration = (decimal)dropDownDate.Subtract(pickUpDate).TotalDays;

        var rentalPrice = duration * carModelRent.PricePerDay - ((duration * carModelRent.PricePerDay) * carModelRent.Discount);

        var totalPrice = rentalPrice;

        return new Reservation(driverFirstName, driverLastName, email, pickUpDate, dropDownDate, totalPrice, pickUpLocationId, dropDownLocationId, carModelRent.Id, rentalPrice);
    }

    public void AddReservationDetail(decimal quantity, Extras.Extra extras)
    {
        var detailElement = _reservationDetails.Where(x => x.ExtrasId == extras.Id).SingleOrDefault();
        if (detailElement != null)
        {
            _reservationDetails.RemoveAll(x => x.ExtrasId == extras.Id);
            TotalPrice -= detailElement.Price;
        }
        var reservationDetail = ReservationDetail.Create(Guid.NewGuid(), quantity, extras, this);
        TotalPrice += reservationDetail.Price;
        _reservationDetails.Add(reservationDetail);
    }
}
