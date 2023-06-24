namespace WebApi.Contracts.Extras;

public record CreateExtraRequest(
    string Name,
    string Description,
    decimal PricePerDay);