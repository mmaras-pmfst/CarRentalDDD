using Application.Abstractions;
using Application.Extras.Create;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extras.Update;
internal class ExtrasUpdateCommandHandler : ICommandHandler<ExtrasUpdateCommand, bool>
{
    private ILogger<ExtrasUpdateCommandHandler> _logger;
    private readonly IExtrasRepository _extrasRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ExtrasUpdateCommandHandler(ILogger<ExtrasUpdateCommandHandler> logger, IExtrasRepository extrasRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _extrasRepository = extrasRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<bool>> Handle(ExtrasUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started ExtrasUpdateCommandHandler");

        try
        {
            var exists = await _extrasRepository.AlreadyExists(request.Name, cancellationToken);
            if (exists)
            {
                _logger.LogWarning("ExtrasUpdateCommandHandler: Extra already exists!");
                return Result.Failure<bool>(DomainErrors.Extra.ExtraAlreadyExists);
            }
            var dbExtra = await _extrasRepository.GetByIdAsync(request.ExtraId, cancellationToken);
            if(dbExtra == null)
            {
                _logger.LogWarning("ExtrasUpdateCommandHandler: Extra doesn't exist!");
                return Result.Failure<bool>(new Error(
                "Extra.NotFound",
                $"The Extra with Id {request.ExtraId} was not found"));
            }

            dbExtra.Update(request.Name, request.Description, request.PricePerDay);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Finished ExtrasUpdateCommandHandler");
            return true;

        }
        catch (Exception ex)
        {
            _logger.LogError("ExtrasUpdateCommandHandler error: {0}", ex.Message);
            return Result.Failure<bool>(new Error(
                "Error",
                ex.Message));
        }

    }
}
