using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesSkillsTracker.Interfaces.Helpers
{
    public interface IEmailHelper
    {
        public void sendEmail(string subject, string to, string body);
    }
}
