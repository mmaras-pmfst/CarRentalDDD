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

namespace Application.Reservations.GetById;
internal class ReservationGetByIdQueryHandler : IQueryHandler<ReservationGetByIdQuery, Reservation?>
{
    private ILogger<ReservationGetByIdQueryHandler> _logger;
    private readonly IReservationRepository _reservationRepository;

    public ReservationGetByIdQueryHandler(
        ILogger<ReservationGetByIdQueryHandler> logger,
        IReservationRepository reservationRepository)
    {
        _logger = logger;
        _reservationRepository = reservationRepository;
    }

    public async Task<Result<Reservation?>> Handle(ReservationGetByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started ReservationGetByCustomerQueryHandler");

        try
        {
            var reservation = await _reservationRepository.GetByIdAsync(request.ReservationId, cancellationToken);

            if (reservation == null || reservation is null)
            {
                _logger.LogWarning("ReservationGetByCustomerQueryHandler: Reservation doesn't exist!");
                return Result.Failure<Reservation?>(new Error(
                "Reservation.NotFound",
                $"The Reservation with Id {request.ReservationId} was not found"));
            }

            _logger.LogInformation("Finished ReservationGetByCustomerQueryHandler");
            return reservation;
        }
        catch (Exception ex)
        {
            _logger.LogError("ReservationGetByCustomerQueryHandler error: {0}", ex.Message);
            return Result.Failure<Reservation?>(new Error(
                    "Error",
                    ex.Message));
        }
    }
}
