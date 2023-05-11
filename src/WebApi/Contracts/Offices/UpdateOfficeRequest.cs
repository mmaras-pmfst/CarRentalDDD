namespace WebApi.Contracts.Offices
{
    public sealed record UpdateOfficeRequest(Guid id, string country, string city, string streetName,
    string streetNumber, DateTime? openingTime, DateTime? closingTime, string phoneNumber);
}
