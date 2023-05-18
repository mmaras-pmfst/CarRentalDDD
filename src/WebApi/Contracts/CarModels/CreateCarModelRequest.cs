namespace WebApi.Contracts.CarModels;

public sealed record CreateCarModelRequest(
    string CarModelName,
    Guid CarBrandId,
    Guid CarCategoryId);
