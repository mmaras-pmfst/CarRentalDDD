namespace WebApi.Contracts.ReservationContracts;

public sealed record CreateReservationContractRequest(DateTime PickUpDate,
    DateTime DropDownDate,
    Guid CarModelId,
    Guid PickupLocationId,
    Guid DropDownLocationId);