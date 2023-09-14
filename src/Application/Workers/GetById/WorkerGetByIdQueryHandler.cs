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
    private readonly IOfficeRepository _officeRepository;

    public WorkerGetByIdQueryHandler(ILogger<WorkerGetByIdQueryHandler> logger, IOfficeRepository officeRepository)
    {
        _logger = logger;
        _officeRepository = officeRepository;
    }
    public async Task<Result<Worker?>> Handle(WorkerGetByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started WorkerGetByIdQueryHandler");
        _logger.LogInformation("Finished WorkerGetByIdQueryHandler");

        try
        {
            var officeWorkers = await _officeRepository.GetAllAsync(cancellationToken);

            var worker = officeWorkers
                            .SelectMany(x => x.Workers)
                            .Where(x => x.Id == request.WorkerId)
                            .SingleOrDefault();

            if(worker == null)
            {
                _logger.LogWarning("WorkerGetByIdQueryHandler: Worker doesn't exist!");
                return Result.Failure<Worker?>(new Error(
                "Worker.NotFound",
                $"The Worker with Id {request.WorkerId} was not found"));
            }

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
