using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agdata.SeatBooking.Application.Interfaces;
using Agdata.SeatBooking.Domain.Entities;

namespace Agdata.SeatBooking.Application.Services
{
    public class SeatService : ISeatService
    {
        private readonly List<Seat> _seats = new List<Seat>();
        private static int _nextSeatId = 1; // Static variable for auto-incrementing SeatId

        public void AddSeat(Seat seat)
        {
            seat.Id = _nextSeatId++; // Assign and increment SeatId
            _seats.Add(seat);
            Console.WriteLine($"Seat {seat.SeatNumber} added with ID {seat.Id}.");
        }

        public void RemoveSeat(int seatId)
        {
            var seat = _seats.FirstOrDefault(s => s.Id == seatId);
            if (seat != null)
            {
                if (seat.IsBooked)
                {
                    Console.WriteLine($"Error: Seat with ID {seatId} cannot be removed because it is already booked.");
                }
                else
                {
                    _seats.Remove(seat);
                    Console.WriteLine($"Seat with ID {seatId} removed.");
                }
            }
            else
            {
                Console.WriteLine($"Error: Seat with ID {seatId} does not exist.");
            }
        }

        public List<Seat> GetAllSeats()
        {
            return _seats;
        }

        public Seat GetSeatById(int seatId)
        {
            return _seats.FirstOrDefault(s => s.Id == seatId);
        }
    }
}
