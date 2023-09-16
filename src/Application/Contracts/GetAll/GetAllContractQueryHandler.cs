using Application.Abstractions;
using Application.Reservations.GetAll;
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

namespace Application.Contracts.GetAll;
internal class GetAllContractQueryHandler : IQueryHandler<GetAllContractQuery, List<Contract>>
{
    private ILogger<GetAllContractQueryHandler> _logger;
    private readonly IContractRepository _contractRepository;

    public GetAllContractQueryHandler(ILogger<GetAllContractQueryHandler> logger, IContractRepository contractRepository)
    {
        _logger = logger;
        _contractRepository = contractRepository;
    }

    public async Task<Result<List<Contract>>> Handle(GetAllContractQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started GetAllContractQueryHandler");
        try
        {

            var contracts = await _contractRepository.GetAllAsync(request.DateFrom, request.DateTo, cancellationToken);


            if (!contracts.Any())
            {
                _logger.LogWarning("GetAllContractQueryHandler: No Contract in database");
                return Result.Failure<List<Contract>>(new Error(
                        "Contract.NoData",
                        "There are no Contracts to fetch"));
            }
            _logger.LogInformation("Finished GetAllContractQueryHandler");
            return contracts;

        }
        catch (Exception ex)
        {
            _logger.LogError("GetAllContractQueryHandler error: {0}", ex.Message);
            return Result.Failure<List<Contract>>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
