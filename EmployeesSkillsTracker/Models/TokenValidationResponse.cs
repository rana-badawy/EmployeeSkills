using EmployeesSkillsTracker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesSkillsTracker.Models
{
    public class TokenValidationResponse
    {
        public int Id { get; set; }
        public Employee? Employee { get; set; }
        public TokenValidationResponse(Employee employee)
        {
            Employee = employee;
        }
    }
}
