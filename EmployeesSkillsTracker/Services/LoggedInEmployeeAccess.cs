using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesSkillsTracker.Services
{
    public class LoggedInEmployeeAccess : IAuthorizationRequirement
    {
        public int EmployeeId { get;}
        public LoggedInEmployeeAccess(int employeeId)
        {
            EmployeeId = employeeId;
        }
    }

    public class LoggedInEmployeeAccessHandler : AuthorizationHandler<LoggedInEmployeeAccess>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LoggedInEmployeeAccess requirement)
        {
            if (!context.User.HasClaim(c => c.Type == JwtRegisteredClaimNames.Sub))
            {
                return Task.CompletedTask;
            }

            var loggedInId = int.Parse(context.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub).Value);

            if(loggedInId == requirement.EmployeeId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

}
