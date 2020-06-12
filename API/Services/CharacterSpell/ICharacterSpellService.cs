using API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface ICharacterSpellService
    {
        Task<ServiceResponse<GetCharacterDTO>> Create(AddCharacterSpellDTO skill);
    }
}
