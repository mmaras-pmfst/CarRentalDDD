namespace WebApi.Contracts.Workers;

public record UpdateWorkerRequest(
    Guid WorkerId,
    string Email,
    string PhoneNumber,
    Guid OfficeId);