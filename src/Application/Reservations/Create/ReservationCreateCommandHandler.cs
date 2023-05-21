using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reservations.Create;

internal sealed class ReservationCreateCommandHandler : IRequestHandler<ReservationCreateCommand, Unit>
{
    private ILogger<ReservationCreateCommandHandler> _logger;
    private readonly ICarBrandRepository _carBrandRepository;
    private readonly IReservationRepository _reservationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOfficeRepository _officeRepository;

    public ReservationCreateCommandHandler(ILogger<ReservationCreateCommandHandler> logger, ICarBrandRepository carBrandRepository, IReservationRepository reservationRepository, IUnitOfWork unitOfWork, IOfficeRepository officeRepository)
    {
        _logger = logger;
        _carBrandRepository = carBrandRepository;
        _reservationRepository = reservationRepository;
        _unitOfWork = unitOfWork;
        _officeRepository = officeRepository;
    }

    public async Task<Unit> Handle(ReservationCreateCommand request, CancellationToken cancellationToken)
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
            var reservation = carModel.AddReservation(carModel, request.PickUpDate, request.DropDownDate, request.PickupLocationId, request.DropDownLocationId);

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
