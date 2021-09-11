using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesSkillsTracker.Profiles
{
    public class SkillsProfile: Profile
    {
        public SkillsProfile()
        {
            CreateMap<Entities.Skill, Models.SkillDto>();

            CreateMap<Models.SkillDto, Entities.Skill>();
        }
    }
}
