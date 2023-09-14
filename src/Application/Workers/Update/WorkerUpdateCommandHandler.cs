using Application.Abstractions;
using Application.Workers.Create;
using Domain.Management.Workers;
using Domain.Repositories;
using Domain.Shared;
using Domain.Shared.ValueObjects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Workers.Update;
internal class WorkerUpdateCommandHandler : ICommandHandler<WorkerUpdateCommand, bool>
{
    private ILogger<WorkerUpdateCommandHandler> _logger;
    private readonly IWorkerRepository _workerRepository;
    private readonly IOfficeRepository _officeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public WorkerUpdateCommandHandler(
        ILogger<WorkerUpdateCommandHandler> logger,
        IUnitOfWork unitOfWork,
        IWorkerRepository workerRepository,
        IOfficeRepository officeRepository)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _workerRepository = workerRepository;
        _officeRepository = officeRepository;
    }
    public async Task<Result<bool>> Handle(WorkerUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started WorkerUpdateCommandHandler");

        try
        {
            var worker = await _workerRepository.GetByIdAsync(request.WorkerId, cancellationToken);
            if (worker == null)
            {
                _logger.LogWarning("WorkerGetByIdQueryHandler: Worker doesn't exist!");
                return Result.Failure<bool>(new Error(
                "Worker.NotFound",
                $"The Worker with Id {request.WorkerId} was not found"));
            }

            var office = await _officeRepository.GetByIdAsync(request.OfficeId, cancellationToken);
            if (office == null)
            {
                _logger.LogWarning("WorkerGetByIdQueryHandler: Office doesn't exist!");
                return Result.Failure<bool>(new Error(
                "Office.NotFound",
                $"The Office with Id {request.OfficeId} was not found"));
            }
            
            var emailResult = Email.Create(request.Email);
            if (emailResult.IsFailure)
            {
                return Result.Failure<bool>(emailResult.Error);
            }
            var phoneNumberResult = PhoneNumber.Create(request.PhoneNumber);
            if (phoneNumberResult.IsFailure)
            {
                return Result.Failure<bool>(phoneNumberResult.Error);
            }

            worker.Update(
                emailResult.Value,
                phoneNumberResult.Value,
                office);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Finished WorkerUpdateCommandHandler");
            return true;


        }
        catch (Exception ex)
        {
            _logger.LogError("WorkerUpdateCommandHandler error: {0}", ex.Message);
            return Result.Failure<bool>(new Error(
                "Error",
                ex.Message));
        }

    }
}
