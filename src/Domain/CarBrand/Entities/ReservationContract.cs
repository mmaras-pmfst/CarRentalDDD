using Domain.Common.Models;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CarBrand.Entities;

public sealed class ReservationContract : Entity
{

    /// Below is for create = create reservation
    public string DriverFirstName { get; set; }
    public string DriverLastName { get; set; }
    public DateTime PickUpDate { get; private set; }
    public DateTime DropDownDate { get; private set; }
    public decimal TotalPrice { get; private set; }
    public Guid CarModelId { get; private set; }
    public Guid PickUpLocationId { get; private set; }
    public Guid DropDownLocationId { get; private set; }

    /// Below is for update = create contract
    public Guid? CarId { get; private set; }
    public string? DriverLicenceNumber { get; private set; }
    public string? DriverIdentificationNumber { get; private set; }
    public CardType? CardType { get; private set; }
    public PaymentMethod? PaymentMethod { get; private set; }
    public string? CardName { get; private set; }
    public string? CardNumber { get; private set; }
    public int? CVV { get; private set; }
    public string? CardDateExpiration { get; private set; }
    public string? CardYearExpiration { get; private set; }


    internal ReservationContract(Guid id, CarModel carModel, string driverFirstName, string driverLastName, DateTime pickUpDate, DateTime dropDownDate, Guid pickUpLocationId, Guid dropDownLocationId, string? driverLicenceNumber = null, string? driverIdentificationNumber = null, CardType? cardType = null, PaymentMethod? paymentMethod = null, string? cardName = null, string? cardNumber = null, int? cVV = null, string? cardDateExpiration = null, string? cardYearExpiration = null, Guid? carId = null)
        : base(id)
    {
        var duration = (decimal)dropDownDate.Subtract(pickUpDate).TotalDays;
        DriverFirstName = driverFirstName;
        DriverLastName = driverLastName;
        PickUpDate = pickUpDate;
        DropDownDate = dropDownDate;
        TotalPrice = TotalPrice = duration * carModel.BasePricePerDay; ;
        CarModelId = carModel.Id;
        PickUpLocationId = pickUpLocationId;
        DropDownLocationId = dropDownLocationId;
        DriverLicenceNumber = driverLicenceNumber;
        DriverIdentificationNumber = driverIdentificationNumber;
        CardType = cardType;
        PaymentMethod = paymentMethod;
        CardName = cardName;
        CardNumber = cardNumber;
        CVV = cVV;
        CardDateExpiration = cardDateExpiration;
        CardYearExpiration = cardYearExpiration;
        CarId = carId;
    }

    private ReservationContract()
    {
    }

    public void Update(CarModel carModel, string driverFirstName, string driverLastName, DateTime pickUpDate, DateTime dropDownDate, Guid pickUpLocationId, Guid dropDownLocationId, Guid carId, string? driverLicenceNumber, string? driverIdentificationNumber, CardType? cardType, PaymentMethod? paymentMethod, string? cardName, string? cardNumber, int? cVV, string? cardDateExpiration, string? cardYearExpiration)
    {

        var duration = (decimal)dropDownDate.Subtract(pickUpDate).TotalDays;
        DriverFirstName = driverFirstName;
        DriverLastName = driverLastName;
        PickUpDate = pickUpDate;
        DropDownDate = dropDownDate;
        TotalPrice = TotalPrice = duration * carModel.BasePricePerDay; ;
        CarModelId = carModel.Id;
        PickUpLocationId = pickUpLocationId;
        DropDownLocationId = dropDownLocationId;
        DriverLicenceNumber = driverLicenceNumber;
        DriverIdentificationNumber = driverIdentificationNumber;
        CardType = cardType;
        PaymentMethod = paymentMethod;
        CardName = cardName;
        CardNumber = cardNumber;
        CVV = cVV;
        CardDateExpiration = cardDateExpiration;
        CardYearExpiration = cardYearExpiration;
        CarId = carId;

    }

}
