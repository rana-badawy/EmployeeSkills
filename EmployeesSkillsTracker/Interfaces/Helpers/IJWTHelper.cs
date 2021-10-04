using System.Collections.Generic;
using System.Security.Claims;

namespace EmployeesSkillsTracker.Interfaces.Helpers
{
    public interface IJWTHelper
    {
        string GenerateJSONWebToken(IEnumerable<Claim> claims, string tokenType);

        IEnumerable<Claim> ValidateJWTToken(string token, string tokenType);

        string CreatePassword(string password = "");

        bool VerifyPassword(string hashedPassword, string providedPassword);

        public string GenerateRandomPassword(int length = 8);
    }
}