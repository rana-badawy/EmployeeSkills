using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EmployeesSkillsTracker.Entities;
using EmployeesSkillsTracker.Interfaces.Repositories;
using EmployeesSkillsTracker.Interfaces.Helpers;
using EmployeesSkillsTracker.Interfaces.Services;
using EmployeesSkillsTracker.Models;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EmployeesSkillsTracker.Controllers
{
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IJWTHelper _jWTHelper;
        private readonly IAuthServices _authServices;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public EmployeesController(IEmployeeRepository employeeRepository, IJWTHelper jWTHelper, IAuthServices authServices, IMapper mapper, IEmailService emailService)
        {
            _employeeRepository = employeeRepository;
            _jWTHelper = jWTHelper;
            _authServices = authServices;
            _mapper = mapper;
            _emailService = emailService;
        }

        [HttpGet("api/employees")]
        public ActionResult<IEnumerable<EmployeeDto>> GetEmployees()
        {
            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(_employeeRepository.GetEmployees()));
        }

        [HttpGet("api/employees/{employeeId}", Name = "GetEmployee")]
        public ActionResult<EmployeeWithoutPasswordDto> GetEmployeeByID(int employeeId)
        {
            if (employeeId == int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value)
                || int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value) == 1)
            {
                var employee = _employeeRepository.GetEmployeeByID(employeeId);

                if (employee == null)
                    return NotFound();

                return Ok(_mapper.Map<EmployeeWithoutPasswordDto>(employee));   
            }
            return Unauthorized();
        }

        [HttpGet("api/employees/{employeeId}/skills", Name = "GetEmployeeSkills")]
        public ActionResult<EmployeeWithSkillsDto> GetEmployeeSkills(int employeeId)
        {
            if (employeeId == int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value)
                || int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value) == 1)
            {
                var employee = _employeeRepository.GetEmployeeSkills(employeeId);

                if (employee == null)
                    return NotFound();

                return Ok(_mapper.Map<EmployeeWithSkillsDto>(employee));
            }
            return Unauthorized();
        }

        [HttpPost("api/employees")]
        public IActionResult CreateEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var password = _jWTHelper.GenerateRandomPassword();

                employee.Password = _jWTHelper.CreatePassword(password);

                _employeeRepository.CreateEmployee(employee);
                _employeeRepository.Save();

                var accessToken = _authServices.GenerateAccessToken(employee);
                var refreshToken = _authServices.GenerateRefreshToken(employee);

                var employeeDto = _mapper.Map<EmployeeWithoutPasswordDto>(employee);

                var body = "Dear " + employee.FirstName + ",<br><br>Your account has been created successfully.<br>Your Temporary password is: " + password + "<br><br>Kindly, refer to the following link to change your password<br>http://localhost:5000/api/employees/" + employee.EmployeeID + "/changepassword<br><br>Regards";

                //Send password to the employee by email
                _emailService.sendEmail("Account Created Successfully", employee.Email, body);

                return Ok(new ResponseDto<EmployeeWithoutPasswordDto> { responseObject = employeeDto, AccessToken = accessToken, RefreshToken = refreshToken });     
            }

            return BadRequest();
        }

        [HttpPut("api/employees/{employeeId}")]
        public ActionResult UpdateEmployee(int employeeId, Employee employee)
        {
            if (employeeId == int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value)
                || int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value) == 1)
            {
                if (employee == null)
                    return BadRequest();

                if (!_employeeRepository.EmployeeExists(employeeId))
                    return NotFound();

                _employeeRepository.UpdateEmployee(employee);
                _employeeRepository.Save();

                return Ok();
            }
            return Unauthorized();
        }

        [HttpDelete("api/employees/{employeeId}")]
        public ActionResult DeleteEmployee(int employeeId)
        {
            if (int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value) == 1)
            {
                var employee = _employeeRepository.GetEmployeeByID(employeeId);

                if (employee == null)
                    return NotFound();

                _employeeRepository.DeleteEmployee(employee);
                _employeeRepository.Save();

                return Ok();
            }
            return Unauthorized();
        }

        [HttpPost("api/employees/{employeeId}/skills")]
        public ActionResult<Employee> AddEmployeeSkills(int employeeId, List<EmployeeSkill> employeeSkills)
        {
            if (employeeId == int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value)
                || int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value) == 1)
            {
                var employee = _employeeRepository.GetEmployeeByID(employeeId);

                if (employee == null)
                    return NotFound();

                var updatedEmployee = _employeeRepository.AddEmployeeSkills(employee, employeeSkills);
                _employeeRepository.Save();

                return Ok();
            }
            return Unauthorized();
        }

        [HttpDelete("api/employees/{employeeId}/skills/{skillId}")]
        public ActionResult DeleteEmployeeSkill(int employeeId, int skillId)
        {
            if (employeeId == int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value)
               || int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value) == 1)
            {
                var employeeSkill = _employeeRepository.GetEmployeeSkill(employeeId, skillId);

                if (employeeSkill == null)
                    return NotFound();

                _employeeRepository.DeleteEmployeeSkill(employeeSkill);
                _employeeRepository.Save();

                return Ok();
            }
            return Unauthorized();
        }

        [HttpPut("api/employees/{employeeId}/skills/{skillId}")]
        public ActionResult UpdateEmployeeSkill(int employeeId, int skillId, EmployeeSkill updatedEmployeeSkill)
        {
            if (employeeId == int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value)
               || int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value) == 1)
            {
                var employeeSkill = _employeeRepository.GetEmployeeSkill(employeeId, skillId);

                if (employeeSkill == null)
                    return NotFound();

                updatedEmployeeSkill.EmployeeSkillID = employeeSkill.EmployeeSkillID;

                _employeeRepository.UpdateEmployeeSkill(updatedEmployeeSkill);
                _employeeRepository.Save();

                return Ok();
            }
            return Unauthorized();
        }

        [HttpPost("api/login")]
        [AllowAnonymous]
        public IActionResult Login([FromForm] string username, [FromForm] string password)
        {
            var response = _authServices.LoginEmployee(username, password);

            return Ok(response);
        }

        [HttpPut("api/employees/{employeeId}/changepassword")]
        public ActionResult ChangeEmployeePassword(int employeeId, [FromForm] string oldPassword, [FromForm] string newPassword)
        {
            if (employeeId == int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value))
            {
                var employee = _employeeRepository.GetEmployeeByID(employeeId);

                if (employee == null)
                    return NotFound();

                var verified = _jWTHelper.VerifyPassword(employee.Password, oldPassword);

                if (!verified)
                    return Unauthorized();

                employee.Password = _jWTHelper.CreatePassword(newPassword);

                _employeeRepository.UpdateEmployee(employee);
                _employeeRepository.Save();

                return Ok();
            }
            return Unauthorized();
        }
    }
}
