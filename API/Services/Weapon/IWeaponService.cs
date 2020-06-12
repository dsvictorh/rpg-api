using API.DTOs;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDTO>> Create(AddWeaponDTO weapon);
    }
}
