using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Customers.Command;
using Application.Customers.Query;
using Application.Customers.Querys;
using Microsoft.AspNetCore.Authorization;
using Application.Bookings.Command;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id, CancellationToken ct)
        {
            var result = await _mediator.Send(new DeleteCustomerCommand { CustomerId = id }, ct);
            return Ok(new { message = "Cliente eliminada exitosamente." });
            
        }

        [HttpGet]
        public async Task<IActionResult> GetListCustomer()
        {
            try
            {
                var customers = await _mediator.Send(new GetCustomerListQuery ());
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al obtener la lista de clientes.", Details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            try
            {
                var customer = await _mediator.Send(new GetCustomerByIdQuery(id));

                if (customer == null)
                {
                    return NotFound(new { Message = $"Cliente con ID {id} no encontrado." });
                }

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al obtener el cliente.", Details = ex.Message });
            }
        }

    }
}
