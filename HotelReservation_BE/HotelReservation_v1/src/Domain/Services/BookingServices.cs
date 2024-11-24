using Domain.Entities;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class BookingServices : IBookingServices
    {
        public bool IsBookingServices(Booking booking)
        {
            return booking.PeopleNumber > 0 && booking.ReservationDate > DateTime.Now;
        }
    }
}
