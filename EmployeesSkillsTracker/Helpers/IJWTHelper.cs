using System.Collections.Generic;
using System.Security.Claims;

namespace EmployeesSkillsTracker.Helpers
{
    public interface IJWTHelper
    {
        string GenerateJSONWebToken(IEnumerable<Claim> claims, string tokenType);
        IEnumerable<Claim> ValidateJWTToken(string token, string tokenType);
        string CreatePassword(string Password);
    }
}