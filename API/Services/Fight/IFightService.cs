using API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IFightService
    {
        Task<ServiceResponse<AttackResultDTO>> WeaponAttack(WeaponAttackDTO weapon);
        Task<ServiceResponse<AttackResultDTO>> SpellAttack(SpellAttackDTO spell);
        Task<ServiceResponse<GetFightResultDTO>> StartFight(StartFightDTO fight);
        Task<ServiceResponse<List<GetHighScoreDTO>>> GetHighScores();
    }
}
