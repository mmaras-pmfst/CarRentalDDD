using Domain.Shared.Enums;

namespace WebApi.Contracts.Cars;

public record CreateCarRequest(
    string NumberPlate,
    decimal Kilometers,
    byte[]? Image,
    CarStatus CarStatus,
    FuelType FuelType,
    Guid CarModelId,
    Guid OfficeId);