using EmployeesSkillsTracker.Entities;
using EmployeesSkillsTracker.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace EmployeesSkillsTracker.Services
{
    public interface IAuthServices
    {
        string GenerateAccessToken(Employee employee);
        string GenerateRefreshToken(Employee employee);
        TokenResponseDto ValidateRefreshToken(string refreshToken);
        TokenValidationResponse ValidateUserClaimsFromToken(IEnumerable<Claim> claims);
        TokenValidationResponse ValidateUserClaimsFromContext();
        ResponseDto<Employee> LoginEmployee(string username, string password);
    }
}