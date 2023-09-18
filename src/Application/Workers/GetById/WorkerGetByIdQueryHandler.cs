using Application.Abstractions;
using Application.Mappings.DtoModels;
using Application.Workers.Update;
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

namespace Application.Workers.GetById;
internal class WorkerGetByIdQueryHandler : IQueryHandler<WorkerGetByIdQuery, WorkerDetailDto?>
{
    private ILogger<WorkerGetByIdQueryHandler> _logger;
    private readonly IWorkerRepository _workerRepository;
    private IMapper _mapper;

    public WorkerGetByIdQueryHandler(
        ILogger<WorkerGetByIdQueryHandler> logger,
        IWorkerRepository workerRepository,
        IMapper mapper)
    {
        _logger = logger;
        _workerRepository = workerRepository;
        _mapper = mapper;
    }
    public async Task<Result<WorkerDetailDto?>> Handle(WorkerGetByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started WorkerGetByIdQueryHandler");

        try
        {
            var worker = await _workerRepository.GetByIdAsync(request.WorkerId, cancellationToken);

            if (worker == null || worker is null)
            {
                _logger.LogWarning("WorkerGetByIdQueryHandler: Worker doesn't exist!");
                return Result.Failure<WorkerDetailDto?>(new Error(
                "Worker.NotFound",
                $"The Worker with Id {request.WorkerId} was not found"));
            }
            var resultDto = _mapper.Map<Worker, WorkerDetailDto>(worker);
            _logger.LogInformation("Finished WorkerGetByIdQueryHandler");
            return resultDto;


        }
        catch (Exception ex)
        {
            _logger.LogError("WorkerGetByIdQueryHandler error: {0}", ex.Message);
            return Result.Failure<WorkerDetailDto?>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
