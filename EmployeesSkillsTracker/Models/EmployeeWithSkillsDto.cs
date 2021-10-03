using EmployeesSkillsTracker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesSkillsTracker.Models
{
    public class EmployeeWithSkillsDto
    {
        public int EmployeeID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Position { get; set; }

        public double YearsOfExperience { get; set; }

        public virtual List<EmployeeSkillDto> Skills { get; set; }
    }
}
