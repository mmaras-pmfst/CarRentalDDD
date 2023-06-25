namespace WebApi.Contracts.CarModelRents;

public record UpdateCarModelRentRequest(
    Guid CarModelRentId,
    DateTime ValidFrom,
    DateTime ValidUntil,
    decimal PricePerDay,
    decimal Discount,
    bool IsVisible,
    Guid CarModelId);
