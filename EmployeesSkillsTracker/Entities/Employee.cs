using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesSkillsTracker.Entities
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }


        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        //[JsonIgnore]
        public string Password { get; set; }


        public int Salary { get; set; }

        public string Position { get; set; }

        public double YearsOfExperience { get; set; }

        public virtual List<EmployeeSkill> Skills { get; set; }

        [ForeignKey("Role")]
        public int? RoleId { get; set; } = 1;

        public virtual Role Role { get; set; }

    }
}
