using Application.ReservationContracts.Create;
using Application.ReservationContracts.GetAll;
using Application.ReservationContracts.GetById;
using Application.ReservationContracts.Update;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstractions;
using WebApi.Contracts.ReservationContracts;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationContractController : ApiController
    { 
        private ILogger<ReservationContractController> _logger;

        public ReservationContractController(ILogger<ReservationContractController> logger, ISender sender)
            :base(sender)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReservationContractRequest request)
        {
            _logger.LogInformation("Started ReservationContractController.Create");

            var command = new ReservationContractCreateCommand(request.DriverFirstName,
                request.DriverLastName,
                request.Email,
                request.PickUpDate,
                request.DropDownDate,
                request.CarModelId,
                request.PickupLocationId,
                request.DropDownLocationId);

            var response = await Sender.Send(command);

            _logger.LogInformation("Finished ReservationContractController.Create");

            if (response.IsFailure)
            {
                return HandleFailure(response);
            }
            return CreatedAtAction(
                nameof(Create),
                new { id = response.Value },
                response.Value);

        }

        [Route("getall")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Started ReservationContractController.GetAll");

            var command = new ReservationContractGetAllQuery();

            var response = await Sender.Send(command);

            _logger.LogInformation("Finished ReservationContractController.GetAll");

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);

        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("Started ReservationContractController.GetById");

            var command = new ReservationContractGetByIdQuery(id);

            var response = await Sender.Send(command);  

            _logger.LogInformation("Finished ReservationContractController.GetById");

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);

        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateReservationContractRequest request)
        {
            _logger.LogInformation("Started ReservationContractController.Update");

            var command = new ReservationContractUpdateCommand(request.ReservationContractId,
                request.DriverFirstName,
                request.DriverLastName,
                request.Email,
                request.PickUpDate,
                request.DropDownDate,
                request.PickupLocationId,
                request.DropDownLocationId,
                request.DriverLicenceNumber,
                request.DriverIdentificationNumber,
                request.CardType,
                request.PaymentMethod,
                request.CardName,
                request.CardNumber,
                request.CVV,
                request.CardDateExpiration,
                request.CardYearExpiration);

            var response = await Sender.Send(command);  

            _logger.LogInformation("Finished ReservationContractController.Update");
            if (response.IsFailure)
            {
                return HandleFailure(response);
            }
            return CreatedAtAction(
                nameof(Update),
                new { id = response.Value },
                response.Value);
        }
    }
}
