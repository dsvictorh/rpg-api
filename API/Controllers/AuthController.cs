using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO user)
        {
            var response = await authRepository.Register(new User 
            { 
                Username = user.Username
            },
            user.Password);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO user)
        {
            var response = await authRepository.Login(user.Username, user.Password);

            return Ok(response);
        }
    }
}
