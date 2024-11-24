using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Command
{
    public readonly record struct DeleteServicesCommand(int ServicesId) : IRequest<string>;

    public class DeleteServicesCommandHandler : IRequestHandler<DeleteServicesCommand, string>
    {
        private readonly AplicationDbContext _context;

        public DeleteServicesCommandHandler(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(DeleteServicesCommand request, CancellationToken ct)
        {
            var service = await _context.Services
                .FirstOrDefaultAsync(s => s.ServiceId == request.ServicesId, ct);

            if (service == null)
            {
                throw new KeyNotFoundException($"El Servicio con ID {request.ServicesId} no encontrado.");
            }

            _context.Services.Remove(service);

            await _context.SaveChangesAsync(ct);

            return service.ServiceId.ToString();
        }
    }
}
