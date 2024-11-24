using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;

namespace Application.Services.Command
{
    public readonly record struct CreateServicesCommand(
        string ServiceName, 
        string ServiceDescription, 
        int Price) : IRequest<string>;

    public class CreateServicesCommandHandle : IRequestHandler<CreateServicesCommand, string>
    {
        private readonly AplicationDbContext _context;

        public CreateServicesCommandHandle(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(CreateServicesCommand request, CancellationToken ct)
        {
            var service = new Service
            {
                ServiceName = request.ServiceName,
                ServiceDescription = request.ServiceDescription,
                Price = request.Price,
            };

            _context.Add(service);
            await _context.SaveChangesAsync(ct);

            return service?.ToString() ?? string.Empty;
        }
    }
}
