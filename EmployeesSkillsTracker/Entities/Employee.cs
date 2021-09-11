using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesSkillsTracker.Entities
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int Salary { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public double YearsOfExperience { get; set; }

        public virtual List<EmployeeSkill> Skills { get; set; }

    }
}
