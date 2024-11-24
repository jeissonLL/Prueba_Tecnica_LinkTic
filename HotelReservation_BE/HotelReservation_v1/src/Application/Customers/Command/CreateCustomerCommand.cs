using MediatR;
using Infrastructure.Persistence;
using Domain.Entities;

namespace Application.Customers.Command
{
    public readonly record struct CreateCustomerCommand(string Name, string Email) : IRequest<string>;
    public class CreateCustomerCommandHandle : IRequestHandler<CreateCustomerCommand, string>
    {
        private readonly AplicationDbContext _context;

        public CreateCustomerCommandHandle(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(CreateCustomerCommand request, CancellationToken ct)
        {
            var customer = new Customer
            {
                Name = request.Name,
                Email = request.Email
            };

            _context.Add(customer);
            await _context.SaveChangesAsync(ct);

            return customer?.ToString() ?? string.Empty;
        }
    }
}
