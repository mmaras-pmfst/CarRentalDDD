namespace WebApi.Contracts.CarModelRents;

public record CreateCarModelRentRequest(
    DateTime ValidFrom,
    DateTime ValidUntil,
    decimal PricePerDay,
    decimal Discount,
    bool IsVisible,
    Guid CarModelId);