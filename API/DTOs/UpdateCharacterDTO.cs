using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class UpdateCharacterDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Frodo";
        public int HitPoint { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public Class Class { get; set; } = Class.Knight;
    }
}
