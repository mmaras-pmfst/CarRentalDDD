namespace WebApi.Contracts.Reservations;

public record RemoveReservationItemRequest(
    Guid ReservationId,
    List<Guid> ExtraIds);