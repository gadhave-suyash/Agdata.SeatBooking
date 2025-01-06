using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agdata.SeatBooking.Domain.Entities;

namespace Agdata.SeatBooking.Application.Interfaces
{
    public interface IBookingService
    {
        int BookSeat(int seatId, int employeeId);
        void RemoveBooking(int bookingId);
        List<Booking> GetAllBookings();
        List<Booking> GetBookingsByEmployeeId(int employeeId);
    }
}
