using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customers.Command
{
    public readonly record struct UpdateCustomerCommand(
        int CustomerId, 
        string Name, 
        string Email) : IRequest<string>;

    public class UpdateCustomerCommandHandle : IRequestHandler<UpdateCustomerCommand, string>
    {
        private readonly AplicationDbContext _context;

        public UpdateCustomerCommandHandle(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(UpdateCustomerCommand request, CancellationToken ct)
        {
            var updateCustomer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == request.CustomerId, ct);

            if (updateCustomer == null)
            {
                throw new KeyNotFoundException($"Cliente con ID {request.CustomerId} no encontrado.");
            }

            updateCustomer.Name = request.Name;
            updateCustomer.Email = request.Email;

            await _context.SaveChangesAsync(ct);

            return updateCustomer.CustomerId.ToString();
        }
    }
}
