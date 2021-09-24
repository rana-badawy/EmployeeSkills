using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesSkillsTracker.Models
{
    public class ResponseDto<T>
    {
        public T responseObject { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
