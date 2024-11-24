using Application.DTO;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Bookings.Querys
{
    public readonly record struct GetBookingByIdQuery(int BookingId) : IRequest<BookingDTO>;
    public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, BookingDTO>
    {
        private readonly AplicationDbContext _context;

        public GetBookingByIdQueryHandler(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BookingDTO> Handle(GetBookingByIdQuery request, CancellationToken ct)
        {
            var booking = await _context.Bookings
                .Where(c => c.BookingId == request.BookingId)
                .Select(c => new BookingDTO
                {
                    BookingId = c.BookingId,
                    ReservationDate = c.ReservationDate,
                    PeopleNumber = c.PeopleNumber,
                    Estado = c.Estado,
                    CustomerId = c.CustomerId,
                    ServiceId = c.ServiceId
                })
                .FirstOrDefaultAsync(ct);

            return booking!;
        }
    }
}
