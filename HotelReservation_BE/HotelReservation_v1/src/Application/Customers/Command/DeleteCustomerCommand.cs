using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Customers.Command
{
    public readonly record struct DeleteCustomerCommand(int CustomerId) : IRequest<string>;

    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, string>
    {
        private readonly AplicationDbContext _context;

        public DeleteCustomerCommandHandler(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(DeleteCustomerCommand request, CancellationToken ct)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == request.CustomerId, ct);

            if (customer == null)
            {
                throw new KeyNotFoundException($"Cliente con ID {request.CustomerId} no encontrado.");
            }

            _context.Customers.Remove(customer);

            await _context.SaveChangesAsync(ct);

            return customer.CustomerId.ToString();
        }
    }
}
