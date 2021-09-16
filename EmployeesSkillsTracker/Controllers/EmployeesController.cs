using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeesSkillsTracker.Entities;
using EmployeesSkillsTracker.Interfaces;
using EmployeesSkillsTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesSkillsTracker.Controllers
{
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        [HttpGet("api/employees")]
        public ActionResult<IEnumerable<EmployeeDto>> GetEmployees()
        {
            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(_employeeRepository.GetEmployees()));
        }

        [HttpGet("api/employees/{employeeId}", Name = "GetEmployee")]
        public ActionResult<EmployeeDto> GetEmployeeByID(int employeeId)
        {
            var employee = _employeeRepository.GetEmployeeByID(employeeId);

            if (employee == null)
                return NotFound();

            return Ok(_mapper.Map<EmployeeDto>(employee));
        }


        [HttpGet("api/employees/{employeeId}/skills", Name = "GetEmployeeSkills")]
        public ActionResult<Employee> GetEmployeeSkills(int employeeId)
        {
            var employee = _employeeRepository.GetEmployeeSkills(employeeId);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost("api/employees")]
        public ActionResult<EmployeeDto> CreateEmployee(EmployeeDto employee)
        {
            var employeeEntity = _mapper.Map<Employee>(employee);

            _employeeRepository.CreateEmployee(employeeEntity);
            _employeeRepository.Save();

            var createdEmployee = _mapper.Map<EmployeeDto>(employeeEntity);

            //Create Temp Password and send it to the email

            return CreatedAtRoute("GetEmployee", new { employeeId = createdEmployee.EmployeeID }, createdEmployee);
        }

        [HttpPut("api/employees/{employeeId}")]
        public ActionResult UpdateEmployee(int employeeId, Employee employee)
        {
            if (employee == null)
                return BadRequest();

            if (!_employeeRepository.EmployeeExists(employeeId))
                return NotFound();
                
            _employeeRepository.UpdateEmployee(employee);
            _employeeRepository.Save();

            var employeeEntity = _employeeRepository.GetEmployeeByID(employeeId);

            return Ok(employeeEntity);
        }

        [HttpDelete("api/employees/{employeeId}")]
        public ActionResult DeleteEmployee(int employeeId)
        {
            var employee = _employeeRepository.GetEmployeeByID(employeeId);

            if (employee == null)
                return NotFound();

            _employeeRepository.DeleteEmployee(employee);
            _employeeRepository.Save();

            return NoContent();
        }

        [HttpPost("api/employees/{employeeId}/skills")]
        public ActionResult<List<Skill>> AddEmployeeSkills(int employeeId, List<EmployeeSkill> employeeSkills)
        {
            var employee = _employeeRepository.GetEmployeeByID(employeeId);

            if (employee == null)
                return NotFound();

            var updatedEmployee = _employeeRepository.AddEmployeeSkills(employee, employeeSkills);
            _employeeRepository.Save();

            return CreatedAtRoute("GetEmployeeSkills", new { employeeId = updatedEmployee.EmployeeID }, updatedEmployee);
        }

        [HttpDelete("api/employees/{employeeId}/skills/{skillId}")]
        public ActionResult DeleteEmployeeSkill(int employeeId, int skillId)
        {
            var employeeSkill = _employeeRepository.GetEmployeeSkill(employeeId, skillId);

            if (employeeSkill == null)
                return NotFound();

            _employeeRepository.DeleteEmployeeSkill(employeeSkill);
            _employeeRepository.Save();

            return NoContent();
        }

        [HttpPut("api/employees/{employeeId}/skills/{skillId}")]
        public IActionResult UpdateEmployeeSkill(int employeeId, int skillId, EmployeeSkill updatedEmployeeSkill)
        {
            var employeeSkill = _employeeRepository.GetEmployeeSkill(employeeId, skillId);

            if (employeeSkill == null)
                return NotFound();

            _employeeRepository.UpdateEmployeeSkill(updatedEmployeeSkill);
            _employeeRepository.Save();

            return NoContent();

        }


    }
}
