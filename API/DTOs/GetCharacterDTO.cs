using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class GetCharacterDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HitPoint { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int Intelligence { get; set; }
        public Class Class { get; set; }
        public int Fights { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }
        public GetWeaponDTO Weapon { get; set; }
        public List<GetSpellDTO> Spells { get; set; }
    }
}
