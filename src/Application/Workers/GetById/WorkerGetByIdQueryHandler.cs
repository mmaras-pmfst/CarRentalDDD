using Application.Abstractions;
using Application.Workers.Update;
using Domain.Management.Workers;
using Domain.Repositories;
using Domain.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Workers.GetById;
internal class WorkerGetByIdQueryHandler : IQueryHandler<WorkerGetByIdQuery, Worker?>
{
    private ILogger<WorkerGetByIdQueryHandler> _logger;
    private readonly IWorkerRepository _workerRepository;

    public WorkerGetByIdQueryHandler(
        ILogger<WorkerGetByIdQueryHandler> logger,
        IWorkerRepository workerRepository)
    {
        _logger = logger;
        _workerRepository = workerRepository;
    }
    public async Task<Result<Worker?>> Handle(WorkerGetByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started WorkerGetByIdQueryHandler");

        try
        {
            var worker = await _workerRepository.GetByIdAsync(request.WorkerId, cancellationToken);

            if (worker == null)
            {
                _logger.LogWarning("WorkerGetByIdQueryHandler: Worker doesn't exist!");
                return Result.Failure<Worker?>(new Error(
                "Worker.NotFound",
                $"The Worker with Id {request.WorkerId} was not found"));
            }

            _logger.LogInformation("Finished WorkerGetByIdQueryHandler");
            return worker;


        }
        catch (Exception ex)
        {
            _logger.LogError("WorkerGetByIdQueryHandler error: {0}", ex.Message);
            return Result.Failure<Worker?>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
