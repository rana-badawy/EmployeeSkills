﻿using EmployeesSkillsTracker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesSkillsTracker.Services
{
    public interface ISkillRepository
    {
        public IEnumerable<Skill> GetSkills();

        public Skill GetSkillByID(int skillId);

        public Skill GetSkillEmployees(int skillId);

        public void CreateSkill(Skill skill);

        public void Save();

        public void UpdateSkill(Skill skill);

        public void DeleteSkill(Skill skill);
    }
}
