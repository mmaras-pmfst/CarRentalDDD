using WebApi.Contracts.Reservations;

namespace WebApi.Contracts.Contracts;

public record CreateContractItemRequest(
    Guid ContractId,
    List<ExtrasRequest> Extras);