namespace WebApi.Contracts.CarModels;

public sealed record CreateCarModelRequest(
    string CarModelName,
    decimal PricePerDay,
    decimal Discount,
    Guid CarBrandId,
    Guid CarCategoryId);
