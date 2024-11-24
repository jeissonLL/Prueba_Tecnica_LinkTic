using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Bookings.Command
{
    public readonly record struct UpdateBookingCommand(
       int BookingId,
       DateTime ReservationDate,
       int PeopleNumber,
       string Estado,
       int CustomerId,
       int ServiceId) : IRequest<string>;

    public class UpdateBookingCommandHandle : IRequestHandler<UpdateBookingCommand, string>
    {
        private readonly AplicationDbContext _context;

        public UpdateBookingCommandHandle(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(UpdateBookingCommand request, CancellationToken ct)
        {
            var updateBooking = await _context.Bookings
                .FirstOrDefaultAsync(c => c.BookingId == request.BookingId, ct);

            if (updateBooking == null)
            {
                throw new KeyNotFoundException($"La reservacion con ID {request.ServiceId} no encontrado.");
            }

            updateBooking.ReservationDate = request.ReservationDate;
            updateBooking.PeopleNumber = request.PeopleNumber;
            updateBooking.Estado = request.Estado;
            updateBooking.CustomerId = request.CustomerId;
            updateBooking.ServiceId = request.ServiceId;
            

            await _context.SaveChangesAsync(ct);

            return updateBooking.ServiceId.ToString();
        }
    }
}
