using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agdata.SeatBooking.Domain.Entities
{
    public class Seat
    {
        public int Id { get; set; }
        public string SeatNumber { get; set; }
        public bool IsBooked { get; set; }
        public int? BookedById { get; set; }
    }
}
