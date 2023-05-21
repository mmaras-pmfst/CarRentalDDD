using Azure.Core;

namespace WebApi.Contracts.CarModels;

public sealed record UpdateCarModelRequest(
    Guid CarModelId,
    decimal BasePricePerDay,
    string CarModelName,
    Guid CarBrandId,
    Guid CarCategoryId);
