namespace WebApi.Contracts.Workers;

public record CreateWorkerRequest(
    string PersonalIdentificationNumber,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    Guid OfficeId);