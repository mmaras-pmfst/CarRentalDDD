using Domain.Office;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Office.Create;

internal sealed class CreateOfficeCommandHandler : IRequestHandler<CreateOfficeCommand, Unit>
{
    private readonly IOfficeRepository _officeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private ILogger<CreateOfficeCommandHandler> _logger;

    public CreateOfficeCommandHandler(IOfficeRepository officeRepository, IUnitOfWork unitOfWork, ILogger<CreateOfficeCommandHandler> logger)
    {
        _officeRepository = officeRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Unit> Handle(CreateOfficeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CreateOfficeCommandHandler");

        try
        {
            var exists = await _officeRepository.AlreadyExists(request.city, request.streetName, request.streetNumber, cancellationToken);
            if (exists)
            {
                return Unit.Value;
            }
            var newOffice = Domain.Office.Office.Create(
                    Guid.NewGuid(),
                    request.country,
                    request.city,
                    request.streetName,
                    request.streetNumber,
                    request.openingTime,
                    request.closingTime,
                    request.phoneNumber
                    );

            await _officeRepository.AddAsync(newOffice);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Finished CreateOfficeCommandHandler");

            return Unit.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError("CreateOfficeCommandHandler error: {0}",ex.Message);

            throw;
        }
        
    }

}
