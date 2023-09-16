namespace WebApi.Contracts.Reservations;

public record GetAllReservationsRequest(
    DateTime? DateFrom,
    DateTime? DateTo);