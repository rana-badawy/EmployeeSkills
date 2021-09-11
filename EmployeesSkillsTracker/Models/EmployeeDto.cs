using EmployeesSkillsTracker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesSkillsTracker.Models
{
    public class EmployeeDto
    {
        public int EmployeeID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Role { get; set; }

        public int Salary { get; set; }

        public double YearsOfExperience { get; set; }

        public virtual List<EmployeeSkillDto> Skills { get; set; }
    }
}
