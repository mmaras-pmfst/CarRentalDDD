using Domain.Office;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Offices.GetById
{
    internal sealed class OfficeGetByIdCommandHandler : IRequestHandler<OfficeGetByIdCommand, Office?>
    {
        private readonly IOfficeRepository _officeRepository;
        private ILogger<OfficeGetByIdCommandHandler> _logger;

        public OfficeGetByIdCommandHandler(IOfficeRepository officeRepository, ILogger<OfficeGetByIdCommandHandler> logger)
        {
            _officeRepository = officeRepository;
            _logger = logger;
        }

        public async Task<Office?> Handle(OfficeGetByIdCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started OfficeGetByIdCommandHandler");

            try
            {
                var dbOffice = await _officeRepository.GetByIdAsync(request.id, cancellationToken);
                if(dbOffice == null)
                {
                    _logger.LogWarning("OfficeGetByIdCommandHandler: Office.Id doesn't exist!");
                    return null;
                }

                //TODO: mapping if needed!!!

                _logger.LogInformation("Finished OfficeGetByIdCommandHandler");

                return dbOffice;

            }
            catch (Exception ex)
            {
                _logger.LogError("OfficeGetByIdCommandHandler error: {0}", ex.Message);

                throw;
            }

        }
    }
}
