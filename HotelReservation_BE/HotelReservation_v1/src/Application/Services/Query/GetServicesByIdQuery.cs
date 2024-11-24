using Application.DTO;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Query
{
    public readonly record struct GetServicesByIdQuery(int ServiceId) : IRequest<ServicesDTO>;
    public class GetServicesByIdQueryHandler : IRequestHandler<GetServicesByIdQuery, ServicesDTO>
    {
        private readonly AplicationDbContext _context;

        public GetServicesByIdQueryHandler(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServicesDTO> Handle(GetServicesByIdQuery request, CancellationToken ct)
        {
            var service = await _context.Services
                .Where(c => c.ServiceId == request.ServiceId)
                .Select(c => new ServicesDTO
                {
                    ServiceId = c.ServiceId,
                    ServiceName = c.ServiceName,
                    ServiceDescription = c.ServiceDescription,
                    Price = c.Price
                })
                .FirstOrDefaultAsync(ct);

            return service!;
        }
    }
}
