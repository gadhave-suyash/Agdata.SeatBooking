using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agdata.SeatBooking.Application.Interfaces;
using Agdata.SeatBooking.Domain.Entities;

namespace Agdata.SeatBooking.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly List<Employee> _employees = new List<Employee>();
        private static int _nextEmployeeId = 1; // Static variable for auto-incrementing EmployeeId

        public void AddEmployee(Employee employee)
        {
            employee.Id = _nextEmployeeId++; // Assign and increment EmployeeId
            _employees.Add(employee);
            Console.WriteLine($"Employee {employee.Name} added with ID {employee.Id}.");
        }

        public void RemoveEmployee(int employeeId)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == employeeId);
            if (employee != null)
            {
                _employees.Remove(employee);
                Console.WriteLine($"Employee with ID {employeeId} removed.");
            }
            else
            {
                Console.WriteLine($"Error: Employee with ID {employeeId} does not exist.");
            }
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return _employees.FirstOrDefault(e => e.Id == employeeId);
        }

        public List<Employee> GetAllEmployees()
        {
            return _employees;
        }

        public Employee Authenticate(string name, string password)
        {
            return _employees.FirstOrDefault(e => e.Name == name && e.Password == password);
        }
    }
}
