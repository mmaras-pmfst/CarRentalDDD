using Domain.Shared.Enums;
using WebApi.Contracts.Reservations;

namespace WebApi.Contracts.Contracts;

public record CreateContractRequest(
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
    List<ExtrasRequest>? Extras);