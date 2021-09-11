using EmployeesSkillsTracker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesSkillsTracker.Models
{
    public class SkillEmployeeDto
    {
        public int EmployeeSkillID { get; set; }
        public string Level { get; set; }

        public EmployeeDto Employee;

    }
}
