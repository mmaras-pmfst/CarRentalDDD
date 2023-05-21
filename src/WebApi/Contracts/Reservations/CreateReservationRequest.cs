namespace WebApi.Contracts.Reservations;

public sealed record CreateReservationRequest(DateTime PickUpDate,
    DateTime DropDownDate,
    Guid CarModelId,
    Guid PickupLocationId,
    Guid DropDownLocationId);