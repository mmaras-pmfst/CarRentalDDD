using Application.ReservationContracts.Create;
using Application.ReservationContracts.Delete;
using Application.ReservationContracts.GetAll;
using Application.ReservationContracts.GetById;
using Application.ReservationContracts.Update;
using Domain.CarBrand.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.ReservationContracts;

namespace WebApi.Controllers
{
    [Route("api/reservation")]
    [ApiController]
    public class ReservationContractController : ControllerBase
    {
        private ILogger<ReservationContractController> _logger;
        private ISender _sender;

        public ReservationContractController(ILogger<ReservationContractController> logger, ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        [HttpPost]
        public async Task Create(CreateReservationContractRequest request)
        {
            _logger.LogInformation("Started ReservationController.Create");
            var command = new ReservationContractCreateCommand( request.DriverFirstName, request.DriverLastName, request.PickUpDate, request.DropDownDate, request.CarModelId, request.PickupLocationId, request.DropDownLocationId);

            var response = await _sender.Send(command);

            _logger.LogInformation("Finished ReservationController.Create");

        }

        [Route("getall")]
        [HttpGet]
        public async Task<List<ReservationContract>> GetAll()
        {
            _logger.LogInformation("Started ReservationController.GetAll");

            var command = new ReservationContractGetAllCommand();

            var response = await _sender.Send(command);

            _logger.LogInformation("Finished ReservationController.GetAll");

            return response;

        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ReservationContract> GetById(Guid id)
        {
            _logger.LogInformation("Started ReservationController.GetById");

            var command = new ReservationContractGetByIdCommand(id);

            var response = await _sender.Send(command);

            _logger.LogInformation("Finished ReservationController.GetById");

            return response;

        }

        [Route("{id}")]
        [HttpDelete]
        public async Task Delete(Guid id)
        {
            _logger.LogInformation("Started ReservationController.Delete");

            var command = new ReservationContractDeleteCommand(id);

            var response = await _sender.Send(command);

            _logger.LogInformation("Finished ReservationController.Delete");

        }

        [HttpPut]
        public async Task Update(UpdateReservationContractRequest request)
        {
            var command = new ReservationContractUpdateCommand(request.ReservationContractId, request.DriverFirstName, request.DriverLastName, request.PickUpDate, request.DropDownDate, request.PickupLocationId, request.DropDownLocationId, request.DriverLicenceNumber, request.DriverIdentificationNumber, request.CardType, request.PaymentMethod, request.CardName, request.CardNumber, request.CVV, request.CardDateExpiration, request.CardYearExpiration);

            var response = await _sender.Send(command);

        }
    }
}
