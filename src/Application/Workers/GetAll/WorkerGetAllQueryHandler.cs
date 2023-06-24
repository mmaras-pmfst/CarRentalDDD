using Application.Abstractions;
using Application.Workers.GetById;
using Domain.Management.Office.Entities;
using Domain.Repositories;
using Domain.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Workers.GetAll;
internal class WorkerGetAllQueryHandler : IQueryHandler<WorkerGetAllQuery, List<Worker>>
{
    private ILogger<WorkerGetAllQueryHandler> _logger;
    private readonly IOfficeRepository _officeRepository;

    public WorkerGetAllQueryHandler(ILogger<WorkerGetAllQueryHandler> logger, IOfficeRepository officeRepository)
    {
        _logger = logger;
        _officeRepository = officeRepository;
    }

    public async Task<Result<List<Worker>>> Handle(WorkerGetAllQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started WorkerGetAllQueryHandler");

        try
        {
            var officeWorkers = await _officeRepository.GetAllAsync(cancellationToken);

            if (officeWorkers.SelectMany(x => x.Workers) == null)
            {
                _logger.LogWarning("WorkerGetAllQueryHandler: No Workers in database");
                return Result.Failure<List<Worker>>(new Error(
                        "Workers.NoData",
                        "There are no Workers to fetch"));
            }

            var workers = officeWorkers
                            .SelectMany(x => x.Workers)
                            .ToList();

            _logger.LogInformation("Finished WorkerGetAllQueryHandler");
            return workers;
        }
        catch (Exception ex)
        {
            _logger.LogError("WorkerGetAllQueryHandler error: {0}", ex.Message);
            return Result.Failure<List<Worker>>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
