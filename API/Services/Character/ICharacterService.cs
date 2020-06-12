using API.DTOs;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDTO>>> List();
        Task<ServiceResponse<GetCharacterDTO>> Get(int id);
        Task<ServiceResponse<GetCharacterDTO>> Create(AddCharacterDTO character);
        Task<ServiceResponse<GetCharacterDTO>> Update(UpdateCharacterDTO character);
        Task<ServiceResponse<GetCharacterDTO>> Delete(int id);
    }
}
