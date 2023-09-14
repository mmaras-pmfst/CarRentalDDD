using Application.Abstractions;
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
    internal sealed class OfficeGetAllQueryHandler : IQueryHandler<OfficeGetAllQuery, List<Office>>
    {
        private ILogger<OfficeGetAllQueryHandler> _logger;
        private readonly IOfficeRepository _officeRepository;

        public OfficeGetAllQueryHandler(ILogger<OfficeGetAllQueryHandler> logger, IOfficeRepository officeRepository)
        {
            _logger = logger;
            _officeRepository = officeRepository;
        }

        public async Task<Result<List<Office>>> Handle(OfficeGetAllQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started GetAllOfficeCommandHandler");

            try
            {   
                var dbOffices = await _officeRepository.GetAllAsync(cancellationToken);

                if (!dbOffices.Any())
                {
                    _logger.LogWarning("GetAllOfficeCommandHandler: No Offices in database");
                    return Result.Failure<List<Office>>(new Error(
                            "Office.NoData",
                            "There are no Offices to fetch"));
                }

                //TODO: make mapping if needed!!!

                _logger.LogInformation("Finished GetAllOfficeCommandHandler");

                return dbOffices;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAllOfficeCommandHandler error: {0}", ex.Message);
                return Result.Failure<List<Office>>(new Error(
                    "Error",
                    ex.Message));
            }

        }
    }
}
