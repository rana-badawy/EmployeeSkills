using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeesSkillsTracker.Entities;
using EmployeesSkillsTracker.Helpers;
using EmployeesSkillsTracker.Interfaces;
using EmployeesSkillsTracker.Models;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EmployeesSkillsTracker.Controllers
{
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JWTSettings _options;

        public EmployeesController(IEmployeeRepository employeeRepository, IMapper mapper, UserManager<IdentityUser> userManager, 
                                   SignInManager<IdentityUser> signInManager, IOptions<JWTSettings> optionsAccessor)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _options = optionsAccessor.Value;   
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
        public async Task<ActionResult<EmployeeDto>> CreateEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = employee.Username, Email = employee.Email };
                var password = "abasdasdASDASDA124!@";
                var result = await _userManager.CreateAsync(user, password);
               
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return new JsonResult(new Dictionary<string, object>
                    {
                        { "access_token", GetAccessToken(employee.Email) },
                        { "id_token", GetIdToken(user) }
                    });
                }
            }


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



        
        private static string RandomString()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_";
            return new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private string GetIdToken(IdentityUser user)
        {
            var payload = new Dictionary<string, object>
              {
                { "id", user.Id },
                { "sub", user.Email },
                { "email", user.Email },
                { "emailConfirmed", user.EmailConfirmed },
              };
                    return GetToken(payload);
        }

        private string GetToken(Dictionary<string, object> payload)
        {
            var secret = _options.SecretKey;

            payload.Add("iss", _options.Issuer);
            payload.Add("aud", _options.Audience);
            payload.Add("nbf", ConvertToUnixTimestamp(DateTime.Now));
            payload.Add("iat", ConvertToUnixTimestamp(DateTime.Now));
            payload.Add("exp", ConvertToUnixTimestamp(DateTime.Now.AddDays(7)));
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            return encoder.Encode(payload, secret);
        }

        private string GetAccessToken(string Email)
        {
            var payload = new Dictionary<string, object>
                      {
                        { "sub", Email },
                        { "email", Email }
                      };
            return GetToken(payload);
        }

        private static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }


    }
}
