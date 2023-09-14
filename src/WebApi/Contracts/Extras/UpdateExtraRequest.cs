namespace WebApi.Contracts.Extras;

public record UpdateExtraRequest(
    Guid ExtraId,
    string Description,
    decimal PricePerDay);