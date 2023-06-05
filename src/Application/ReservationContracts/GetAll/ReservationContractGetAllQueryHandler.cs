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

namespace Application.ReservationContracts.GetAll;
internal sealed class ReservationContractGetAllQueryHandler : IQueryHandler<ReservationContractGetAllQuery, List<ReservationContract>>
{
    private ILogger<ReservationContractGetAllQueryHandler> _logger;
    private IReservationContractRepository _reservationContractRepository;

    public ReservationContractGetAllQueryHandler(ILogger<ReservationContractGetAllQueryHandler> logger, IReservationContractRepository reservationContractRepository)
    {
        _logger = logger;
        _reservationContractRepository = reservationContractRepository;
    }

    public async Task<Result<List<ReservationContract>>> Handle(ReservationContractGetAllQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started ReservationContractGetAllQueryHandler");

        try
        {

            throw new NotImplementedException();
            _logger.LogInformation("Finished ReservationContractGetAllQueryHandler");
        }
        catch (Exception ex)
        {
            _logger.LogError("ReservationContractGetAllQueryHandler error: {0}", ex.Message);
            return Result.Failure<List<ReservationContract>>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
