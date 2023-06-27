using Application.Abstractions;
using Domain.Management.Car;
using Domain.Repositories;
using Domain.Sales.CarModelRent.Entities;
using Domain.Shared;
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
    private readonly ICarBrandRepository _carBrandRepository;
    private readonly IReservationRepository _reservationRepository;
    private readonly IReservationDetailRepository _reservationDetailRepository;
    private readonly IExtrasRepository _extrasRepository;
    private readonly IOfficeRepository _officeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReservationCreateCommandHandler(ILogger<ReservationCreateCommandHandler> logger, IUnitOfWork unitOfWork, ICarBrandRepository carBrandRepository, IReservationRepository reservationRepository, IReservationDetailRepository reservationDetailRepository, IExtrasRepository extrasRepository, IOfficeRepository officeRepository)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _carBrandRepository = carBrandRepository;
        _reservationRepository = reservationRepository;
        _reservationDetailRepository = reservationDetailRepository;
        _extrasRepository = extrasRepository;
        _officeRepository = officeRepository;
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

            var brands = await _carBrandRepository.GetAllAsync(cancellationToken);
            if (!brands.Any())
            {
                _logger.LogWarning("ReservationCreateCommandHandler: No CarBrands in database");
                return Result.Failure<Guid>(new Error(
                        "CarBrand.NoData",
                        "There are no CarBrands to fetch"));
            }
            var model = brands
                .SelectMany(x => x.CarModels)
                .Where(x => x.CarModelRents
                    .Any(x => x.Id == request.CarModelRentId))
                .SingleOrDefault();

            if (model == null || !model.CarModelRents.Any())
            {
                _logger.LogWarning("ReservationCreateCommandHandler: No CarModels in database");
                return Result.Failure<Guid>(new Error(
                        "CarModels.NoData",
                        "There are no CarModels to fetch"));
            }
            if (!model.CarModelRents.Any())
            {
                _logger.LogWarning("ReservationCreateCommandHandler: CarModelRent doesn't exist!");
                return Result.Failure<Guid>(new Error(
                        "CarModelRent.NotFound",
                         $"The CarModelRent with Id {request.CarModelRentId} was not found"));
            }

            var newReservation = Reservation.Create(
                request.DriverFirstName,
                request.DriverLastName,
                request.Email,
                request.PickUpDate,
                request.DropDownDate,
                request.PickUpLocationId,
                request.DropDownLocationId,
                model.CarModelRents.First());

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
                var newExtra = newReservation.AddReservationDetail(extra.Quantity, dbExtra);
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
