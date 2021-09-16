using EmployeesSkillsTracker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesSkillsTracker.Interfaces
{
    public interface IEmployeeRepository
    {
        public IEnumerable<Employee> GetEmployees();

        public bool EmployeeExists(int employeeId);

        public Employee GetEmployeeByID(int employeeID);

        public Employee GetEmployeeSkills(int employeeId);

        public void CreateEmployee(Employee employee);

        public Employee AddEmployeeSkills(Employee employee, List<EmployeeSkill> employeeSkills);

        public void Save();

        public void UpdateEmployee(Employee employee);

        public void DeleteEmployee(Employee employee);

        public EmployeeSkill GetEmployeeSkill(int employeeId, int skillId);

        public void DeleteEmployeeSkill(EmployeeSkill employeeSkill);

        public void UpdateEmployeeSkill(EmployeeSkill employeeSkill);
    }
}
