using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }
        public DateTime ReservationDate { get; set; }
        public int PeopleNumber { get; set; }
        public string Estado { get; set; } = string.Empty;
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer CustomerBooking { get; set; } = null!;

        [ForeignKey("ServiceId")]
        public virtual Service ServiceBoking { get; set; } = null!;
    }
}
