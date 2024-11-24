using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class BookingDTO
    {
        public int BookingId { get; set; }
        public DateTime ReservationDate { get; set; }
        public int PeopleNumber { get; set; }
        public string Estado { get; set; } = string.Empty;
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
    }
}
