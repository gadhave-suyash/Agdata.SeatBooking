using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agdata.SeatBooking.Domain.Entities;

namespace Agdata.SeatBooking.Application.Interfaces
{
    public interface IEmployeeService
    {
        void AddEmployee(Employee employee);
        void RemoveEmployee(int employeeId);
        Employee GetEmployeeById(int employeeId);
        List<Employee> GetAllEmployees();
        Employee Authenticate(string name, string password);
    }
}
