using Application.Abstractions;
using Domain.Repositories;
using Domain.ReservationContract;
using Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ReservationContracts.GetById;
internal sealed class ReservationContractGetByIdQueryHandler : IQueryHandler<ReservationContractGetByIdQuery, ReservationContract>
{
    private ILogger<ReservationContractGetByIdQueryHandler> _logger;
    private IReservationContractRepository _reservationContractRepository;

    public ReservationContractGetByIdQueryHandler(ILogger<ReservationContractGetByIdQueryHandler> logger, IReservationContractRepository reservationContractRepository)
    {
        _logger = logger;
        _reservationContractRepository = reservationContractRepository;
    }

    public async Task<Result<ReservationContract>> Handle(ReservationContractGetByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started ReservationContractGetByIdQueryHandler");

        try
        {
            throw new NotImplementedException();
            _logger.LogInformation("Finished ReservationContractGetByIdQueryHandler");

        }
        catch (Exception ex)
        {
            _logger.LogError("ReservationContractGetAllQueryHandler error: {0}", ex.Message);
            return Result.Failure<ReservationContract>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
