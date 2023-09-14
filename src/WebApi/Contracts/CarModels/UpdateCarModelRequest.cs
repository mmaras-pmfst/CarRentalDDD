using Azure.Core;

namespace WebApi.Contracts.CarModels;

public sealed record UpdateCarModelRequest(
    Guid CarModelId,
    decimal PricePerDay,
    decimal Discount,
    string CarModelName,
    Guid CarCategoryId);
