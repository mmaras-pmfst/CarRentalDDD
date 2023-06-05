using Application.Abstractions;
using Domain.Repositories;
using Domain.ReservationContract;
using Domain.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ReservationContracts.Update;
internal sealed class ReservationContractUpdateCommandHandler : ICommandHandler<ReservationContractUpdateCommand, bool>
{
    private ILogger<ReservationContractUpdateCommandHandler> _logger;
    private IReservationContractRepository _reservationContractRepository;
    private IUnitOfWork _unitOfWork;

    public ReservationContractUpdateCommandHandler(ILogger<ReservationContractUpdateCommandHandler> logger, IReservationContractRepository reservationContractRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _reservationContractRepository = reservationContractRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(ReservationContractUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started ReservationContractUpdateCommandHandler");

        try
        {
            throw new NotImplementedException();
            _logger.LogInformation("Finished ReservationContractUpdateCommandHandler");

        }
        catch (Exception ex)
        {
            _logger.LogError("ReservationContractUpdateCommandHandler error: {0}", ex.Message);
            return Result.Failure<bool>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
