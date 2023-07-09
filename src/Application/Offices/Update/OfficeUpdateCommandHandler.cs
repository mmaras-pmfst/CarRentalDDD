using Application.Abstractions;
using Domain.Common.ValueObjects;
using Domain.Management.Office;
using Domain.Repositories;
using Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Offices.Update
{
    internal sealed class OfficeUpdateCommandHandler : ICommandHandler<OfficeUpdateCommand, bool>
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

        public async Task<Result<bool>> Handle(OfficeUpdateCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started OfficeUpdateCommandHandler");

            try
            {
                var dbOffice = await _officeRepository.GetByIdAsync(request.OfficeId, cancellationToken);

                if(dbOffice == null)
                {
                    _logger.LogWarning("OfficeUpdateCommandHandler: Office doesn't exist!");
                    return Result.Failure<bool>(new Error(
                            "Office.NotFound",
                            $"The Office with Id {request.OfficeId} was not found"));
                }


                dbOffice.Update(request.Country, request.City, request.StreetName, request.StreetNumber, request.OpeningTime, request.ClosingTime, request.PhoneNumber);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Finished OfficeUpdateCommandHandler");

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError("OfficeUpdateCommandHandler error: {0}", ex.Message);
                return Result.Failure<bool>(new Error(
                    "Error",
                    ex.Message));
            }
        }
    }
}
