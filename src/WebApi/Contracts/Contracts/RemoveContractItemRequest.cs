namespace WebApi.Contracts.Contracts;

public record RemoveContractItemRequest(
    Guid ContractId,
    List<Guid> ExtraIds);