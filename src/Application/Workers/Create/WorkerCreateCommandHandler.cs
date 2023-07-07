using Application.Abstractions;
using Domain.Errors;
using Domain.Management.Office;
using Domain.Repositories;
using Domain.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Workers.Create;
internal class WorkerCreateCommandHandler : ICommandHandler<WorkerCreateCommand, Guid>
{
    private ILogger<WorkerCreateCommandHandler> _logger;
    private readonly IWorkerRepository _workerRepository;
    private readonly IOfficeRepository _officeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public WorkerCreateCommandHandler(ILogger<WorkerCreateCommandHandler> logger, IWorkerRepository workerRepository, IUnitOfWork unitOfWork, IOfficeRepository officeRepository)
    {
        _logger = logger;
        _workerRepository = workerRepository;
        _unitOfWork = unitOfWork;
        _officeRepository = officeRepository;
    }

    public async Task<Result<Guid>> Handle(WorkerCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started WorkerCreateCommandHandler");

        try
        {
            var office = await _officeRepository.GetByIdAsync(request.OfficeId, cancellationToken);
            if (office == null)
            {
                _logger.LogWarning("WorkerCreateCommandHandler: Office doesn't exist!");
                return Result.Failure<Guid>(new Error(
                "Office.NotFound",
                $"The Office with Id {request.OfficeId} was not found"));
            }

            var worker = office.Workers
                .Where(x => x.PersonalIdentificationNumber == request.PersonalIdentificationNumber)
                .SingleOrDefault();
            if (worker is not null)
            {
                _logger.LogWarning("WorkerCreateCommandHandler: Worker already exist!");
                return Result.Failure<Guid>(DomainErrors.Worker.WorkerAlreadyExists);

            }

            var newWorker = office.AddWorker(request.PersonalIdentificationNumber, request.FirstName, request.LastName, request.Email, request.PhoneNumber);

            await _workerRepository.AddAsync(newWorker, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Finished WorkerCreateCommandHandler");
            return newWorker.Id;

        }
        catch (Exception ex)
        {
            _logger.LogError("WorkerCreateCommandHandler error: {0}", ex.Message);
            return Result.Failure<Guid>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
