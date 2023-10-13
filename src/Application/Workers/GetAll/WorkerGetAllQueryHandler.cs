using Application.Abstractions;
using Application.Mappings.DtoModels;
using Application.Workers.GetById;
using AutoMapper;
using Domain.Management.Workers;
using Domain.Repositories;
using Domain.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Workers.GetAll;
internal class WorkerGetAllQueryHandler : IQueryHandler<WorkerGetAllQuery, List<WorkerDto>>
{
    private ILogger<WorkerGetAllQueryHandler> _logger;
    private readonly IWorkerRepository _workerRepository;
    private IMapper _mapper;

    public WorkerGetAllQueryHandler(
        ILogger<WorkerGetAllQueryHandler> logger,
        IWorkerRepository workerRepository,
        IMapper mapper)
    {
        _logger = logger;
        _workerRepository = workerRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<WorkerDto>>> Handle(WorkerGetAllQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started WorkerGetAllQueryHandler");

        try
        {
            var workers = await _workerRepository.GetAllAsync(cancellationToken);

            if (!workers.Any())
            {
                _logger.LogWarning("WorkerGetAllQueryHandler: No Workers in database");
                return Result.Failure<List<WorkerDto>>(new Error(
                        "Workers.NoData",
                        "There are no Workers to fetch"));
            }
            var resultDto = _mapper.Map<List<Worker>,List<WorkerDto>>(workers);
            _logger.LogInformation("Finished WorkerGetAllQueryHandler");
            return resultDto;
        }
        catch (Exception ex)
        {
            _logger.LogError("WorkerGetAllQueryHandler error: {0}", ex.Message);
            return Result.Failure<List<WorkerDto>>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
