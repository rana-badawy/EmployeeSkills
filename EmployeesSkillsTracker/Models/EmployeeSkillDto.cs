using EmployeesSkillsTracker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesSkillsTracker.Models
{
    public class EmployeeSkillDto
    {
        public string Level { get; set; }

        public SkillDto Skill;
    }
}
