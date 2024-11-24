using MediatR;
using Infrastructure.Persistence;
using Domain.Entities;

namespace Application.Bookings.Command
{
    public readonly record struct CreateBookingCommand(
        DateTime ReservationDate, 
        int PeopleNumber,
        string Estado,
        int CustomerId,
        int ServiceId) : IRequest<string>;

    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, string>
    {
        private readonly AplicationDbContext _context;

        public CreateBookingCommandHandler(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = new Booking
            {
                ReservationDate = request.ReservationDate,
                PeopleNumber = request.PeopleNumber,
                Estado = request.Estado,
                CustomerId = request.CustomerId,
                ServiceId = request.ServiceId
            };

            _context.Add(booking);
            await _context.SaveChangesAsync(cancellationToken);

            return booking?.ToString() ?? string.Empty;
        }
    }
}
