using Agdata.SeatBooking.Application.Interfaces;
using Agdata.SeatBooking.Data;
using Agdata.SeatBooking.Domain.Entities;
using System.Collections.Generic;
using System;
using System.Linq;

public class EmployeeService : IEmployeeService
{
    public void AddEmployee(Employee employee)
    {
        using (var context = new SeatBookingContext())
        {
            context.Employees.Add(employee);
            context.SaveChanges();
            Console.WriteLine($"Employee {employee.Name} added with ID {employee.Id}.");
        }
    }

    public void RemoveEmployee(int employeeId)
    {
        using (var context = new SeatBookingContext())
        {
            var employee = context.Employees.Find(employeeId);
            if (employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
                Console.WriteLine($"Employee with ID {employeeId} removed.");
            }
            else
            {
                Console.WriteLine($"Error: Employee with ID {employeeId} does not exist.");
            }
        }
    }

    public Employee GetEmployeeById(int employeeId)
    {
        using (var context = new SeatBookingContext())
        {
            return context.Employees.Find(employeeId);
        }
    }

    public List<Employee> GetAllEmployees()
    {
        using (var context = new SeatBookingContext())
        {
            return context.Employees.ToList();
        }
    }

    public Employee Authenticate(string name, string password)
    {
        using (var context = new SeatBookingContext())
        {
            return context.Employees.FirstOrDefault(e => e.Name == name && e.Password == password);
        }
    }
}