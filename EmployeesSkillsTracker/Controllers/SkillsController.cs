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
        private readonly IMapper _mapper;

        public SkillsController(ISkillRepository skillRepository, IMapper mapper)
        {
            _skillRepository = skillRepository;
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
