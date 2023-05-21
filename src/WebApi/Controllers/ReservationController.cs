using Application.Reservations.Create;
using Application.Reservations.Delete;
using Application.Reservations.GetAll;
using Application.Reservations.GetById;
using Domain.CarBrand.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.Reservations;

namespace WebApi.Controllers
{
    [Route("api/reservation")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private ILogger<ReservationController> _logger;
        private ISender _sender;

        public ReservationController(ILogger<ReservationController> logger, ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        [HttpPost]
        public async Task Create(CreateReservationRequest request)
        {
            _logger.LogInformation("Started ReservationController.Create");
            var command = new ReservationCreateCommand(request.PickUpDate, request.DropDownDate, request.CarModelId, request.PickupLocationId, request.DropDownLocationId);

            var response = await _sender.Send(command);

            _logger.LogInformation("Finished ReservationController.Create");

        }

        [Route("getall")]
        [HttpGet]
        public async Task<List<Reservation>> GetAll()
        {
            _logger.LogInformation("Started ReservationController.GetAll");

            var command = new ReservationGetAllCommand();

            var response = await _sender.Send(command);

            _logger.LogInformation("Finished ReservationController.GetAll");

            return response;

        }

        [Route("{id}")]
        [HttpGet]
        public async Task<Reservation> GetById(Guid id)
        {
            _logger.LogInformation("Started ReservationController.GetById");

            var command = new ReservationGetByIdCommand(id);

            var response = await _sender.Send(command);

            _logger.LogInformation("Finished ReservationController.GetById");

            return response;

        }

        [Route("{id}")]
        [HttpDelete]
        public async Task Delete(Guid id)
        {
            _logger.LogInformation("Started ReservationController.Delete");

            var command = new ReservationDeleteCommand(id);

            var response = await _sender.Send(command);

            _logger.LogInformation("Finished ReservationController.Delete");

        }
    }
}
