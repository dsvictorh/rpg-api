using API.Data;
using API.DTOs;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Services
{
    public class CharacterSpellService : ICharacterSpellService
    {
        private readonly IMapper mapper;
        private readonly DataContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        private int GetUserID()
        {
            return int.Parse(this.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public CharacterSpellService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper;
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> Create(AddCharacterSpellDTO spell)
        {
            var response = new ServiceResponse<GetCharacterDTO>();
            var character = await this.context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.CharacterSpells).ThenInclude(cs => cs.Spell)
                .FirstOrDefaultAsync(c => c.Id == spell.CharacterId && c.User.Id == GetUserID());
            if (character != null)
            {
                var s = await context.Spells.FirstOrDefaultAsync(s => s.Id == spell.SpellId);
                if (s != null)
                {
                    var cs = this.mapper.Map<CharacterSpell>(spell);
                    cs.Character = character;

                    await context.CharacterSpells.AddAsync(cs);
                    await context.SaveChangesAsync();

                    response.Data = this.mapper.Map<GetCharacterDTO>(character);
                }
                else
                {
                    response.Message = "Spell not found";
                    response.Success = false;
                }
            }
            else
            {
                response.Message = "Character not found";
                response.Success = false;
            }

            return response;
        }
    }
}
