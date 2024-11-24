using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Query
{
    public readonly record struct GetServicesListQuery : IRequest<List<Service>>;
    public class GetServicesListQueryHandler : IRequestHandler<GetServicesListQuery, List<Service>>
    {
        private readonly AplicationDbContext _context;

        public GetServicesListQueryHandler(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Service>> Handle(GetServicesListQuery request, CancellationToken ct)
        {
            var service = await _context.Services.ToListAsync(ct);
            return service;
        }
    }
}
