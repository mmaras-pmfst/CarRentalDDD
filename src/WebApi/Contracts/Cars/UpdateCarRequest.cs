using Domain.Shared.Enums;

namespace WebApi.Contracts.Cars;

public record UpdateCarRequest(
    Guid CarId,
    decimal Kilometers,
    byte[]? Image,
    CarStatus CarStatus,
    Guid OfficeId);