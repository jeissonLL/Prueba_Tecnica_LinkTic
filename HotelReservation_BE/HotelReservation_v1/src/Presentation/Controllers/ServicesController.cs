using Application.Services.Command;
using Application.Services.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ServicesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ServicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateServices([FromBody] CreateServicesCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateService([FromBody] UpdateServicesCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteService([FromBody] DeleteServicesCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetListService()
        {
            try
            {
                var service = await _mediator.Send(new GetServicesListQuery());
                return Ok(service);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al obtener la lista de servicios.", Details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            try
            {
                var service = await _mediator.Send(new GetServicesByIdQuery(id));

                if (service == null)
                {
                    return NotFound(new { Message = $"Servicio con ID {id} no encontrado." });
                }

                return Ok(service);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al obtener el servicio.", Details = ex.Message });
            }
        }
    }
}
