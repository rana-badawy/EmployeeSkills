using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesSkillsTracker.Models
{
    public class TokenResponseDto
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public TokenResponseDto(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

    }
}
