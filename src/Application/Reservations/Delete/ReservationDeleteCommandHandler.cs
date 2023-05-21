using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reservations.Delete;

internal sealed class ReservationDeleteCommandHandler : IRequestHandler<ReservationDeleteCommand, Unit>
{
    private ILogger<ReservationDeleteCommandHandler> _logger;
    private readonly IReservationRepository _reservationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReservationDeleteCommandHandler(ILogger<ReservationDeleteCommandHandler> logger, IReservationRepository reservationRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _reservationRepository = reservationRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Unit> Handle(ReservationDeleteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started ReservationDeleteCommandHandler");

        try
        {
            await _reservationRepository.Delete(request.ReservationId, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Finished ReservationDeleteCommandHandler");
            return Unit.Value;
        }
        catch (Exception ex)
        {

            _logger.LogError("ReservationDeleteCommandHandler error: {0}", ex.Message);
            throw;
        }

    }
}
