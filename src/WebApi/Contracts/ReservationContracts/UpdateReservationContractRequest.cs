using Domain.Enums;

namespace WebApi.Contracts.ReservationContracts;

public sealed record UpdateReservationContractRequest(
    Guid ReservationContractId,
    string DriverFirstName,
    string DriverLastName,
    DateTime PickUpDate,
    DateTime DropDownDate,
    Guid PickupLocationId,
    Guid DropDownLocationId,
    string DriverLicenceNumber,
    string DriverIdentificationNumber,
    CardType CardType,
    PaymentMethod PaymentMethod,
    string CardName,
    string CardNumber,
    int CVV,
    string CardDateExpiration,
    string CardYearExpiration,
    Guid CarId);