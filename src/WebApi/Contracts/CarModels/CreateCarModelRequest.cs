namespace WebApi.Contracts.CarModels;

public sealed record CreateCarModelRequest(
    string CarModelName,
    decimal BasePricePerDay,
    Guid CarBrandId,
    Guid CarCategoryId);
