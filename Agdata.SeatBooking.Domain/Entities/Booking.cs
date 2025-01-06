using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agdata.SeatBooking.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public int SeatId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
