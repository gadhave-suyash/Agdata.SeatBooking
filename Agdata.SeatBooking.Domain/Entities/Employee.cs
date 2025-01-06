using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agdata.SeatBooking.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; } // "Admin" or "User "
        public string Password { get; set; }
    }
}
