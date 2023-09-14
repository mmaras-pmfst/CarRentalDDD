using Application.Abstractions;
using Domain.Repositories;
using Domain.Sales.Reservations;
using Domain.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reservations.GetAll;
internal class ReservationGetAllQueryHandler : IQueryHandler<ReservationGetAllQuery, List<Reservation>>
{
    private ILogger<ReservationGetAllQueryHandler> _logger;
    private readonly IReservationRepository _reservationRepository;

    public ReservationGetAllQueryHandler(
        ILogger<ReservationGetAllQueryHandler> logger, 
        IReservationRepository reservationRepository)
    {
        _logger = logger;
        _reservationRepository = reservationRepository;
    }

    public async Task<Result<List<Reservation>>> Handle(ReservationGetAllQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started ReservationGetAllQueryHandler");

        try
        {

            var reservations = await _reservationRepository.GetAllAsync(request.DateFrom, request.DateTo, cancellationToken);


            if (!reservations.Any())
            {
                _logger.LogWarning("ReservationGetAllQueryHandler: No Reservation in database");
                return Result.Failure<List<Reservation>>(new Error(
                        "Reservation.NoData",
                        "There are no Reservations to fetch"));
            }
            _logger.LogInformation("Finished ReservationGetAllQueryHandler");
            return reservations;

        }
        catch (Exception ex)
        {
            _logger.LogError("ReservationGetAllQueryHandler error: {0}", ex.Message);
            return Result.Failure<List<Reservation>>(new Error(
                    "Error",
                    ex.Message));
        }
    }
}
