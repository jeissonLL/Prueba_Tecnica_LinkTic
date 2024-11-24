using Application.Bookings.Command;
using Application.Bookings.Querys;
using Application.Customers.Command;
using Application.Customers.Query;
using Application.Customers.Querys;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBooking([FromBody] UpdateBookingCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBooking([FromBody] DeleteBookingCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetListBooking()
        {
            try
            {
                var booking = await _mediator.Send(new GetBookingListQuery());
                return Ok(booking);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al obtener la lista de reservas.", Details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            try
            {
                var booking = await _mediator.Send(new GetBookingByIdQuery(id));

                if (booking == null)
                {
                    return NotFound(new { Message = $"La reservacion con ID {id} no encontrado." });
                }

                return Ok(booking);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al obtener la reservacion.", Details = ex.Message });
            }
        }
    }
}
