using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agdata.SeatBooking.Application.Interfaces;
using Agdata.SeatBooking.Domain.Entities;

namespace Agdata.SeatBooking.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly List<Booking> _bookings = new List<Booking>();
        private readonly List<Seat> _seats;

        // Static variable to keep track of the next booking ID
        private static int _nextBookingId = 1;

        public BookingService(List<Seat> seats)
        {
            _seats = seats;
        }

        public int BookSeat(int seatId, int employeeId)
        {
            var seat = _seats.FirstOrDefault(s => s.Id == seatId);
            if (seat == null)
            {
                Console.WriteLine($"Error: Seat with ID {seatId} does not exist.");
                return -1; // Indicate failure
            }

            if (seat.IsBooked)
            {
                Console.WriteLine($"Error: Seat with ID {seatId} is already booked.");
                return -1; // Indicate failure
            }

            seat.IsBooked = true;
            seat.BookedById = employeeId; // Set the BookedById to the employee's ID
            var booking = new Booking
            {
                Id = _nextBookingId++, // Use the current booking ID and increment it
                SeatId = seatId,
                EmployeeId = employeeId,
                BookingDate = DateTime.Now
            };
            _bookings.Add(booking);
            Console.WriteLine($"Booking successful! Your booking ID is {booking.Id}.");
            return booking.Id; // Return the booking ID
        }

        public void RemoveBooking(int bookingId)
        {
            var booking = _bookings.FirstOrDefault(b => b.Id == bookingId);
            if (booking != null)
            {
                var seat = _seats.FirstOrDefault(s => s.Id == booking.SeatId);
                if (seat != null)
                {
                    seat.IsBooked = false; // Mark the seat as available
                    seat.BookedById = null; // Clear the booking reference
                }
                _bookings.Remove(booking);
                Console.WriteLine($"Booking with ID {bookingId} removed successfully.");
            }
            else
            {
                Console.WriteLine($"Error: Booking with ID {bookingId} does not exist.");
            }
        }

        public List<Booking> GetAllBookings()
        {
            return _bookings;
        }

        public List<Booking> GetBookingsByEmployeeId(int employeeId)
        {
            var bookings = _bookings.Where(b => b.EmployeeId == employeeId).ToList();
            if (bookings.Count == 0)
            {
                Console.WriteLine($"No bookings found for Employee ID {employeeId}.");
            }
            return bookings;
        }
    }
}
