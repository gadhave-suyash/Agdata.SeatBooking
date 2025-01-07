using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Agdata.SeatBooking.Application.Interfaces;
using Agdata.SeatBooking.Data;
using Agdata.SeatBooking.Domain.Entities;

namespace Agdata.SeatBooking.Application.Services
{
    public class SeatService : ISeatService
    {
        public void AddSeat(Seat seat)
        {
            using (var context = new SeatBookingContext())
            {
                context.Seats.Add(seat);
                context.SaveChanges();
                Console.WriteLine($"Seat {seat.SeatNumber} added with ID {seat.Id}.");
            }
        }

        public void RemoveSeat(int seatId)
        {
            using (var context = new SeatBookingContext())
            {
                var seat = context.Seats.Find(seatId);
                if (seat != null)
                {
                    if (seat.IsBooked)
                    {
                        Console.WriteLine($"Error: Seat with ID {seatId} cannot be removed because it is already booked.");
                    }
                    else
                    {
                        context.Seats.Remove(seat);
                        context.SaveChanges();
                        Console.WriteLine($"Seat with ID {seatId} removed.");
                    }
                }
                else
                {
                    Console.WriteLine($"Error: Seat with ID {seatId} does not exist.");
                }
            }
        }

        public List<Seat> GetAllSeats()
        {
            using (var context = new SeatBookingContext())
            {
                return context.Seats.ToList();
            }
        }

        public Seat GetSeatById(int seatId)
        {
            using (var context = new SeatBookingContext())
            {
                return context.Seats.Find(seatId);
            }
        }
    }
}