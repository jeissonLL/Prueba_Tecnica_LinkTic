using Application.DTO;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customers.Querys
{
    public readonly record struct GetCustomerByIdQuery(int CustomerId) : IRequest<CustomerDTO>;
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDTO>
    {
        private readonly AplicationDbContext _context;

        public GetCustomerByIdQueryHandler(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerDTO> Handle(GetCustomerByIdQuery request, CancellationToken ct)
        {
            // Buscar al cliente por ID
            var customer = await _context.Customers
                .Where(c => c.CustomerId == request.CustomerId)
                .Select(c => new CustomerDTO
                {
                    CustomerId = c.CustomerId,
                    Name = c.Name,
                    Email = c.Email
                })
                .FirstOrDefaultAsync(ct);

            return customer!;
        }
    }
}
