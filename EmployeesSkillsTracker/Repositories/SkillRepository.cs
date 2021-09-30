using EmployeesSkillsTracker.DbContexts;
using EmployeesSkillsTracker.Entities;
using EmployeesSkillsTracker.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesSkillsTracker.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly AppDbContext _appDbContext;

        public SkillRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Skill> GetSkills()
        {
             return _appDbContext.Skills;
        }

        public bool SkillExists(int skillId)
        {
            return _appDbContext.Skills.Any(s => s.SkillID == skillId);
        }

        public Skill GetSkillByID(int skillId)
        {
            return _appDbContext.Skills.FirstOrDefault(s => s.SkillID == skillId);
        }

        public Skill GetSkillEmployees(int skillId)
        {
            return _appDbContext.Skills.Include(s => s.Employees).ThenInclude(e => e.Employee).FirstOrDefault(s => s.SkillID == skillId);
        }

        public void CreateSkill(Skill skill)
        {
            _appDbContext.Skills.Add(skill);
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public void UpdateSkill(Skill skill)
        {
            _appDbContext.Skills.Update(skill);
        }

        public void DeleteSkill(Skill skill)
        {
            _appDbContext.Skills.Remove(skill);
        }
    }
}
