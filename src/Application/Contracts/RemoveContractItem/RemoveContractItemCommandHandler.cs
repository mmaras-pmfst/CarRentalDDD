using Application.Abstractions;
using Application.Reservations.AddReservationItem;
using Domain.Repositories;
using Domain.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.RemoveContractItem;
internal class RemoveContractItemCommandHandler : ICommandHandler<RemoveContractItemCommand, Guid>
{
    private ILogger<RemoveContractItemCommandHandler> _logger;
    private readonly IContractRepository _contractRepository;
    private readonly IContractItemRepository _contractItemRepository;
    private readonly IExtrasRepository _extrasRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveContractItemCommandHandler(
        ILogger<RemoveContractItemCommandHandler> logger,
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

    public async Task<Result<Guid>> Handle(RemoveContractItemCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started RemoveContractItemCommandHandler");
        _logger.LogInformation("Finished RemoveContractItemCommandHandler");

        try
        {
            var contract = await _contractRepository.GetByIdAsync(request.ContractId, cancellationToken);
            if (contract is null)
            {
                _logger.LogWarning("RemoveContractItemCommandHandler: Contract doesn't exist!");
                return Result.Failure<Guid>(new Error(
                    "Contract.NotFound",
                    $"The Contract with Id {request.ContractId} was not found"));
            }

            foreach (var extraId in request.ExtraIds)
            {
                var dbExtra = await _extrasRepository.GetByIdAsync(extraId, cancellationToken);
                if (dbExtra is null)
                {
                    _logger.LogWarning("RemoveContractItemCommandHandler: Extra doesn't exist!");
                    return Result.Failure<Guid>(new Error(
                            "Extra.NotFound",
                             $"The Extra with Id {extraId} was not found"));
                }
                contract.RemoveContractDetail(dbExtra);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Finished RemoveReservationItemCommandHandler");
            return contract.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError("RemoveContractItemCommandHandler error: {0}", ex.Message);
            return Result.Failure<Guid>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
