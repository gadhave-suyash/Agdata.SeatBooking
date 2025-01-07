using System.Configuration; // Ensure this is included
using Agdata.SeatBooking.Domain.Entities;
using System.Data.Entity; // Use System.Data.Entity for EF6

namespace Agdata.SeatBooking.Data
{
    public class SeatBookingContext : DbContext
    {
        public SeatBookingContext() : base("name=SeatBookingContext") // Use the name of the connection string
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}