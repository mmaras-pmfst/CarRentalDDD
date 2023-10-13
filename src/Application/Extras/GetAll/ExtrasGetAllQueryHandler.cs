using Application.Abstractions;
using Application.Extras.Create;
using Application.Mappings.DtoModels;
using AutoMapper;
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
internal class ExtrasGetAllQueryHandler : IQueryHandler<ExtrasGetAllQuery, List<ExtraDto>>
{
    private ILogger<ExtrasGetAllQueryHandler> _logger;
    private readonly IExtrasRepository _extrasRepository;
    private IMapper _mapper;

    public ExtrasGetAllQueryHandler(ILogger<ExtrasGetAllQueryHandler> logger, IExtrasRepository extrasRepository, IMapper mapper)
    {
        _logger = logger;
        _extrasRepository = extrasRepository;
        _mapper = mapper;
    }
    public async Task<Result<List<ExtraDto>>> Handle(ExtrasGetAllQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started ExtrasGetAllQueryHandler");

        try
        {

            var dbExtras = await _extrasRepository.GetAllAsync(cancellationToken);
            if (!dbExtras.Any())
            {
                _logger.LogWarning("ExtrasGetAllQueryHandler: No Extras in database");
                return Result.Failure<List<ExtraDto>>(new Error(
                        "Extra.NoData",
                        "There are no Extras to fetch"));
            }
            
            var resultDto = _mapper.Map<List<Extra>, List<ExtraDto>>(dbExtras);

            _logger.LogInformation("Finished ExtrasGetAllQueryHandler");
            return resultDto;
        }
        catch (Exception ex)
        {
            _logger.LogError("ExtrasGetAllQueryHandler error: {0}", ex.Message);
            return Result.Failure<List<ExtraDto>>(new Error(
                "Error",
                ex.Message));
        }

    }
}
