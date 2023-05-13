namespace WebApi.Contracts.CarCategories;

public record UpdateCarCategoryRequest(Guid id, string name, string shortName, string description);