namespace WebApi.Contracts.ReservationContracts;

public sealed record CreateReservationContractRequest(
    string DriverFirstName,
    string DriverLastName, 
    DateTime PickUpDate,
    DateTime DropDownDate,
    Guid CarModelId,
    Guid PickupLocationId,
    Guid DropDownLocationId);