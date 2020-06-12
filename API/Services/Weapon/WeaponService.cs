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
    public class WeaponService : IWeaponService
    {
        private readonly IMapper mapper;
        private readonly DataContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        private int GetUserID()
        {
            return int.Parse(this.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public WeaponService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper;
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> Create(AddWeaponDTO weapon)
        {
            var response = new ServiceResponse<GetCharacterDTO>();
            var character = await this.context.Characters.FirstOrDefaultAsync(c => c.Id == weapon.CharacterId && c.User.Id == GetUserID());
            if (character != null)
            {
                var w = this.mapper.Map<Weapon>(weapon);
                w.Character = character;

                await context.Weapons.AddAsync(w);
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
    }
}
