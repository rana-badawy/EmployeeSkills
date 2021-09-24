using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesSkillsTracker.Entities
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }

        public List<Employee> Employees { get; set; }
    
    }
}
