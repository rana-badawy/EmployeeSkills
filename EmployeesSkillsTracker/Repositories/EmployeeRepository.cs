using EmployeesSkillsTracker.DbContexts;
using EmployeesSkillsTracker.Entities;
using EmployeesSkillsTracker.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesSkillsTracker.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _appDbContext;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _appDbContext.Employees;
        }

        public bool EmployeeExists(int employeeId)
        {
            return _appDbContext.Employees.Any(e => e.EmployeeID == employeeId);
        }

        public Employee GetEmployeeByID(int employeeId)
        {
            return _appDbContext.Employees.AsNoTracking().FirstOrDefault(e => e.EmployeeID == employeeId);
        }
        public Employee GetEmployeeByUsername(string username)
        {
            return _appDbContext.Employees.AsNoTracking().FirstOrDefault(e => e.Username == username);
        }

        public Employee GetEmployeeSkills(int employeeId)
        {
            return _appDbContext.Employees.Include(e => e.Skills).ThenInclude(s => s.Skill).FirstOrDefault(e => e.EmployeeID == employeeId);
        }

        public void CreateEmployee(Employee employee)
        {
            _appDbContext.Employees.Add(employee);
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public void UpdateEmployee(Employee employee)
        {
            //var entity = _appDbContext.Attach(employee);
            //entity.State = EntityState.Modified;
            _appDbContext.Employees.Update(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            _appDbContext.Employees.Remove(employee);
        }

        public EmployeeSkill GetEmployeeSkill(int employeeId, int skillId)
        {
            return _appDbContext.EmployeesSkills.FirstOrDefault(s => s.EmployeeID == employeeId & s.SkillID == skillId);
        }

        public void DeleteEmployeeSkill(EmployeeSkill employeeSkill)
        {
            _appDbContext.EmployeesSkills.Remove(employeeSkill);
        }

        public Employee AddEmployeeSkills(Employee employee, List<EmployeeSkill> employeeSkills)
        {
            foreach(var employeeSkill in employeeSkills)
            {
                employeeSkill.EmployeeID = employee.EmployeeID;
                _appDbContext.EmployeesSkills.Add(employeeSkill);
            }

            return employee;
        }

        public void UpdateEmployeeSkill(EmployeeSkill employeeSkill)
        {
            _appDbContext.EmployeesSkills.Update(employeeSkill);
        }
    }
}
