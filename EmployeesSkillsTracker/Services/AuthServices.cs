using EmployeesSkillsTracker.Entities;
using EmployeesSkillsTracker.Helpers;
using EmployeesSkillsTracker.Interfaces;
using EmployeesSkillsTracker.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmployeesSkillsTracker.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly IJWTHelper _jWTHelper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthServices(IJWTHelper jWTHelper, IEmployeeRepository employeeRepository, IHttpContextAccessor httpContextAccessor)
        {
            _jWTHelper = jWTHelper;
            _employeeRepository = employeeRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public string GenerateAccessToken(Employee employee)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Role, employee.RoleId.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, employee.EmployeeID.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds.ToString(), ClaimValueTypes.Integer),

            };
            string token = _jWTHelper.GenerateJSONWebToken(claims, "Access");
            return token;
        }
        public string GenerateRefreshToken(Employee employee)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Role, employee.RoleId.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, employee.EmployeeID.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds.ToString(), ClaimValueTypes.Integer),
            };

            string token = _jWTHelper.GenerateJSONWebToken(claims, "Refresh");
            return token;
        }



        public TokenResponseDto ValidateRefreshToken(string refreshToken)
        {
            var claims = _jWTHelper.ValidateJWTToken(refreshToken, "Refresh") ?? throw new UnauthorizedAccessException();

            var tokenValidationResponse = ValidateUserClaimsFromToken(claims);

            var user = _employeeRepository.GetEmployeeByID(tokenValidationResponse.Id);

            return new TokenResponseDto(GenerateAccessToken(user), GenerateRefreshToken(user));

        }

        public TokenValidationResponse ValidateUserClaimsFromToken(IEnumerable<Claim> claims)
        {
            var Id = claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;
            var user = _employeeRepository.GetEmployeeByID(int.Parse(Id)) ?? throw new UnauthorizedAccessException();

            return new TokenValidationResponse(user);
        }

        public TokenValidationResponse ValidateUserClaimsFromContext()
        {
            var Id = _httpContextAccessor.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            var user = _employeeRepository.GetEmployeeByID(int.Parse(Id)) ?? throw new UnauthorizedAccessException();

            return new TokenValidationResponse(user);
        }

    }
}
