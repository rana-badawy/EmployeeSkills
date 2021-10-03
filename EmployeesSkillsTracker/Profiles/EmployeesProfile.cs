using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesSkillsTracker.Profiles
{
    public class EmployeesProfile: Profile
    {
        public EmployeesProfile()
        {
            CreateMap<Entities.Employee, Models.EmployeeDto>().ReverseMap();
            CreateMap<Entities.Employee, Models.EmployeeWithoutPasswordDto>().ReverseMap();
            CreateMap<Entities.Employee, Models.EmployeeWithSkillsDto>().ReverseMap();
            CreateMap<Entities.Employee, Models.EmployeeFromSkillDto>().ReverseMap();
        }
    }
}
