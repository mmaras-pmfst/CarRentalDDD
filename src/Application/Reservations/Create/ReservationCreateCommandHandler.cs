using Application.Abstractions;
using Domain.Errors;
using Domain.Management.Cars;
using Domain.Repositories;
using Domain.Sales.Reservations;
using Domain.Shared;
using Domain.Shared.ValueObjects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reservations.Create;
internal class ReservationCreateCommandHandler : ICommandHandler<ReservationCreateCommand, Guid>
{
    private ILogger<ReservationCreateCommandHandler> _logger;
    private readonly ICarModelRepository _carModelRepository;
    private readonly IReservationRepository _reservationRepository;
    private readonly IReservationItemRepository _reservationDetailRepository;
    private readonly IExtrasRepository _extrasRepository;
    private readonly IOfficeRepository _officeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReservationCreateCommandHandler(
        ILogger<ReservationCreateCommandHandler> logger,
        IUnitOfWork unitOfWork,
        IReservationRepository reservationRepository,
        IReservationItemRepository reservationDetailRepository,
        IExtrasRepository extrasRepository,
        IOfficeRepository officeRepository,
        ICarModelRepository carModelRepository)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _reservationRepository = reservationRepository;
        _reservationDetailRepository = reservationDetailRepository;
        _extrasRepository = extrasRepository;
        _officeRepository = officeRepository;
        _carModelRepository = carModelRepository;
    }

    public async Task<Result<Guid>> Handle(ReservationCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started ReservationCreateCommandHandler");

        try
        {
            var dropDownLocation = await _officeRepository.GetByIdAsync(request.DropDownLocationId, cancellationToken);
            if(dropDownLocation == null)
            {
                _logger.LogWarning("ReservationCreateCommandHandler: DropDownLocation doesn't exist!");
                return Result.Failure<Guid>(new Error(
                    "Office.NotFound",
                    $"The Office with Id {request.DropDownLocationId} was not found"));
            }
            var pickUpLocation = await _officeRepository.GetByIdAsync(request.PickUpLocationId, cancellationToken);
            if(pickUpLocation == null)
            {
                _logger.LogWarning("ReservationCreateCommandHandler: PickUpLocation doesn't exist!");
                return Result.Failure<Guid>(new Error(
                    "Office.NotFound",
                    $"The Office with Id {request.PickUpLocationId} was not found"));
            }

            var carModel = await _carModelRepository.GetByIdAsync(request.CarModelId,cancellationToken);

            if (carModel == null)
            {
                _logger.LogWarning("ReservationCreateCommandHandler: CarModel doesn't exist!");

                return Result.Failure<Guid>(new Error(
                    "CarModel.NotFound",
                    $"The CarModel with Id {request.CarModelId} was not found"));
            }

            var driverFirstName = FirstName.Create(request.DriverFirstName);
            if (driverFirstName.IsFailure)
            {
                return Result.Failure<Guid>(driverFirstName.Error);
            }
            var driverLastName = LastName.Create(request.DriverLastName);
            if (driverLastName.IsFailure)
            {
                return Result.Failure<Guid>(driverLastName.Error);
            }

            var email = Email.Create(request.Email);
            if (email.IsFailure)
            {
                return Result.Failure<Guid>(email.Error);

            }

            var newReservation = Reservation.Create(
                driverFirstName.Value,
                driverLastName.Value,
                email.Value,
                request.PickUpDate,
                request.DropDownDate,
                request.PickUpLocationId,
                request.DropDownLocationId,
                carModel);

            foreach (var extra in request.Extras)
            {
                var dbExtra = await _extrasRepository.GetByIdAsync(extra.ExtraId, cancellationToken);
                if (dbExtra == null)
                {
                    _logger.LogWarning("ReservationCreateCommandHandler: Extra doesn't exist!");
                    return Result.Failure<Guid>(new Error(
                            "Extra.NotFound",
                             $"The Extra with Id {extra.ExtraId} was not found"));
                }
                var newExtra = newReservation.AddItem(extra.Quantity, dbExtra);
                await _reservationDetailRepository.AddAsync(newExtra, cancellationToken);
            }

            await _reservationRepository.AddAsync(newReservation, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Finished ReservationCreateCommandHandler");
            return newReservation.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError("ReservationCreateCommandHandler error: {0}", ex.Message);
            return Result.Failure<Guid>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
