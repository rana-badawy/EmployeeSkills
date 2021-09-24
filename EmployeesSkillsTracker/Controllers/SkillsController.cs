using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeesSkillsTracker.Entities;
using EmployeesSkillsTracker.Interfaces;
using EmployeesSkillsTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesSkillsTracker.Controllers
{
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillRepository _skillRepository;

        public SkillsController(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        [HttpGet("api/skills")]
        public ActionResult<IEnumerable<Skill>> GetSkills()
        {
            return Ok(_skillRepository.GetSkills());
        }

        [HttpGet("api/skills/{skillId}", Name = "GetSkill")]
        public ActionResult<Skill> GetSkillByID(int skillId)
        {
            var skill = _skillRepository.GetSkillByID(skillId);

            if (skill == null)
                return NotFound();

            return Ok(skill);
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
        public ActionResult<Skill> CreateSkill(Skill skill)
        {
            _skillRepository.CreateSkill(skill);
            _skillRepository.Save();

            return CreatedAtRoute("GetSkill", new { skillId = skill.SkillID }, skill);
        }

        [HttpPut("api/skills/{skillId}")]
        public ActionResult UpdateSkill(int skillId, Skill skill)
        {
            if (skill == null)
                return BadRequest();

            if (!_skillRepository.SkillExists(skillId))
                return NotFound();

            _skillRepository.UpdateSkill(skill);
            _skillRepository.Save();

            var skillEntity = _skillRepository.GetSkillByID(skillId);

            return Ok(skillEntity);
        }

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
