using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/weapon")]
    [ApiController]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService weaponService;

        public WeaponController(IWeaponService weaponService)
        {
            this.weaponService = weaponService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddWeaponDTO weapon)
        {
            return Ok(await this.weaponService.Create(weapon));
        }
    }
}
