namespace WebApi.Contracts.CarModels;

public sealed record UpdateCarModelRequest(
    Guid CarModelId,
    string CarModelName,
    Guid CarBrandId,
    Guid CarCategoryId);
