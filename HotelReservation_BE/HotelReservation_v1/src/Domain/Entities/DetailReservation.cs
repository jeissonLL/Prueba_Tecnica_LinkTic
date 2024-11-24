using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DetailReservation
    {
        public int DetailReservationId { get; set; }

        public int BookingId {  get; set; }

        [ForeignKey("BookingId")]
        public virtual Booking Details { get; set; } = null!;
    }
}
