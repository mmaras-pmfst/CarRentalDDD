using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ReservationContracts.Update;

internal sealed class ReservationContractUpdateCommandHandler : IRequestHandler<ReservationContractUpdateCommand, Unit>
{
    private ILogger<ReservationContractUpdateCommandHandler> _logger;
    private readonly ICarBrandRepository _carBrandRepository;
    private readonly IReservationContractRepository _reservationContractRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReservationContractUpdateCommandHandler(ILogger<ReservationContractUpdateCommandHandler> logger, ICarBrandRepository carBrandRepository, IReservationContractRepository reservationContractRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _carBrandRepository = carBrandRepository;
        _reservationContractRepository = reservationContractRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(ReservationContractUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started ReservationContractUpdateCommandHandler");

        try
        {
            var dbBrands = await _carBrandRepository.GetAllAsync(cancellationToken);
            var carModelWithReservationContract = dbBrands
                .SelectMany(x => x.CarModels)
                .Where(x => x.ReservationContracts.Any(x => x.Id == request.ReservationContractId)).FirstOrDefault();


            if (carModelWithReservationContract == null)
            {
                return Unit.Value;
            }

            carModelWithReservationContract.ReservationContracts.First().Update(carModelWithReservationContract,
                 request.DriverFirstName, request.DriverLastName, request.PickUpDate, request.DropDownDate, request.PickupLocationId, request.DropDownLocationId, request.CarId, request.DriverLicenceNumber, request.DriverIdentificationNumber, request.CardType, request.PaymentMethod, request.CardName, request.CardNumber, request.CVV, request.CardDateExpiration, request.CardYearExpiration);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Finished ReservationContractUpdateCommandHandler");
            return Unit.Value;

        }
        catch (Exception ex)
        {

            _logger.LogError("ReservationContractUpdateCommandHandler error: {0}", ex.Message);
            throw;
        }
    }
}
