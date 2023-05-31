using Application.Abstractions;
using Domain.Office;
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
    internal sealed class OfficeGetByIdQueryHandler : IQueryHandler<OfficeGetByIdQuery, Office?>
    {
        private readonly IOfficeRepository _officeRepository;
        private ILogger<OfficeGetByIdQueryHandler> _logger;

        public OfficeGetByIdQueryHandler(IOfficeRepository officeRepository, ILogger<OfficeGetByIdQueryHandler> logger)
        {
            _officeRepository = officeRepository;
            _logger = logger;
        }

        public async Task<Result<Office?>> Handle(OfficeGetByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started OfficeGetByIdCommandHandler");

            try
            {
                var dbOffice = await _officeRepository.GetByIdAsync(request.OfficeId, cancellationToken);
                if(dbOffice == null)
                {
                    _logger.LogWarning("OfficeGetByIdCommandHandler: Office doesn't exist!");
                    return Result.Failure<Office?>(new Error(
                    "Office.NotFound",
                    $"The Office with Id {request.OfficeId} was not found"));
                }

                //TODO: mapping if needed!!!

                _logger.LogInformation("Finished OfficeGetByIdCommandHandler");

                return dbOffice;

            }
            catch (Exception ex)
            {
                _logger.LogError("OfficeGetByIdCommandHandler error: {0}", ex.Message);
                return Result.Failure<Office?>(new Error(
                    "Error",
                    ex.Message));
            }

        }
    }
}
