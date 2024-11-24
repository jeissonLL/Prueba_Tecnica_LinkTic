using Application.DTO;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Bookings.Querys
{
    public readonly record struct GetBookingListQuery : IRequest<List<Booking>>;
    public class GetBookingListQueryHandler : IRequestHandler<GetBookingListQuery, List<Booking>>
    {
        private readonly AplicationDbContext _context;

        public GetBookingListQueryHandler(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Booking>> Handle(GetBookingListQuery request, CancellationToken ct)
        {
            var booking = await _context.Bookings.ToListAsync(ct);
            return booking;
        }
    }
}
