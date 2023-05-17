namespace WebApi.Contracts.CarBrands;

public record UpdateCarBrandRequest(Guid id, string carBrandName);