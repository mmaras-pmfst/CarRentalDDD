using Application.Abstractions;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ReservationContracts.Update;
public sealed record ReservationContractUpdateCommand(
    Guid ReservationContractId,
    string DriverFirstName,
    string DriverLastName,
    string Email,
    DateTime PickUpDate,
    DateTime DropDownDate,
    Guid PickUpLocationId,
    Guid DropDownLocationId,
    string DriverLicenceNumber,
    string DriverIdentificationNumber,
    CardType? CardType,
    PaymentMethod PaymentMethod,
    string? CardName,
    string CardNumber,
    int? CVV,
    string? CardDateExpiration,
    string? CardYearExpiration) : ICommand<bool>;