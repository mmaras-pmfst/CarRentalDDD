using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ReservationContracts.Delete;

internal sealed class ReservationContractDeleteCommandHandler : IRequestHandler<ReservationContractDeleteCommand, Unit>
{
    private ILogger<ReservationContractDeleteCommandHandler> _logger;
    private readonly IReservationContractRepository _reservationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReservationContractDeleteCommandHandler(ILogger<ReservationContractDeleteCommandHandler> logger, IReservationContractRepository reservationRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _reservationRepository = reservationRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Unit> Handle(ReservationContractDeleteCommand request, CancellationToken cancellationToken)
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
