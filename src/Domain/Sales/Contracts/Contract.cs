using Domain.Common.Models;
using Domain.Management.CarModels;
using Domain.Management.Cars;
using Domain.Management.Offices;
using Domain.Management.Workers;
using Domain.Sales.Contracts.Entities;
using Domain.Sales.Extras;
using Domain.Sales.Reservations;
using Domain.Sales.Reservations.ValueObjects;
using Domain.Shared.Enums;
using Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Sales.Contracts;

public sealed class Contract : AggregateRoot, IAuditableEntity
{
    private readonly List<ContractItem> _contractItems = new();

    public FirstName DriverFirstName { get; set; }
    public LastName DriverLastName { get; set; }
    public Email Email { get; set; }
    public DateTime PickUpDate { get; private set; }
    public DateTime DropDownDate { get; private set; }
    public decimal RentalPrice { get; private set; }
    public decimal TotalPrice { get; private set; }
    public Guid PickUpOfficeId { get; private set; }
    public Guid DropDownOfficeId { get; private set; }
    public Guid CarId { get; private set; }
    public string DriverLicenceNumber { get; private set; }
    public string DriverIdentificationNumber { get; private set; }
    public CardType? CardType { get; private set; }
    public PaymentMethod? PaymentMethod { get; private set; }
    public Card? Card { get; private set; }
    public Guid? ReservationId { get; private set; }
    public Guid WorkerId { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public IReadOnlyCollection<ContractItem> ContractItems => _contractItems;
    public Car Car { get; private set; }
    public Office DropDownOffice { get; private set; }
    public Office PickUpOffice { get; private set; }
    public Reservation? Reservation { get; private set; }
    public Worker Worker { get; private set; }

    

    public Contract(Guid id, FirstName driverFirstName, LastName driverLastName, Email email, DateTime pickUpDate, DateTime dropDownDate, decimal totalPrice, Guid pickUpOfficeId, Guid dropDownOfficeId, Guid carId, string driverLicenceNumber, string driverIdentificationNumber, CardType? cardType, PaymentMethod? paymentMethod, Card? card, Guid? reservationId, decimal rentalPrice, Guid workerId)
        : base(id)
    {
        DriverFirstName = driverFirstName;
        DriverLastName = driverLastName;
        Email = email;
        PickUpDate = pickUpDate;
        DropDownDate = dropDownDate;
        TotalPrice = totalPrice;
        PickUpOfficeId = pickUpOfficeId;
        DropDownOfficeId = dropDownOfficeId;
        CarId = carId;
        DriverLicenceNumber = driverLicenceNumber;
        DriverIdentificationNumber = driverIdentificationNumber;
        CardType = cardType;
        PaymentMethod = paymentMethod;
        Card = card;
        ReservationId = reservationId;
        RentalPrice = rentalPrice;
        WorkerId = workerId;
    }


    public static Contract Create(Guid id, FirstName driverFirstName, LastName driverLastName, Email email, DateTime pickUpDate, DateTime dropDownDate, Office pickUpOffice, Office dropDownOffice, Car car, string driverLicenceNumber, string driverIdentificationNumber, CardType? cardType, PaymentMethod? paymentMethod, Card card, Reservation? reservation, CarModel carModel, Worker worker)
    {
        var duration = (decimal)dropDownDate.Subtract(pickUpDate).TotalDays;

        var rentalPrice = duration * carModel.PricePerDay - ((duration * carModel.PricePerDay) * carModel.Discount);

        var totalPrice = rentalPrice;


        return new Contract(id, driverFirstName, driverLastName, email, pickUpDate, dropDownDate, totalPrice, pickUpOffice.Id, dropDownOffice.Id, car.Id, driverLicenceNumber, driverIdentificationNumber, cardType, paymentMethod, card, reservation == null ? null : reservation.Id, rentalPrice, worker.Id);


    }



    public void Update(DateTime dropDownDate, Office dropDownOffice, CarModel carModel)
    {

        var duration = (decimal)dropDownDate.Subtract(PickUpDate).TotalDays;
        var priceCalculation = duration * carModel.PricePerDay - ((duration * carModel.PricePerDay) * carModel.Discount);
        DropDownDate = dropDownDate;
        TotalPrice = priceCalculation;
        DropDownOfficeId = dropDownOffice.Id;

    }

    public void AddContractDetail(decimal quantity, Extra extra)
    {
        var detailElement = _contractItems.Where(x => x.ExtraId == extra.Id).SingleOrDefault();
        if (detailElement != null)
        {
            _contractItems.RemoveAll(x => x.ExtraId == extra.Id);
            TotalPrice -= detailElement.Price;
        }
        var contracDetail = ContractItem.Create(Guid.NewGuid(), quantity, extra, this);
        TotalPrice += contracDetail.Price;
        _contractItems.Add(contracDetail);
    }

    public void RemoveContractDetail(decimal quantity, Extra extra)
    {
        var contractDetail = _contractItems.Where(x => x.ExtraId == extra.Id).SingleOrDefault();
        if (contractDetail != null)
        {
            if (contractDetail.Quantity == quantity)
            {
                _contractItems.RemoveAll(x => x.ExtraId == extra.Id);
                TotalPrice -= contractDetail.Price;
            }
            else if (contractDetail.Quantity > quantity)
            {
                AddContractDetail(quantity, extra);
            }


        }
    }

}
