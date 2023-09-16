namespace WebApi.Contracts.Contracts;

public record GetAllContractRequest(
    DateTime? DateFrom,
    DateTime? DateTo);