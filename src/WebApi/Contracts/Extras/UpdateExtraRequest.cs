namespace WebApi.Contracts.Extras;

public record UpdateExtraRequest(
    Guid ExtraId,
    string Name,
    string Description,
    decimal PricePerDay);