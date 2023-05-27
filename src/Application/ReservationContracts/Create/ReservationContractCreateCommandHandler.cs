using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ReservationContracts.Create;

internal sealed class ReservationContractCreateCommandHandler : IRequestHandler<ReservationContractCreateCommand, Unit>
{
    private ILogger<ReservationContractCreateCommandHandler> _logger;
    private readonly ICarBrandRepository _carBrandRepository;
    private readonly IReservationContractRepository _reservationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOfficeRepository _officeRepository;

    public ReservationContractCreateCommandHandler(ILogger<ReservationContractCreateCommandHandler> logger, ICarBrandRepository carBrandRepository, IReservationContractRepository reservationRepository, IUnitOfWork unitOfWork, IOfficeRepository officeRepository)
    {
        _logger = logger;
        _carBrandRepository = carBrandRepository;
        _reservationRepository = reservationRepository;
        _unitOfWork = unitOfWork;
        _officeRepository = officeRepository;
    }

    public async Task<Unit> Handle(ReservationContractCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started ReservationCreateCommandHandler");

        try
        {

            var carBrands = await _carBrandRepository.GetAllAsync(cancellationToken);
            var carModel = carBrands.SelectMany(x => x.CarModels).Where(x => x.Id == request.CarModelId).FirstOrDefault();
            var pickUpOffice = await _officeRepository.GetByIdAsync(request.PickupLocationId, cancellationToken);
            var dropDownOffce = await _officeRepository.GetByIdAsync(request.DropDownLocationId, cancellationToken);
            if (carModel is null || pickUpOffice is null || dropDownOffce is null)
            {
                return Unit.Value;
            }
            var reservation = carModel.AddReservation(carModel, request.DriverFirstName, request.DriverLastName, request.PickUpDate, request.DropDownDate, request.PickupLocationId, request.DropDownLocationId);

            await _reservationRepository.AddAsync(reservation, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Finished ReservationCreateCommandHandler");
            return Unit.Value;

        }
        catch (Exception ex)
        {

            _logger.LogError("ReservationCreateCommandHandler error: {0}", ex.Message);
            throw;
        }

    }
}
