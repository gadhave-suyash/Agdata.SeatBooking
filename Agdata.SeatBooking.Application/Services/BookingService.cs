using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agdata.SeatBooking.Application.Interfaces;
using Agdata.SeatBooking.Data;
using Agdata.SeatBooking.Domain.Entities;

namespace Agdata.SeatBooking.Application.Services
{
    public class BookingService : IBookingService
    {
        public int BookSeat(int seatId, int employeeId)
        {
            using (var context = new SeatBookingContext())
            {
                var seat = context.Seats.Find(seatId);
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
                    SeatId = seatId,
                    EmployeeId = employeeId,
                    BookingDate = DateTime.Now
                };
                context.Bookings.Add(booking);
                context.SaveChanges();
                Console.WriteLine($"Booking successful! Your booking ID is {booking.Id}.");
                return booking.Id; // Return the booking ID
            }
        }

        public void RemoveBooking(int bookingId)
        {
            using (var context = new SeatBookingContext())
            {
                var booking = context.Bookings.Find(bookingId);
                if (booking != null)
                {
                    var seat = context.Seats.Find(booking.SeatId);
                    if (seat != null)
                    {
                        seat.IsBooked = false; // Mark the seat as available
                        seat.BookedById = null; // Clear the booking reference
                    }
                    context.Bookings.Remove(booking);
                    context.SaveChanges();
                    Console.WriteLine($"Booking with ID {bookingId} removed successfully.");
                }
                else
                {
                    Console.WriteLine($"Error: Booking with ID {bookingId} does not exist.");
                }
            }
        }

        public List<Booking> GetAllBookings()
        {
            using (var context = new SeatBookingContext())
            {
                return context.Bookings.ToList();
            }
        }

        public List<Booking> GetBookingsByEmployeeId(int employeeId)
        {
            using (var context = new SeatBookingContext())
            {
                var bookings = context.Bookings.Where(b => b.EmployeeId == employeeId).ToList();
                if (bookings.Count == 0)
                {
                    Console.WriteLine($"No bookings found for Employee ID {employeeId}.");
                }
                return bookings;
            }
        }
    }
}
