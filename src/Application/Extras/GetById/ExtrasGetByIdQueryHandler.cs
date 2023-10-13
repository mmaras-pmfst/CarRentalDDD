using Application.Abstractions;
using Application.Extras.GetAll;
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

namespace Application.Extras.GetById;
internal class ExtrasGetByIdQueryHandler : IQueryHandler<ExtrasGetByIdQuery, ExtraDto?>
{
    private ILogger<ExtrasGetByIdQueryHandler> _logger;
    private readonly IExtrasRepository _extrasRepository;
    private IMapper _mapper;

    public ExtrasGetByIdQueryHandler(ILogger<ExtrasGetByIdQueryHandler> logger, IExtrasRepository extrasRepository, IMapper mapper)
    {
        _logger = logger;
        _extrasRepository = extrasRepository;
        _mapper = mapper;
    }
    public async Task<Result<ExtraDto?>> Handle(ExtrasGetByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started ExtrasGetByIdQueryHandler");

        try
        {

            var dbExtra = await _extrasRepository.GetByIdAsync(request.ExtraId, cancellationToken);

            if (dbExtra == null || dbExtra is null)
            {
                _logger.LogWarning("ExtrasGetByIdQueryHandler: Extra doesn't exist!");
                return Result.Failure<ExtraDto?>(new Error(
                "Extra.NotFound",
                $"The Extra with Id {request.ExtraId} was not found"));
            }

            var resultDto = _mapper.Map<List<Extra>, List<ExtraDto>>(new List<Extra> { dbExtra});

            _logger.LogInformation("Finished ExtrasGetByIdQueryHandler");

            return resultDto.First();
        }
        catch (Exception ex)
        {
            _logger.LogError("ExtrasGetByIdQueryHandler error: {0}", ex.Message);
            return Result.Failure<ExtraDto?>(new Error(
                "Error",
                ex.Message));
        }

    }
}
