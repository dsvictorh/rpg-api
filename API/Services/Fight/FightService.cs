using API.Data;
using API.DTOs;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class FightService : IFightService
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public FightService(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ServiceResponse<AttackResultDTO>> WeaponAttack(WeaponAttackDTO attack)
        {
            var response = new ServiceResponse<AttackResultDTO>();
            var attacker = await context.Characters
                .Include(c => c.Weapon)
                .FirstOrDefaultAsync(c => c.Id == attack.AttackerId);
            var opponent = await context.Characters
                .FirstOrDefaultAsync(c => c.Id == attack.OpponentId);
            int damage = DoWeaponAttack(attacker, opponent);

            if (opponent.HitPoint <= 0)
            {
                response.Message = $"{opponent.Name} has been defeatead";
            }

            context.Characters.Update(opponent);
            await context.SaveChangesAsync();

            response.Data = new AttackResultDTO
            {
                Attacker = attacker.Name,
                AttackerHP = attacker.HitPoint,
                Opponent = opponent.Name,
                OpponentHP = opponent.HitPoint,
                Damage = damage
            };

            return response;
        }

        private static int DoWeaponAttack(Character attacker, Character opponent)
        {
            int damage = attacker.Weapon.Damage + (new Random().Next(attacker.Strength));
            damage -= new Random().Next(opponent.Defense);

            if (damage > 0)
            {
                opponent.HitPoint -= damage;
            }

            return damage;
        }

        public async Task<ServiceResponse<AttackResultDTO>> SpellAttack(SpellAttackDTO attack)
        {
            var response = new ServiceResponse<AttackResultDTO>();
            var attacker = await context.Characters
                .Include(c => c.CharacterSpells).ThenInclude(cs => cs.Spell)
                .FirstOrDefaultAsync(c => c.Id == attack.AttackerId);
            var opponent = await context.Characters
                .FirstOrDefaultAsync(c => c.Id == attack.OpponentId);

            var characterSpell = attacker.CharacterSpells.FirstOrDefault(cs => cs.Spell.Id == attack.SpellId);

            if (characterSpell != null)
            {
                int damage = DoSpellAttack(attacker, opponent, characterSpell);

                if (opponent.HitPoint <= 0)
                {
                    response.Message = $"{opponent.Name} has been defeatead";
                }

                context.Characters.Update(opponent);
                await context.SaveChangesAsync();

                response.Data = new AttackResultDTO
                {
                    Attacker = attacker.Name,
                    AttackerHP = attacker.HitPoint,
                    Opponent = opponent.Name,
                    OpponentHP = opponent.HitPoint,
                    Damage = damage
                };
            }
            else
            {
                response.Message = $"{attacker.Name} doesn't know that skill";
                response.Success = false;
            }
            

            return response;
        }

        private static int DoSpellAttack(Character attacker, Character opponent, CharacterSpell characterSpell)
        {
            int damage = characterSpell.Spell.Damage + (new Random().Next(attacker.Intelligence));
            damage -= new Random().Next(opponent.Defense);

            if (damage > 0)
            {
                opponent.HitPoint -= damage;
            }

            return damage;
        }

        public async Task<ServiceResponse<GetFightResultDTO>> StartFight(StartFightDTO fight)
        {
            var response = new ServiceResponse<GetFightResultDTO>()
            {
                Data = new GetFightResultDTO()
            };

            var characters = await context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.CharacterSpells).ThenInclude(cs => cs.Spell)
                .Where(c => fight.CharacterIds.Contains(c.Id))
                .ToListAsync();

            bool defeated = false;
            while (!defeated)
            {
                foreach (Character attacker in characters)
                {
                    var opponents = characters.Where(c => c.Id != attacker.Id).ToList();
                    var opponent = opponents[new Random().Next(opponents.Count)];

                    int damage;
                    string attackUsed;

                    bool useWeapon = new Random().Next(2) == 0;
                    if (useWeapon)
                    {
                        attackUsed = attacker.Weapon.Name;
                        damage = DoWeaponAttack(attacker, opponent);
                    }
                    else
                    {
                        var spell = new Random().Next(attacker.CharacterSpells.Count);
                        attackUsed = attacker.CharacterSpells[spell].Spell.Name;
                        damage = DoSpellAttack(attacker, opponent, attacker.CharacterSpells[spell]);
                    }

                    response.Data.Log.Add($"{attacker.Name} attacks {opponent.Name} using {attackUsed} for {(damage > 0 ? damage : 0)} damage.");

                    if (opponent.HitPoint <= 0)
                    {
                        defeated = true;
                        attacker.Victories++;
                        opponent.Defeats++;
                        response.Data.Log.Add($"{opponent.Name} has been defetead.");
                        response.Data.Log.Add($"{attacker.Name} wins with {attacker.HitPoint} HP left");
                        break;
                    }
                }
            }

            characters.ForEach(c =>
            {
                c.Fights++;
                c.HitPoint = 100;
            });

            context.Characters.UpdateRange(characters);
            await context.SaveChangesAsync();
            return response;
        }

        public async Task<ServiceResponse<List<GetHighScoreDTO>>> GetHighScores()
        {
            var response = new ServiceResponse<List<GetHighScoreDTO>>();
            var characters = await context.Characters
                    .Where(c => c.Fights > 0)
                    .OrderByDescending(c => c.Victories)
                    .ThenBy(c => c.Defeats)
                    .ToListAsync();

            response.Data = characters.Select(c => this.mapper.Map<GetHighScoreDTO>(c)).ToList();

            return response;
        }
    }
}
