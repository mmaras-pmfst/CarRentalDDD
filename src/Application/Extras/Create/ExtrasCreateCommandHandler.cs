using Application.Abstractions;
using Domain.Errors;
using Domain.Repositories;
using Domain.Sales.Extras;
using Domain.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extras.Create;
internal class ExtrasCreateCommandHandler : ICommandHandler<ExtrasCreateCommand, Guid>
{
    private ILogger<ExtrasCreateCommandHandler> _logger;
    private readonly IExtrasRepository _extrasRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ExtrasCreateCommandHandler(ILogger<ExtrasCreateCommandHandler> logger, IExtrasRepository extrasRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _extrasRepository = extrasRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(ExtrasCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started ExtrasCreateCommandHandler");

        try
        {

            var exists = await _extrasRepository.AlreadyExists(request.Name,cancellationToken);
            if (exists)
            {
                _logger.LogWarning("ExtrasCreateCommandHandler: Extra already exists!");
                return Result.Failure<Guid>(DomainErrors.Extra.ExtraAlreadyExists);
            }

            var newExtra = Extra.Create(
                Guid.NewGuid(),
                request.Name,
                request.Description,
                request.PricePerDay);

            await _extrasRepository.AddAsync(newExtra, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Finished ExtrasCreateCommandHandler");

            return newExtra.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError("ExtrasCreateCommandHandler error: {0}", ex.Message);
            return Result.Failure<Guid>(new Error(
                "Error",
                ex.Message));
        }
    }
}
