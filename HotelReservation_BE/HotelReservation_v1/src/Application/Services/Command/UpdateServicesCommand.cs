using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Command
{
    public readonly record struct UpdateServicesCommand(
        int ServiceId, 
        string NameService, 
        string ServiceDescription,
        int Price) : IRequest<string>;

    public class UpdateServicesCommandHandle : IRequestHandler<UpdateServicesCommand, string>
    {
        private readonly AplicationDbContext _context;

        public UpdateServicesCommandHandle(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(UpdateServicesCommand request, CancellationToken ct)
        {
            var updateService = await _context.Services
                .FirstOrDefaultAsync(c => c.ServiceId == request.ServiceId, ct);

            if (updateService == null)
            {
                throw new KeyNotFoundException($"El servicio con ID {request.ServiceId} no encontrado.");
            }

            updateService.ServiceName = request.NameService;
            updateService.ServiceDescription = request.ServiceDescription;
            updateService.Price = request.Price;

            await _context.SaveChangesAsync(ct);

            return updateService.ServiceId.ToString();
        }
    }
}
