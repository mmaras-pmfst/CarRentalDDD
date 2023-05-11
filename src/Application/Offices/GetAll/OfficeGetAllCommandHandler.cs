using Domain.Office;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Offices.GetAll
{
    internal sealed class OfficeGetAllCommandHandler : IRequestHandler<OfficeGetAllCommand, List<Office>>
    {
        private ILogger<OfficeGetAllCommandHandler> _logger;
        private readonly IOfficeRepository _officeRepository;

        public OfficeGetAllCommandHandler(ILogger<OfficeGetAllCommandHandler> logger, IOfficeRepository officeRepository)
        {
            _logger = logger;
            _officeRepository = officeRepository;
        }

        public async Task<List<Office>> Handle(OfficeGetAllCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started GetAllOfficeCommandHandler");

            try
            {   
                var dbOffices = await _officeRepository.GetAllAsync(cancellationToken);

                //TODO: make mapping if needed!!!

                _logger.LogInformation("Finished GetAllOfficeCommandHandler");

                return dbOffices;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAllOfficeCommandHandler error: {0}", ex.Message);  

                throw;
            }

        }
    }
}
