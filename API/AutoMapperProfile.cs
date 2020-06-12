using API.DTOs;
using API.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDTO>()
                .ForMember(dto => dto.Spells, c => c.MapFrom(c => c.CharacterSpells.Select(cs => cs.Spell)));
            CreateMap<AddCharacterDTO, Character>();
            CreateMap<UpdateCharacterDTO, Character>();
            CreateMap<AddWeaponDTO, Weapon>();
            CreateMap<Weapon, GetWeaponDTO>();
            CreateMap<AddCharacterSpellDTO, CharacterSpell>();
            CreateMap<Spell, GetSpellDTO>();
            CreateMap<Character, GetHighScoreDTO>();
        }
    }
}
