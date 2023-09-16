using Application.Abstractions;
using Application.Reservations.GetById;
using Domain.Repositories;
using Domain.Sales.Contracts;
using Domain.Sales.Reservations;
using Domain.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.GetById;
internal class GetByIdContractQueryHandler : IQueryHandler<GetByIdContractQuery, Contract?>
{
    private ILogger<GetByIdContractQueryHandler> _logger;
    private readonly IContractRepository _contractRepository;

    public GetByIdContractQueryHandler(ILogger<GetByIdContractQueryHandler> logger, IContractRepository contractRepository)
    {
        _logger = logger;
        _contractRepository = contractRepository;
    }

    public async Task<Result<Contract?>> Handle(GetByIdContractQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started GetByIdContractQueryHandler");
        try
        {
            var contract = await _contractRepository.GetByIdAsync(request.ContractId, cancellationToken);

            if (contract is null || contract == null)
            {
                _logger.LogWarning("GetByIdContractQueryHandler: Contract doesn't exist!");
                return Result.Failure<Contract?>(new Error(
                "Contract.NotFound",
                $"The Contract with Id {request.ContractId} was not found"));
            }

            _logger.LogInformation("Finished GetByIdContractQueryHandler");
            return contract;
        }
        catch (Exception ex)
        {
            _logger.LogError("GetByIdContractQueryHandler error: {0}", ex.Message);
            return Result.Failure<Contract?>(new Error(
                    "Error",
                    ex.Message));
        }



    }
}
