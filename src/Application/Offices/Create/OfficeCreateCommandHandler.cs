using Application.Abstractions;
using Domain.Errors;
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

namespace Application.Offices.Create;

internal sealed class OfficeCreateCommandHandler : ICommandHandler<OfficeCreateCommand, Guid>
{
    private readonly IOfficeRepository _officeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private ILogger<OfficeCreateCommandHandler> _logger;

    public OfficeCreateCommandHandler(IOfficeRepository officeRepository, IUnitOfWork unitOfWork, ILogger<OfficeCreateCommandHandler> logger)
    {
        _officeRepository = officeRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<Guid>> Handle(OfficeCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CreateOfficeCommandHandler");

        try
        {
            var exists = await _officeRepository.AlreadyExists(request.City, request.StreetName, request.StreetNumber, cancellationToken);
            if (exists)
            {
                _logger.LogWarning("CreateOfficeCommandHandler: Office already exists!");
                return Result.Failure<Guid>(DomainErrors.Office.OfficeAlreadyExists);

            }
            var newOffice = Office.Create(
                    Guid.NewGuid(),
                    request.Country,
                    request.City,
                    request.StreetName,
                    request.StreetNumber,
                    request.OpeningTime,
                    request.ClosingTime,
                    request.PhoneNumber
                    );

            await _officeRepository.AddAsync(newOffice);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Finished CreateOfficeCommandHandler");

            return newOffice.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError("CreateOfficeCommandHandler error: {0}", ex.Message);
            return Result.Failure<Guid>(new Error(
                    "Error",
                    ex.Message));
        }

    }

}
