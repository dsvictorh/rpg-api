using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class AddCharacterDTO
    {
        public string Name { get; set; }
        public int HitPoint { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int Intelligence { get; set; }
        public Class Class { get; set; }
    }
}
