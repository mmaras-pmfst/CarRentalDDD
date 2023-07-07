using Application.Abstractions;
using Application.Extras.Create;
using Domain.Repositories;
using Domain.Sales.Extras;
using Domain.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extras.GetAll;
internal class ExtrasGetAllQueryHandler : IQueryHandler<ExtrasGetAllQuery, List<Extra>>
{
    private ILogger<ExtrasGetAllQueryHandler> _logger;
    private readonly IExtrasRepository _extrasRepository;

    public ExtrasGetAllQueryHandler(ILogger<ExtrasGetAllQueryHandler> logger, IExtrasRepository extrasRepository)
    {
        _logger = logger;
        _extrasRepository = extrasRepository;
    }
    public async Task<Result<List<Extra>>> Handle(ExtrasGetAllQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started ExtrasGetAllQueryHandler");

        try
        {

            var dbExtras = await _extrasRepository.GetAllAsync(cancellationToken);
            if (!dbExtras.Any())
            {
                _logger.LogWarning("ExtrasGetAllQueryHandler: No Extras in database");
                return Result.Failure<List<Extra>>(new Error(
                        "Extra.NoData",
                        "There are no Extras to fetch"));
            }
            //TODO: do mapping!!!

            _logger.LogInformation("Finished ExtrasGetAllQueryHandler");
            return dbExtras;
        }
        catch (Exception ex)
        {
            _logger.LogError("ExtrasGetAllQueryHandler error: {0}", ex.Message);
            return Result.Failure<List<Extra>>(new Error(
                "Error",
                ex.Message));
        }

    }
}
