using Domain.CarBrand.Entities;
using Domain.Common.Models;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ReservationContract;

public sealed class ReservationContract : AggregateRoot
{
    /// Below is for create = create reservation
    public string DriverFirstName { get; set; }
    public string DriverLastName { get; set; }
    public string Email { get; set; }
    public DateTime PickUpDate { get; private set; }
    public DateTime DropDownDate { get; private set; }
    public decimal TotalPrice { get; private set; }
    public Guid PickUpLocationId { get; private set; }
    public Guid DropDownLocationId { get; private set; }
    public Guid CarId { get; private set; }

    /// Below is for update = create contract
    public string? DriverLicenceNumber { get; private set; }
    public string? DriverIdentificationNumber { get; private set; }
    public CardType? CardType { get; private set; }
    public PaymentMethod? PaymentMethod { get; private set; }
    public string? CardName { get; private set; }
    public string? CardNumber { get; private set; }
    public int? CVV { get; private set; }
    public string? CardDateExpiration { get; private set; }
    public string? CardYearExpiration { get; private set; }

    private ReservationContract()
    {
    }

    internal ReservationContract(Guid id, CarModel carModel, string driverFirstName, string driverLastName, string email, DateTime pickUpDate, DateTime dropDownDate, Guid pickUpLocationId, Guid dropDownLocationId, Guid carId, string? driverLicenceNumber = null, string? driverIdentificationNumber = null, CardType? cardType = null, PaymentMethod? paymentMethod = null, string? cardName = null, string? cardNumber = null, int? cVV = null, string? cardDateExpiration = null, string? cardYearExpiration = null)
        :base(id)
    {
        var duration = (decimal)dropDownDate.Subtract(pickUpDate).TotalDays;

        DriverFirstName = driverFirstName;
        DriverLastName = driverLastName;
        Email = email;
        PickUpDate = pickUpDate;
        DropDownDate = dropDownDate;
        TotalPrice = duration * carModel.BasePricePerDay;
        PickUpLocationId = pickUpLocationId;
        DropDownLocationId = dropDownLocationId;
        CarId = carId;
        DriverLicenceNumber = driverLicenceNumber;
        DriverIdentificationNumber = driverIdentificationNumber;
        CardType = cardType;
        PaymentMethod = paymentMethod;
        CardName = cardName;
        CardNumber = cardNumber;
        CVV = cVV;
        CardDateExpiration = cardDateExpiration;
        CardYearExpiration = cardYearExpiration;
    }

    public static ReservationContract CreateReservation(CarModel carModel, string driverFirstName, string driverLastName, string email, DateTime pickUpDate, DateTime dropDownDate, Guid pickUpLocationId, Guid dropDownLocationId, Guid carId)
    {
        return new ReservationContract(Guid.NewGuid(), carModel, driverFirstName, driverLastName, email, pickUpDate, dropDownDate, pickUpLocationId, dropDownLocationId,carId);
    }

    public void CreateContract(CarModel carModel, string driverFirstName, string driverLastName, string email, DateTime pickUpDate, DateTime dropDownDate, Guid pickUpLocationId, Guid dropDownLocationId, Guid carId, string driverLicenceNumber, string driverIdentificationNumber, PaymentMethod? paymentMethod, CardType? cardType = null, string? cardName = null, string? cardNumber = null, int? cVV = null, string? cardDateExpiration = null, string? cardYearExpiration = null)
    {

        var duration = (decimal)dropDownDate.Subtract(pickUpDate).TotalDays;
        DriverFirstName = driverFirstName;
        DriverLastName = driverLastName;
        Email= email;
        TotalPrice = duration * carModel.BasePricePerDay;
        PickUpDate = pickUpDate;
        DropDownDate = dropDownDate;
        PickUpLocationId = pickUpLocationId;
        DropDownLocationId = dropDownLocationId;
        CarId = carId;
        DriverLicenceNumber= driverLicenceNumber;
        DriverIdentificationNumber= driverIdentificationNumber;
        PaymentMethod= paymentMethod;
        CardType= cardType;
        CardName= cardName;
        CardNumber= cardNumber;
        CVV = cVV;
        CardDateExpiration= cardDateExpiration;
        CardYearExpiration= cardYearExpiration;
    }
}
