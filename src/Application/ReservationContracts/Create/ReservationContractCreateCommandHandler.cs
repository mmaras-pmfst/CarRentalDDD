using Application.Abstractions;
using Domain.Repositories;
using Domain.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ReservationContracts.Create;
internal sealed class ReservationContractCreateCommandHandler : ICommandHandler<ReservationContractCreateCommand, Guid>
{
    private ILogger<ReservationContractCreateCommandHandler> _logger;
    private IReservationContractRepository _reservationContractRepository;
    private IUnitOfWork _unitOfWork;

    public ReservationContractCreateCommandHandler(ILogger<ReservationContractCreateCommandHandler> logger, IReservationContractRepository reservationContractRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _reservationContractRepository = reservationContractRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(ReservationContractCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started ReservationContractCreateCommandHandler");

        try
        {
            throw new NotImplementedException();

            _logger.LogInformation("Finished ReservationContractCreateCommandHandler");
        }
        catch (Exception ex)
        {
            _logger.LogError("ReservationContractCreateCommandHandler error: {0}", ex.Message);
            return Result.Failure<Guid>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
