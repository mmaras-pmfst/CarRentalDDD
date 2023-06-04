using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstractions;

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
        public async Task<IActionResult> Create()
        {
            _logger.LogInformation("Started ReservationContractController.Create");



            _logger.LogInformation("Finished ReservationContractController.Create");

            return null;

        }

        [Route("getall")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Started ReservationContractController.GetAll");



            _logger.LogInformation("Finished ReservationContractController.GetAll");

            return null;
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("Started ReservationContractController.GetById");



            _logger.LogInformation("Finished ReservationContractController.GetById");

            return null;
        }

        [HttpPost]
        public async Task<IActionResult> Update()
        {
            _logger.LogInformation("Started ReservationContractController.Update");



            _logger.LogInformation("Finished ReservationContractController.Update");

            return null;
        }
    }
}
