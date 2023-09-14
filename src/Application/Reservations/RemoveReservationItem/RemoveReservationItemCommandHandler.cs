using Application.Abstractions;
using Application.Reservations.AddReservationItem;
using Domain.Repositories;
using Domain.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reservations.RemoveReservationItem;
internal class RemoveReservationItemCommandHandler : ICommandHandler<RemoveReservationItemCommand, Guid>
{
    private ILogger<AddReservationItemCommandHandler> _logger;
    private readonly IReservationRepository _reservationRepository;
    private readonly IReservationItemRepository _reservationDetailRepository;
    private readonly IExtrasRepository _extrasRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveReservationItemCommandHandler(
        ILogger<AddReservationItemCommandHandler> logger,
        IReservationRepository reservationRepository,
        IReservationItemRepository reservationDetailRepository,
        IExtrasRepository extrasRepository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _reservationRepository = reservationRepository;
        _reservationDetailRepository = reservationDetailRepository;
        _extrasRepository = extrasRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RemoveReservationItemCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started RemoveReservationItemCommandHandler");

        try
        {
            var reservation = await _reservationRepository.GetByIdAsync(request.ReservationId, cancellationToken);
            if (reservation == null)
            {
                _logger.LogWarning("AddReservationItemCommandHandler: Reservation doesn't exist!");
                return Result.Failure<Guid>(new Error(
                    "Reservation.NotFound",
                    $"The Reservation with Id {request.ReservationId} was not found"));
            }

            foreach (var extraId in request.ExtraIds)
            {
                var dbExtra = await _extrasRepository.GetByIdAsync(extraId, cancellationToken);
                if (dbExtra == null)
                {
                    _logger.LogWarning("ReservationCreateCommandHandler: Extra doesn't exist!");
                    return Result.Failure<Guid>(new Error(
                            "Extra.NotFound",
                             $"The Extra with Id {extraId} was not found"));
                }
                reservation.RemoveItem(dbExtra);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Finished RemoveReservationItemCommandHandler");
            return reservation.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError("AddReservationItemCommandHandler error: {0}", ex.Message);
            return Result.Failure<Guid>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
