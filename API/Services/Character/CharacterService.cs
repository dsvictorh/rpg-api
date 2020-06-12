using API.Controllers;
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
    public class CharacterService : ICharacterService
    {
        private readonly IMapper mapper;
        private readonly DataContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper;
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        private int GetUserID() 
        {
            return int.Parse(this.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public async Task<ServiceResponse<GetCharacterDTO>> Create(AddCharacterDTO character)
        {
            var response = new ServiceResponse<GetCharacterDTO>();
            var c = this.mapper.Map<Character>(character);
            c.User = await context.Users.FirstOrDefaultAsync(u => u.Id == GetUserID());

            await context.Characters.AddAsync(c);
            await context.SaveChangesAsync();
            response.Data = this.mapper.Map<GetCharacterDTO>(c);
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> Delete(int id)
        {
            var response = new ServiceResponse<GetCharacterDTO>();
            var character = await context.Characters.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserID());

            if (character != null)
            {
                context.Characters.Remove(character);
                await context.SaveChangesAsync();
                response.Data = this.mapper.Map<GetCharacterDTO>(character);
            }
            else
            {
                response.Message = "Character not found";
                response.Success = false;
            }

            return response;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> Get(int id)
        {
            var character = await context.Characters.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserID());
            var response = new ServiceResponse<GetCharacterDTO>();
            response.Data = this.mapper.Map<GetCharacterDTO>(character);
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> List()
        {
            var characters = await context.Characters.Where(c => c.User.Id == GetUserID()).ToListAsync();
            var response = new ServiceResponse<List<GetCharacterDTO>>();
            response.Data = characters.Select(c => this.mapper.Map<GetCharacterDTO>(c)).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> Update(UpdateCharacterDTO character)
        {
            var response = new ServiceResponse<GetCharacterDTO>();

            Character c = await context.Characters.FirstOrDefaultAsync(c => c.Id == character.Id && c.User.Id == GetUserID());

            if (c != null)
            {
                c.Name = character.Name;
                c.Class = character.Class;
                c.Defense = character.Defense;
                c.HitPoint = character.HitPoint;
                c.Intelligence = character.Intelligence;
                c.Strength = character.Strength;

                context.Characters.Update(c);
                await context.SaveChangesAsync();
                response.Data = this.mapper.Map<GetCharacterDTO>(c);
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
