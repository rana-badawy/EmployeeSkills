using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesSkillsTracker.Interfaces.Services
{
    public interface IEmailService
    {
        public void sendEmail(string subject, string to, string body);
    }
}
