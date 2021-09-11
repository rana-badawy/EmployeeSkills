using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeesSkillsTracker.Entities;
using EmployeesSkillsTracker.Models;
using EmployeesSkillsTracker.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesSkillsTracker.Controllers
{
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public SkillsController(ISkillRepository skillRepository, IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _skillRepository = skillRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        [HttpGet("api/skills")]
        public ActionResult<IEnumerable<SkillDto>> GetSkills()
        {
            return Ok(_mapper.Map<IEnumerable<SkillDto>>(_skillRepository.GetSkills()));
        }

        [HttpGet("api/skills/{skillId}", Name = "GetSkill")]
        public ActionResult<Skill> GetSkillByID(int skillId)
        {
            var skill = _skillRepository.GetSkillByID(skillId);

            if (skill == null)
                return NotFound();

            return Ok(_mapper.Map<SkillDto>(skill));
        }

        [HttpGet("api/skills/{skillId}/employees")]
        public ActionResult<Skill> GetSkillEmployees(int skillId)
        {
            var skill = _skillRepository.GetSkillEmployees(skillId);

            if (skill == null)
                return NotFound();

            return Ok(skill);
        }

        [HttpPost("api/skills")]
        public ActionResult<SkillDto> CreateSkill(Skill skill)
        {
            var skillEntity = _mapper.Map<Skill>(skill);

            _skillRepository.CreateSkill(skillEntity);
            _skillRepository.Save();

            var createdSkill = _mapper.Map<SkillDto>(skillEntity);

            return CreatedAtRoute("GetSkill", new { skillId = createdSkill.SkillID }, createdSkill);
        }

        [HttpPut("api/skills/{skillId}")]
        public ActionResult UpdateSkill(int skillId, SkillDto skill)
        {
            if (skill == null || skillId != skill.SkillID)
                return NotFound();

            var skillEntity = _skillRepository.GetSkillByID(skillId);

            if (skillEntity == null)
                return NotFound();

            _mapper.Map(skill, skillEntity);

            _skillRepository.UpdateSkill(skillEntity);

            _skillRepository.Save();

            return NoContent();
        }

        /*
        [HttpPost("api/employees/{employeeId}/skills")]
        public ActionResult<SkillDto> CreateSkillForEmployee(Skill skill, int employeeId)
        {
            if (_employeeRepository.GetEmployeeByID(employeeId) == null)
                return NotFound();


        }*/

        [HttpDelete("api/skills/{skillId}")]
        public ActionResult DeleteSkill(int skillId)
        {
            var skill = _skillRepository.GetSkillByID(skillId);

            if (skill == null)
                return NotFound();

            _skillRepository.DeleteSkill(skill);
            _skillRepository.Save();

            return NoContent();
        }
    }
}
