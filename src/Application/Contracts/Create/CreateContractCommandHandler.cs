using Application.Abstractions;
using Application.Reservations.Create;
using Domain.Repositories;
using Domain.Sales.Contracts;
using Domain.Sales.Reservations;
using Domain.Sales.Reservations.ValueObjects;
using Domain.Shared;
using Domain.Shared.ValueObjects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Create;
internal class CreateContractCommandHandler : ICommandHandler<CreateContractCommand, Guid>
{
    private ILogger<CreateContractCommandHandler> _logger;
    private readonly ICarRepository _carRepository;
    private readonly IContractRepository _contractRepository;
    private readonly IReservationRepository _reservationRepository;
    private readonly IContractItemRepository _contractItemRepository;
    private readonly IExtrasRepository _extrasRepository;
    private readonly IOfficeRepository _officeRepository;
    private readonly IWorkerRepository _workerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateContractCommandHandler(ILogger<CreateContractCommandHandler> logger, ICarRepository carRepository, IContractRepository contractRepository, IContractItemRepository contractItemRepository, IExtrasRepository extrasRepository, IOfficeRepository officeRepository, IUnitOfWork unitOfWork, IReservationRepository reservationRepository, IWorkerRepository workerRepository)
    {
        _logger = logger;
        _carRepository = carRepository;
        _contractRepository = contractRepository;
        _contractItemRepository = contractItemRepository;
        _extrasRepository = extrasRepository;
        _officeRepository = officeRepository;
        _unitOfWork = unitOfWork;
        _reservationRepository = reservationRepository;
        _workerRepository = workerRepository;
    }

    public async Task<Result<Guid>> Handle(CreateContractCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CreateContractCommandHandler");
        try
        {
            var dropDownLocation = await _officeRepository.GetByIdAsync(request.DropDownOfficeId, cancellationToken);
            if (dropDownLocation is null)
            {
                _logger.LogWarning("CreateContractCommandHandler: DropDownLocation doesn't exist!");
                return Result.Failure<Guid>(new Error(
                    "Office.NotFound",
                    $"The Office with Id {request.DropDownOfficeId} was not found"));
            }
            var pickUpLocation = await _officeRepository.GetByIdAsync(request.PickUpOfficeId, cancellationToken);
            if (pickUpLocation is null)
            {
                _logger.LogWarning("CreateContractCommandHandler: PickUpLocation doesn't exist!");
                return Result.Failure<Guid>(new Error(
                    "Office.NotFound",
                    $"The Office with Id {request.PickUpOfficeId} was not found"));
            }

            var worker = await _workerRepository.GetByIdAsync(request.WorkerId, cancellationToken);
            if (worker is null)
            {
                _logger.LogWarning("CreateContractCommandHandler: Worker doesn't exist!");

                return Result.Failure<Guid>(new Error(
                    "Worker.NotFound",
                    $"The Worker with Id {request.WorkerId} was not found"));
            }

            var car = await _carRepository.GetByIdAsync(request.CarId, cancellationToken);
            if (car is null)
            {
                _logger.LogWarning("CreateContractCommandHandler: Car doesn't exist!");

                return Result.Failure<Guid>(new Error(
                    "Car.NotFound",
                    $"The Car with Id {request.CarId} was not found"));
            }
            Reservation? reservation = null;
            if(request.ReservationId != null)
            {
                var reservationDb = await _reservationRepository.GetByIdAsync((Guid)request.ReservationId, cancellationToken);
                if (reservationDb == null)
                {
                    _logger.LogWarning("ReservationCreateCommandHandler: Reservation doesn't exist!");

                    return Result.Failure<Guid>(new Error(
                        "Reservation.NotFound",
                        $"The Reservation with Id {request.ReservationId} was not found"));
                }
                reservation = reservationDb;
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

            var cardResult = Card.Create(
                request.CardName,
                request.CardNumber,
                request.CVV,
                request.CardDateExpiration,
                request.CardYearExpiration,
                request.PaymentMethod);
            if (cardResult.IsFailure)
            {
                return Result.Failure<Guid>(cardResult.Error);

            }

            var newContract = Contract.Create(
                Guid.NewGuid(),
                driverFirstName.Value,
                driverLastName.Value,
                email.Value,
                request.PickUpDate,
                request.DropDownDate,
                pickUpLocation,
                dropDownLocation,
                car,
                request.DriverLicenceNumber,
                request.DriverIdentificationNumber,
                request.CardType,
                request.PaymentMethod,
                cardResult.Value,
                reservation,
                car.CarModel,
                worker);

            foreach (var extra in request.Extras)
            {
                var dbExtra = await _extrasRepository.GetByIdAsync(extra.ExtraId, cancellationToken);
                if (dbExtra is null)
                {
                    _logger.LogWarning("ReservationCreateCommandHandler: Extra doesn't exist!");
                    return Result.Failure<Guid>(new Error(
                            "Extra.NotFound",
                             $"The Extra with Id {extra.ExtraId} was not found"));
                }
                var newExtra = newContract.AddContractDetail(extra.Quantity, dbExtra);
                await _contractItemRepository.AddAsync(newExtra, cancellationToken);
            }

            await _contractRepository.AddAsync(newContract, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Finished CreateContractCommandHandler");
            return newContract.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError("CreateContractCommandHandler error: {0}", ex.Message);
            return Result.Failure<Guid>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
