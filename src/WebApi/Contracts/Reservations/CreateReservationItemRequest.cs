namespace WebApi.Contracts.Reservations;

public record CreateReservationItemRequest(
    Guid ReservationId,
    List<ExtrasRequest> Extras);