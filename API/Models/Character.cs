using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public enum Class 
    {
        Knight = 1,
        Mage,
        Cleric
    }

    public class Character
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
        public User User { get; set; }
        public Weapon Weapon { get; set; }
        public List<CharacterSpell> CharacterSpells { get; set; }
    }
}
