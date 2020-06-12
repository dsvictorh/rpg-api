using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/character-spell")]
    [ApiController]
    public class CharacterSpellController : ControllerBase
    {
        private readonly ICharacterSpellService characterSpellService;

        public CharacterSpellController(ICharacterSpellService characterSpellService)
        {
            this.characterSpellService = characterSpellService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCharacterSpellDTO spell)
        {
            return Ok(await this.characterSpellService.Create(spell));
        }
    }
}
