using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Bookings.Command
{
    public readonly record struct DeleteBookingCommand(int BookingId) : IRequest<string>;
    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, string>
    {
        private readonly AplicationDbContext _context;

        public DeleteBookingCommandHandler(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(DeleteBookingCommand request, CancellationToken ct)
        {
            var booking = await _context.Bookings
                .FirstOrDefaultAsync(c => c.BookingId == request.BookingId, ct);

            if (booking == null)
            {
                throw new KeyNotFoundException($"La reservacion con ID {request.BookingId} no encontrado.");
            }

            _context.Bookings.Remove(booking);

            await _context.SaveChangesAsync(ct);

            return booking.BookingId.ToString();
        }
    }
}
