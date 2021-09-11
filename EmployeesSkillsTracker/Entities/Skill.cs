using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeesSkillsTracker.Entities
{
    public class Skill
    {
        [Key]
        public int SkillID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Vertical { get; set; }

        public virtual List<EmployeeSkill> Employees { get; set; }
        
    }
}