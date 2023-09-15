using Application.Abstractions;
using Application.Extras.GetAll;
using Domain.Repositories;
using Domain.Sales.Extras;
using Domain.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extras.GetById;
internal class ExtrasGetByIdQueryHandler : IQueryHandler<ExtrasGetByIdQuery, Extra?>
{
    private ILogger<ExtrasGetByIdQueryHandler> _logger;
    private readonly IExtrasRepository _extrasRepository;

    public ExtrasGetByIdQueryHandler(ILogger<ExtrasGetByIdQueryHandler> logger, IExtrasRepository extrasRepository)
    {
        _logger = logger;
        _extrasRepository = extrasRepository;
    }
    public async Task<Result<Extra?>> Handle(ExtrasGetByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started ExtrasGetByIdQueryHandler");

        try
        {

            var dbExtra = await _extrasRepository.GetByIdAsync(request.ExtraId, cancellationToken);

            if (dbExtra == null || dbExtra is null)
            {
                _logger.LogWarning("ExtrasGetByIdQueryHandler: Extra doesn't exist!");
                return Result.Failure<Extra?>(new Error(
                "Extra.NotFound",
                $"The Extra with Id {request.ExtraId} was not found"));
            }

            //TODO: do mapping!!!

            _logger.LogInformation("Finished ExtrasGetByIdQueryHandler");

            return dbExtra;
        }
        catch (Exception ex)
        {
            _logger.LogError("ExtrasGetByIdQueryHandler error: {0}", ex.Message);
            return Result.Failure<Extra?>(new Error(
                "Error",
                ex.Message));
        }

    }
}
