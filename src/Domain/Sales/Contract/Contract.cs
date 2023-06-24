using Domain.Common.Models;
using Domain.Enums;
using Domain.Management.CarBrand.Entities;
using Domain.Sales.Contract.Entities;
using Domain.Sales.Extras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Sales.Contract;
public sealed class Contract : AggregateRoot
{
    private readonly List<ContractDetail> _contractDetails = new();

    public string DriverFirstName { get; set; }
    public string DriverLastName { get; set; }
    public string Email { get; set; }
    public DateTime PickUpDate { get; private set; }
    public DateTime DropDownDate { get; private set; }
    public decimal RentalPrice { get; private set; }
    public decimal TotalPrice { get; private set; }
    public Guid PickUpLocationId { get; private set; }
    public Guid DropDownLocationId { get; private set; }
    public Guid CarId { get; private set; }
    public string DriverLicenceNumber { get; private set; }
    public string DriverIdentificationNumber { get; private set; }
    public CardType? CardType { get; private set; }
    public PaymentMethod? PaymentMethod { get; private set; }
    public string? CardName { get; private set; }
    public string? CardNumber { get; private set; }
    public int? CVV { get; private set; }
    public string? CardDateExpiration { get; private set; }
    public string? CardYearExpiration { get; private set; }
    public Guid? ReservationId { get; private set; }
    public Guid WorkerId { get; private set; }

    public IReadOnlyCollection<ContractDetail> ContractDetails => _contractDetails;


    public Contract(Guid id, string driverFirstName, string driverLastName, string email, DateTime pickUpDate, DateTime dropDownDate, decimal totalPrice, Guid pickUpLocationId, Guid dropDownLocationId, Guid carId, string driverLicenceNumber, string driverIdentificationNumber, CardType? cardType, PaymentMethod? paymentMethod, string? cardName, string? cardNumber, int? cVV, string? cardDateExpiration, string? cardYearExpiration, Guid? reservationId, decimal rentalPrice, Guid workerId)
        : base(id)
    {
        DriverFirstName = driverFirstName;
        DriverLastName = driverLastName;
        Email = email;
        PickUpDate = pickUpDate;
        DropDownDate = dropDownDate;
        TotalPrice = totalPrice;
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
        ReservationId = reservationId;
        RentalPrice = rentalPrice;
        WorkerId = workerId;
    }


    public static Contract Create(Guid id, string driverFirstName, string driverLastName, string email, DateTime pickUpDate, DateTime dropDownDate, Guid pickUpLocationId, Guid dropDownLocationId, Guid carId, string? driverLicenceNumber, string? driverIdentificationNumber, CardType? cardType, PaymentMethod? paymentMethod, string? cardName, string? cardNumber, int? cVV, string? cardDateExpiration, string? cardYearExpiration, Guid? reservationId, CarModelRent.CarModelRent carModelRent, Guid workerId)
    {
        var duration = (decimal)dropDownDate.Subtract(pickUpDate).TotalDays;

        var rentalPrice = duration * carModelRent.PricePerDay - ((duration * carModelRent.PricePerDay) * carModelRent.Discount);

        var totalPrice = rentalPrice;


        return new Contract(id, driverFirstName, driverLastName, email, pickUpDate, dropDownDate, totalPrice, pickUpLocationId, dropDownLocationId, carId, driverLicenceNumber, driverIdentificationNumber, cardType, paymentMethod, cardName, cardNumber, cVV, cardDateExpiration, cardYearExpiration, reservationId, rentalPrice, workerId);


    }

    

    public void Update(DateTime dropDownDate, Guid dropDownLocationId, CarModelRent.CarModelRent carModelRent)
    {

        var duration = (decimal)dropDownDate.Subtract(PickUpDate).TotalDays;
        var priceCalculation = duration * carModelRent.PricePerDay - ((duration * carModelRent.PricePerDay) * carModelRent.Discount);
        DropDownDate= dropDownDate;
        DropDownLocationId = dropDownLocationId;
        TotalPrice= priceCalculation;

    }

    public void AddContractDetail(decimal quantity, Extras.Extra extras)
    {
        var detailElement = _contractDetails.Where(x => x.ExtrasId == extras.Id).SingleOrDefault();
        if(detailElement != null)
        {
            _contractDetails.RemoveAll(x => x.ExtrasId == extras.Id);
            TotalPrice -= detailElement.Price;
        }
        var contracDetail = ContractDetail.Create(Guid.NewGuid(),quantity, extras, this);
        TotalPrice += contracDetail.Price;
        _contractDetails.Add(contracDetail);
    }

    public void RemoveContractDetail(decimal quantity, Extras.Extra extras)
    {
        var contractDetail = _contractDetails.Where(x => x.ExtrasId == extras.Id).SingleOrDefault();
        if(contractDetail != null)
        {
            if (contractDetail.Quantity == quantity)
            {
                _contractDetails.RemoveAll(x => x.ExtrasId == extras.Id);
                TotalPrice -= contractDetail.Price;
            }
            else if(contractDetail.Quantity > quantity)
            {
                AddContractDetail(quantity, extras);
            }

                
        }
    }

}
