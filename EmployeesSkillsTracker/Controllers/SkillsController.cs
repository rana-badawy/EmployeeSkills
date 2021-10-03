using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EmployeesSkillsTracker.Entities;
using EmployeesSkillsTracker.Interfaces.Repositories;
using EmployeesSkillsTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesSkillsTracker.Controllers
{
    [ApiController]
    [Authorize]
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
        public ActionResult<SkillDto> GetSkillByID(int skillId)
        {
            var skill = _skillRepository.GetSkillByID(skillId);

            if (skill == null)
                return NotFound();

            return Ok(_mapper.Map<SkillDto>(skill));
        }

        [HttpGet("api/skills/{skillId}/employees")]
        public ActionResult<SkillWithEmployeesDto> GetSkillEmployees(int skillId)
        {
            if (int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value) == 1)
            {
                var skill = _skillRepository.GetSkillEmployees(skillId);

                if (skill == null)
                    return NotFound();

                return Ok(_mapper.Map<SkillWithEmployeesDto>(skill));
            }
            throw new UnauthorizedAccessException();
        }

        [HttpPost("api/skills")]
        public ActionResult<Skill> CreateSkill(SkillDto skill)
        {
            if (int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value) == 1)
            {
                var skillEntity = _mapper.Map<Skill>(skill);

                _skillRepository.CreateSkill(skillEntity);
                _skillRepository.Save();

                return Ok();
            }
            throw new UnauthorizedAccessException();
        }

        [HttpPut("api/skills/{skillId}")]
        public ActionResult<Skill> UpdateSkill(int skillId, SkillDto skill)
        {
            if (int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value) == 1)
            {
                if (skill == null)
                    return BadRequest();

                if (!_skillRepository.SkillExists(skillId))
                    return NotFound();

                var skillEntity = _mapper.Map<Skill>(skill);

                _skillRepository.UpdateSkill(skillEntity);
                _skillRepository.Save();

                return Ok();
            }
            throw new UnauthorizedAccessException();
        }

        [HttpDelete("api/skills/{skillId}")]
        public ActionResult DeleteSkill(int skillId)
        {
            if (int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value) == 1)
            {
                var skill = _skillRepository.GetSkillByID(skillId);

                if (skill == null)
                    return NotFound();

                _skillRepository.DeleteSkill(skill);
                _skillRepository.Save();

                return Ok();
            }
            throw new UnauthorizedAccessException();
        }
    }
}
