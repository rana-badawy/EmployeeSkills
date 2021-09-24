using EmployeesSkillsTracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesSkillsTracker.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class TokenApiController : ControllerBase
    {
        private readonly IAuthServices _authServices;

        public TokenApiController(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        [Route("validate")]
        [Authorize]
        [HttpGet]
        public IActionResult validate()
        {
            return Ok(_authServices.ValidateUserClaimsFromContext());
        }

        [Route("refresh")]
        [HttpGet]
        public IActionResult refresh([FromQuery(Name = "refresh_token")] string refreshToken)
        {
            return Ok(_authServices.ValidateRefreshToken(refreshToken));
        }

    }
}
