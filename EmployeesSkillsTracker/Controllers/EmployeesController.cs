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
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IJWTHelper _jWTHelper;
        private readonly IAuthServices _authServices;

        //private readonly JWTHelper _options;

        public EmployeesController(IEmployeeRepository employeeRepository, IJWTHelper jWTHelper, IAuthServices authServices
                                   /*SignInManager<IdentityUser> signInManager,*/ /*IOptions<JWTHelper> optionsAccessor,*/)
        {
            _employeeRepository = employeeRepository;
            _jWTHelper = jWTHelper;
            _authServices = authServices;
            //_options = optionsAccessor.Value;   
        }

        [HttpGet("api/employees")]
        [Authorize]
        public ActionResult<IEnumerable<Employee>> GetEmployees()
        {
            return Ok(_employeeRepository.GetEmployees());
        }

        [HttpGet("api/employees/{employeeId}", Name = "GetEmployee")]
        [Authorize]
        public ActionResult<Employee> GetEmployeeByID(int employeeId)
        {
            if (employeeId != int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value)
                || int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value) == 2)
            {
                throw new UnauthorizedAccessException();
            };

            var employee = _employeeRepository.GetEmployeeByID(employeeId);

            if (employee == null)
                return NotFound();

            return Ok(employee);
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
        public IActionResult CreateEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.Password = _jWTHelper.CreatePassword("abasdasdASDASDA124!@");
                _employeeRepository.CreateEmployee(employee);
                _employeeRepository.Save();

                var accessToken = _authServices.GenerateAccessToken(employee);
                var refreshToken = _authServices.GenerateRefreshToken(employee);

                return Ok(new ResponseDto<Employee> { responseObject = employee, AccessToken = accessToken, RefreshToken = refreshToken });     
            }

            //var employeeEntity = _mapper.Map<Employee>(employee);

            _employeeRepository.CreateEmployee(employee);
            _employeeRepository.Save();

            //Create Temp Password and send it to the email

            return CreatedAtRoute("GetEmployee", new { employeeId = employee.EmployeeID }, employee);
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
        public ActionResult<Employee> AddEmployeeSkills(int employeeId, List<EmployeeSkill> employeeSkills)
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

        [HttpPost("api/login")]
        public IActionResult Login([FromForm] string username, [FromForm] string password)
        {
            var response = _authServices.LoginEmployee(username, password);

            return Ok(response);
        }


        //private static string RandomString()
        //{
        //    Random random = new Random();
        //    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_";
        //    return new string(Enumerable.Repeat(chars, 10)
        //      .Select(s => s[random.Next(s.Length)]).ToArray());
        //}
        //private string GetIdToken(IdentityUser user)
        //{
        //    var payload = new Dictionary<string, object>
        //      {
        //        { "id", user.Id },
        //        { "sub", user.Email },
        //        { "email", user.Email },
        //        { "emailConfirmed", user.EmailConfirmed },
        //      };
        //            return GetToken(payload);
        //}

        //private string GetToken(Dictionary<string, object> payload)
        //{
        //    var secret = _options.SecretKey;

        //    payload.Add("iss", _options.Issuer);
        //    payload.Add("aud", _options.Audience);
        //    payload.Add("nbf", ConvertToUnixTimestamp(DateTime.Now));
        //    payload.Add("iat", ConvertToUnixTimestamp(DateTime.Now));
        //    payload.Add("exp", ConvertToUnixTimestamp(DateTime.Now.AddDays(7)));
        //    IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
        //    IJsonSerializer serializer = new JsonNetSerializer();
        //    IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
        //    IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

        //    return encoder.Encode(payload, secret);
        //}

        //private string GetAccessToken(string Email)
        //{
        //    var payload = new Dictionary<string, object>
        //              {
        //                { "sub", Email },
        //                { "email", Email }
        //              };
        //    return GetToken(payload);
        //}

        //private static double ConvertToUnixTimestamp(DateTime date)
        //{
        //    DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        //    TimeSpan diff = date.ToUniversalTime() - origin;
        //    return Math.Floor(diff.TotalSeconds);
        //}


    }
}
