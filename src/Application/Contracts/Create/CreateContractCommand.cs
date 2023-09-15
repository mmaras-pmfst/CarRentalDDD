using Application.Abstractions;
using Application.Reservations.Create;
using Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Create;
public sealed record CreateContractCommand(
    string DriverFirstName,
    string DriverLastName,
    string Email,
    DateTime PickUpDate,
    DateTime DropDownDate,
    Guid PickUpOfficeId,
    Guid DropDownOfficeId,
    Guid CarId,
    string DriverLicenceNumber,
    string DriverIdentificationNumber,
    CardType? CardType,
    PaymentMethod PaymentMethod,
    Guid? ReservationId,
    Guid WorkerId,
    string? CardName,
    string? CardNumber,
    string? CVV,
    string? CardDateExpiration,
    string? CardYearExpiration,
    List<ExtrasModel>? Extras) : ICommand<Guid>;