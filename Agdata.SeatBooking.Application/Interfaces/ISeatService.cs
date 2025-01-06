using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agdata.SeatBooking.Domain.Entities;

namespace Agdata.SeatBooking.Application.Interfaces
{
    public interface ISeatService
    {
        void AddSeat(Seat seat);
        void RemoveSeat(int seatId);
        List<Seat> GetAllSeats();
        Seat GetSeatById(int seatId);
    }
}
