using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    [Route("api/character")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService characterService;

        public CharacterController(ICharacterService characterService)
        {
            this.characterService = characterService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
           return Ok(await this.characterService.List());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await this.characterService.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCharacterDTO character)
        {
            return Ok(await this.characterService.Create(character));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCharacterDTO character)
        {
            return Ok(await this.characterService.Update(character));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await this.characterService.Delete(id));
        }
    }
}
