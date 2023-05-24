namespace WebApi.Contracts.ReservationContracts;

public sealed record UpdateReservationContractRequest(Guid id, decimal Price);