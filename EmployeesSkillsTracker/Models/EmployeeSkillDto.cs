using EmployeesSkillsTracker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesSkillsTracker.Models
{
    public class EmployeeSkillDto
    {
        public int EmployeeSkillID { get; set; }
        public string Level { get; set; }
        public SkillDto Skill;

        //public int EmployeeID { get; set; }

        //public int SkillID { get; set; }


        //public Employee Employee;

    }
}
