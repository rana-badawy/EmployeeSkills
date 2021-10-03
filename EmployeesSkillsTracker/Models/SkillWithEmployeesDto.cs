using EmployeesSkillsTracker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesSkillsTracker.Models
{
    public class SkillWithEmployeesDto
    {
        public int SkillID { get; set; }

        public string Name { get; set; }

        public int Vertical { get; set; }

        public virtual List<SkillEmployeeDto> Employees { get; set; }
    }
}
