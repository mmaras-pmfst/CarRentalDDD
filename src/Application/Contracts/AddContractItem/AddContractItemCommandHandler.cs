using Application.Abstractions;
using Application.Reservations.AddReservationItem;
using Domain.Repositories;
using Domain.Sales.Reservations;
using Domain.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.AddContractItem;
internal class AddContractItemCommandHandler : ICommandHandler<AddContractItemCommand, Guid>
{
    private ILogger<AddContractItemCommandHandler> _logger;
    private readonly IContractRepository _contractRepository;
    private readonly IContractItemRepository _contractItemRepository;
    private readonly IExtrasRepository _extrasRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddContractItemCommandHandler(
        ILogger<AddContractItemCommandHandler> logger,
        IContractRepository contractRepository,
        IContractItemRepository contractItemRepository,
        IExtrasRepository extrasRepository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _contractRepository = contractRepository;
        _contractItemRepository = contractItemRepository;
        _extrasRepository = extrasRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(AddContractItemCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started AddContractItemCommandHandler");
        try
        {
            var contract = await _contractRepository.GetByIdAsync(request.ContractId, cancellationToken);
            if (contract is null || contract == null)
            {
                _logger.LogWarning("AddContractItemCommandHandler: Contract doesn't exist!");
                return Result.Failure<Guid>(new Error(
                    "Contract.NotFound",
                    $"The Contract with Id {request.ContractId} was not found"));
            }

            foreach (var extra in request.Extras)
            {
                var dbExtra = await _extrasRepository.GetByIdAsync(extra.ExtraId, cancellationToken);
                if (dbExtra is null || dbExtra == null)
                {
                    _logger.LogWarning("AddContractItemCommandHandler: Extra doesn't exist!");
                    return Result.Failure<Guid>(new Error(
                            "Extra.NotFound",
                             $"The Extra with Id {extra.ExtraId} was not found"));
                }
                var newExtra = contract.AddContractItem(extra.Quantity, dbExtra);
                await _contractItemRepository.AddAsync(newExtra, cancellationToken);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Finished AddContractItemCommandHandler");
            return contract.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError("AddContractItemCommandHandler error: {0}", ex.Message);
            return Result.Failure<Guid>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
