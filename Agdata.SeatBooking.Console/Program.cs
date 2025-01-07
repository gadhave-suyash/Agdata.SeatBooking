using System;
using System.Linq;
using Agdata.SeatBooking.Application.Interfaces;
using Agdata.SeatBooking.Application.Services;
using Agdata.SeatBooking.Data;
using Agdata.SeatBooking.Domain.Entities;

public class Program
{
    private static IEmployeeService _employeeService = new EmployeeService();
    private static ISeatService _seatService = new SeatService();
    private static IBookingService _bookingService;

    static void Main(string[] args)
    {
        // Seed initial data
        SeedInitialData();

        Console.Clear();
        Console.WriteLine("Welcome to the Seat Booking Application!");

        while (true)
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            var employee = _employeeService.Authenticate(name, password);
            if (employee != null)
            {
                Console.WriteLine($"Welcome, {employee.Name}!");
                if (employee.Role == "Admin")
                {
                    AdminMenu(employee.Id);
                }
                else
                {
                    UserMenu(employee.Id);
                }
            }
            else
            {
                Console.WriteLine("Error: Invalid credentials. Please try again.");
            }
        }
    }

    private static void SeedInitialData()
    {
        // Initialize data
        using (var context = new SeatBookingContext())
        {
            if (!context.Employees.Any())
            {
                context.Employees.Add(new Employee { Name = "Admin 1", Role = "Admin", Password = "admin" });
                context.Employees.Add(new Employee { Name = "User  1", Role = "User ", Password = "user" });
                context.SaveChanges();
            }

            if (!context.Seats.Any())
            {
                context.Seats.Add(new Seat { SeatNumber = "A1", IsBooked = false });
                context.Seats.Add(new Seat { SeatNumber = "A2", IsBooked = false });
                context.Seats.Add(new Seat { SeatNumber = "B1", IsBooked = false });
                context.SaveChanges();
            }
        }
    }
    private static void AdminMenu(int employeeId)
    {
        while (true)
        {
            Console.WriteLine("\nAdmin Menu:");
            Console.WriteLine("1. Add Seat");
            Console.WriteLine("2. Remove Seat");
            Console.WriteLine("3. Add Employee");
            Console.WriteLine("4. Remove Employee");
            Console.WriteLine("5. Book Seat for Anyone");
            Console.WriteLine("6. Remove Booking of Anyone");
            Console.WriteLine("7. View All Employees");
            Console.WriteLine("8. View All Bookings");
            Console.WriteLine("9. View All Self Bookings");
            Console.WriteLine("10. View Bookings for Employee");
            Console.WriteLine("11. View All Seats");
            Console.WriteLine("12. Book Seat for Self"); 
            Console.WriteLine("13. Delete Booking for Self");
            Console.WriteLine("0. Logout");
            Console.WriteLine("X. Exit Application");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddSeat();
                    break;
                case "2":
                    RemoveSeat();
                    break;
                case "3":
                    AddEmployee();
                    break;
                case "4":
                    RemoveEmployee();
                    break;
                case "5":
                    BookSeatForAnyone();
                    break;
                case "6":
                    RemoveBookingOfAnyone();
                    break;
                case "7":
                    ViewAllEmployees();
                    break;
                case "8":
                    ViewAllBookings();
                    break;
                case "9":
                    ViewAllSelfBookings(employeeId);
                    break;
                case "10":
                    ViewBookingsForEmployee(); 
                    break;
                case "11":
                    ViewAllSeats();
                    break;
                case "12":
                    BookSeatForSelf(employeeId);
                    break;
                case "13":
                    DeleteBookingForSelf(employeeId);
                    break;
                case "0":
                    return; // Logout
                case "X":
                case "x":
                    if (ConfirmExit("Are you sure you want to exit the application? (y/n)"))
                        Environment.Exit(0); // Exit application
                    break;
                default:
                    Console.WriteLine("Error: Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private static void UserMenu(int employeeId)
    {
        while (true)
        {
            Console.WriteLine("\nUser  Menu:");
            Console.WriteLine("1. Book Seat for Self");
            Console.WriteLine("2. Delete Booking for Self");
            Console.WriteLine("3. View All Self Bookings");
            Console.WriteLine("4. View All Seats");
            Console.WriteLine("0. Logout");
            Console.WriteLine("X. Exit Application");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    BookSeatForSelf(employeeId);
                    break;
                case "2":
                    DeleteBookingForSelf(employeeId);
                    break;
                case "3":
                    ViewAllSelfBookings(employeeId);
                    break;
                case "4":
                    ViewAllSeats(); 
                    break;
                case "0":
                    return; // Logout
                case "X":
                case "x":
                    if (ConfirmExit("Are you sure you want to exit the application? (y/n)"))
                        Environment.Exit(0); // Exit application
                    break;
                default:
                    Console.WriteLine("Error: Invalid choice. Please try again.");
                    break;
            }
        }
    }


    private static bool ConfirmExit(string message)
    {
        Console.Write(message);
        var response = Console.ReadLine();
        return response?.Trim().ToLower() == "y";
    }

    private static void AddSeat()
    {
        Console.Write("Enter seat number: ");
        var seatNumber = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(seatNumber))
        {
            Console.WriteLine("Error: Seat number cannot be empty.");
            return;
        }

        _seatService.AddSeat(new Seat { SeatNumber = seatNumber, IsBooked = false });
        Console.WriteLine($"Seat {seatNumber} added successfully.");
    }

    private static void RemoveSeat()
    {
        Console.Write("Enter seat ID to remove: ");
        if (int.TryParse(Console.ReadLine(), out int seatIdToRemove))
        {
            var seat = _seatService.GetSeatById(seatIdToRemove);
            if (seat == null)
            {
                Console.WriteLine($"Error: Seat with ID {seatIdToRemove} does not exist.");
                return;
            }

            if (seat.IsBooked)
            {
                Console.WriteLine($"Error: Seat with ID {seatIdToRemove} cannot be removed because it is already booked.");
                return;
            }

            _seatService.RemoveSeat(seatIdToRemove);
            Console.WriteLine($"Seat with ID {seatIdToRemove} removed successfully.");
        }
        else
        {
            Console.WriteLine("Error: Invalid seat ID format.");
        }
    }

    private static void AddEmployee()
    {
        Console.Write("Enter employee name: ");
        string name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Error: Employee name cannot be empty.");
            return;
        }

        Console.Write("Enter employee password: ");
        string password = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Error: Employee password cannot be empty.");
            return;
        }

        string role;
        while (true)
        {
            Console.Write("Enter employee role (Admin/User): ");
            role = Console.ReadLine();
            if (role == "Admin" || role == "User ")
            {
                break;
            }
            Console.WriteLine("Error: Role must be either 'Admin' or 'User '. Please try again.");
        }

        var employee = new Employee { Name = name, Password = password, Role = role };
        _employeeService.AddEmployee(employee);
        Console.WriteLine($"Employee {name} added successfully with role {employee.Role}.");
    }

    private static void RemoveEmployee()
    {
        Console.Write("Enter employee ID to remove: ");
        if (int.TryParse(Console.ReadLine(), out int employeeIdToRemove))
        {
            _employeeService.RemoveEmployee(employeeIdToRemove);
        }
        else
        {
            Console.WriteLine("Error: Invalid employee ID format.");
        }
    }

    private static void BookSeatForAnyone()
    {
        Console.Write("Enter seat ID to book: ");
        if (int.TryParse(Console.ReadLine(), out int seatId) &&
            _employeeService.GetAllEmployees().Count > 0)
        {
            Console.Write("Enter employee ID to book for: ");
            if (int.TryParse(Console.ReadLine(), out int employeeId))
            {
                int bookingId = _bookingService.BookSeat(seatId, employeeId);
                if (bookingId != -1) // Check if booking was successful
                {
                    Console.WriteLine($"Booking successful! The booking ID is {bookingId}.");
                }
            }
            else
            {
                Console.WriteLine("Error: Invalid employee ID format.");
            }
        }
        else
        {
            Console.WriteLine("Error: Invalid seat ID format or no employees available.");
        }
    }

    private static void RemoveBookingOfAnyone()
    {
        Console.Write("Enter booking ID to remove: ");
        if (int.TryParse(Console.ReadLine(), out int bookingId))
        {
            _bookingService.RemoveBooking(bookingId);
        }
        else
        {
            Console.WriteLine("Error: Invalid booking ID format.");
        }
    }

    private static void ViewAllEmployees()
    {
        var employees = _employeeService.GetAllEmployees();
        Console.WriteLine("All Employees:");
        foreach (var employee in employees)
        {
            Console.WriteLine($"ID: {employee.Id}, Name: {employee.Name}, Role: {employee.Role}");
        }
    }

    private static void ViewAllBookings()
    {
        var bookings = _bookingService.GetAllBookings();
        Console.WriteLine("All Bookings:");
        foreach (var booking in bookings)
        {
            Console.WriteLine($"Booking ID: {booking.Id}, Seat ID: {booking.SeatId}, Employee ID: {booking.EmployeeId}, Date: {booking.BookingDate}");
        }
    }

    private static void ViewAllSelfBookings(int employeeId)
    {
        var bookings = _bookingService.GetBookingsByEmployeeId(employeeId);
        Console.WriteLine($"Your Bookings (Employee ID: {employeeId}):");
        foreach (var booking in bookings)
        {
            Console.WriteLine($"Booking ID: {booking.Id}, Seat ID: {booking.SeatId}, Date: {booking.BookingDate}");
        }
    }

    private static void BookSeatForSelf(int employeeId)
    {
        Console.Write("Enter seat ID to book for yourself: ");
        if (int.TryParse(Console.ReadLine(), out int seatId))
        {
            var seat = _seatService.GetSeatById(seatId);
            if (seat == null)
            {
                Console.WriteLine($"Error: Seat with ID {seatId} does not exist.");
                return;
            }

            if (seat.IsBooked)
            {
                Console.WriteLine($"Error: Seat with ID {seatId} is already booked.");
                return;
            }

            var bookingId = _bookingService.BookSeat(seatId, employeeId);
            Console.WriteLine($"Booking successful! Your booking ID is {bookingId}.");
        }
        else
        {
            Console.WriteLine("Error: Invalid seat ID format.");
        }
    }

    private static void DeleteBookingForSelf(int employeeId)
    {
        Console.Write("Enter booking ID to delete: ");
        if (int.TryParse(Console.ReadLine(), out int bookingId))
        {
            _bookingService.RemoveBooking(bookingId);
        }
        else
        {
            Console.WriteLine("Error: Invalid booking ID format.");
        }
    }

    private static void ViewAllSeats()
    {
        var seats = _seatService.GetAllSeats();
        Console.WriteLine("All Seats:");
        foreach (var seat in seats)
        {
            string bookedBy = seat.IsBooked ? $" (Booked by Employee ID: {seat.BookedById})" : " (Available)";
            Console.WriteLine($"Seat ID: {seat.Id}, Seat Number: {seat.SeatNumber}, Booked: {seat.IsBooked}{bookedBy}");
        }
    }

    private static void ViewBookingsForEmployee()
    {
        Console.Write("Enter employee ID to view bookings: ");
        if (int.TryParse(Console.ReadLine(), out int employeeId))
        {
            var bookings = _bookingService.GetBookingsByEmployeeId(employeeId);
            if (bookings == null || !bookings.Any())
            {
                Console.WriteLine($"Error: No bookings found for employee ID {employeeId}.");
                return;
            }

            Console.WriteLine("Bookings for employee:");
            foreach (var booking in bookings)
            {
                Console.WriteLine($"Booking ID: {booking.Id}, Seat: {booking.SeatId}, Date: {booking.BookingDate}");
            }
        }
        else
        {
            Console.WriteLine("Error: Invalid employee ID format.");
        }
    }
}

