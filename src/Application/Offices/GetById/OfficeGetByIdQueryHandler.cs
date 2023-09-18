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

namespace Application.Offices.GetById
{
    internal sealed class OfficeGetByIdQueryHandler : IQueryHandler<OfficeGetByIdQuery, OfficeDetailDto?>
    {
        private readonly IOfficeRepository _officeRepository;
        private ILogger<OfficeGetByIdQueryHandler> _logger;
        private IMapper _mapper;

        public OfficeGetByIdQueryHandler(IOfficeRepository officeRepository, ILogger<OfficeGetByIdQueryHandler> logger, IMapper mapper)
        {
            _officeRepository = officeRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result<OfficeDetailDto?>> Handle(OfficeGetByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started OfficeGetByIdCommandHandler");

            try
            {
                var dbOffice = await _officeRepository.GetByIdAsync(request.OfficeId, cancellationToken);
                if(dbOffice == null || dbOffice is null)
                {
                    _logger.LogWarning("OfficeGetByIdCommandHandler: Office doesn't exist!");
                    return Result.Failure<OfficeDetailDto?>(new Error(
                    "Office.NotFound",
                    $"The Office with Id {request.OfficeId} was not found"));
                }

                var resultDto = _mapper.Map<Office, OfficeDetailDto>(dbOffice);

                _logger.LogInformation("Finished OfficeGetByIdCommandHandler");

                return resultDto;

            }
            catch (Exception ex)
            {
                _logger.LogError("OfficeGetByIdCommandHandler error: {0}", ex.Message);
                return Result.Failure<OfficeDetailDto?>(new Error(
                    "Error",
                    ex.Message));
            }

        }
    }
}
