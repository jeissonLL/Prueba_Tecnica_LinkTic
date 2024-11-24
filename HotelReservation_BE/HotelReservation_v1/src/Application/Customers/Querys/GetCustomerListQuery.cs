using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Customers.Query
{
    public readonly record struct GetCustomerListQuery : IRequest<List<Customer>>;

    public class GetCustomerListQueryHandler : IRequestHandler<GetCustomerListQuery, List<Customer>>
    {
        private readonly AplicationDbContext _context;

        public GetCustomerListQueryHandler(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> Handle(GetCustomerListQuery request, CancellationToken ct)
        {
            var customers = await _context.Customers.ToListAsync(ct);
            return customers;
        }
    }
}
