using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/fight")]
    [ApiController]
    public class FightController : ControllerBase
    {

        private readonly IFightService fightService;

        public FightController(IFightService fightService)
        {
            this.fightService = fightService;
        }

        [HttpPost("weapon")]
        public async Task<IActionResult> Weapon(WeaponAttackDTO attack)
        {
            return Ok(await this.fightService.WeaponAttack(attack));
        }

        [HttpPost("spell")]
        public async Task<IActionResult> Spell(SpellAttackDTO attack)
        {
            return Ok(await this.fightService.SpellAttack(attack));
        }

        [HttpPost]
        public async Task<IActionResult> Fight(StartFightDTO fight)
        {
            return Ok(await this.fightService.StartFight(fight));
        }

        [HttpGet]
        public async Task<IActionResult> HighScore()
        {
            return Ok(await this.fightService.GetHighScores());
        }
    }
}
