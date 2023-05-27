using Domain.Office;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Offices.Update
{
    internal sealed class OfficeUpdateCommandHandler : IRequestHandler<OfficeUpdateCommand, Unit>
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private ILogger<OfficeUpdateCommandHandler> _logger;

        public OfficeUpdateCommandHandler(IOfficeRepository officeRepository, IUnitOfWork unitOfWork, ILogger<OfficeUpdateCommandHandler> logger)
        {
            _officeRepository = officeRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Unit> Handle(OfficeUpdateCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started OfficeUpdateCommandHandler");

            try
            {
                var dbOffice = await _officeRepository.GetByIdAsync(request.id, cancellationToken);

                if(dbOffice == null)
                {
                    _logger.LogWarning("OfficeUpdateCommandHandler: Office.Id doesn't exist!");
                    return Unit.Value;
                }

                dbOffice.Update(request.country, request.city, request.streetName, request.streetNumber, request.openingTime, request.closingTime, request.phoneNumber);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Finished OfficeUpdateCommandHandler");

                return Unit.Value;

            }
            catch (Exception ex)
            {
                _logger.LogError("OfficeUpdateCommandHandler error: {0}", ex.Message);

                throw;
            }
        }
    }
}
