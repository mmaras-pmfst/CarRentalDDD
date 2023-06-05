namespace WebApi.Contracts.ReservationContracts;

public sealed record CreateReservationContractRequest(
    string DriverFirstName,
    string DriverLastName, 
    string Email,
    DateTime PickUpDate,
    DateTime DropDownDate,
    Guid CarModelId,
    Guid PickupLocationId,
    Guid DropDownLocationId);