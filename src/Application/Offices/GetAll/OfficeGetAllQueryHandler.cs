using Application.Abstractions;
using Application.Mappings.DtoModels;
using AutoMapper;
using Domain.Management.Offices;
using Domain.Repositories;
using Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Offices.GetAll
{
    internal sealed class OfficeGetAllQueryHandler : IQueryHandler<OfficeGetAllQuery, List<OfficeDto>>
    {
        private ILogger<OfficeGetAllQueryHandler> _logger;
        private readonly IOfficeRepository _officeRepository;
        private IMapper _mapper;

        public OfficeGetAllQueryHandler(ILogger<OfficeGetAllQueryHandler> logger, IOfficeRepository officeRepository, IMapper mapper)
        {
            _logger = logger;
            _officeRepository = officeRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<OfficeDto>>> Handle(OfficeGetAllQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started GetAllOfficeCommandHandler");

            try
            {   
                var dbOffices = await _officeRepository.GetAllAsync(cancellationToken);

                if (!dbOffices.Any())
                {
                    _logger.LogWarning("GetAllOfficeCommandHandler: No Offices in database");
                    return Result.Failure<List<OfficeDto>>(new Error(
                            "Office.NoData",
                            "There are no Offices to fetch"));
                }

                var resultDto = _mapper.Map<List<Office>,List<OfficeDto>>(dbOffices);

                _logger.LogInformation("Finished GetAllOfficeCommandHandler");

                return resultDto;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAllOfficeCommandHandler error: {0}", ex.Message);
                return Result.Failure<List<OfficeDto>>(new Error(
                    "Error",
                    ex.Message));
            }

        }
    }
}
