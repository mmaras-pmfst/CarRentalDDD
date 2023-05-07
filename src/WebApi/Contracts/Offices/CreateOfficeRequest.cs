namespace WebApi.Contracts.Offices;

public sealed record CreateOfficeRequest(string country, string city, string streetName,
    string streetNumber, DateTime? openingTime, DateTime? closingTime, string phoneNumber);
