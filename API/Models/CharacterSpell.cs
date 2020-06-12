using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class CharacterSpell
    {
        public int CharacterId { get; set; }
        public Character Character { get; set; }
        public int SpellId { get; set; }
        public Spell Spell { get; set; }
    }
}
