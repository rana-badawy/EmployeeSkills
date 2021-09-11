using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesSkillsTracker.Profiles
{
    public class EmployeesSkillsProfile: Profile
    {
        public EmployeesSkillsProfile()
        {
            CreateMap<Entities.EmployeeSkill, Models.EmployeeSkillDto>().ReverseMap();
            CreateMap<Entities.EmployeeSkill, Models.SkillEmployeeDto>().ReverseMap();
        }
    }
}
