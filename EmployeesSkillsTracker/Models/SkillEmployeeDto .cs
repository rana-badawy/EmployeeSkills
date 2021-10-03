using EmployeesSkillsTracker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesSkillsTracker.Models
{
    public class SkillEmployeeDto
    {
        public string Level { get; set; }

        public EmployeeFromSkillDto Employee;
    }
}
