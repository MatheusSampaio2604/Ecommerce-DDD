using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Token;

namespace UI.Controllers
{
    public class TokenController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public TokenController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CreateToken")]
        public async Task<IActionResult> CreateToken([FromBody] InputModel Input)
        {
            if (string.IsNullOrWhiteSpace(Input.Email) || string.IsNullOrWhiteSpace(Input.Password))
                return Unauthorized();

            var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var token = new TokenJWTBuilder()
                     .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
                 .AddSubject("Empresa E-Commerce")
                 .AddIssuer("Teste.Securiry.Bearer")
                 .AddAudience("Teste.Securiry.Bearer")
                 .AddClaim("UsuarioAPINumero", "1")
                 .AddExpiry(5)
                 .Builder();

                return Ok(token.value);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
//token --- //eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJNYXRoZXVzIFNhbXBhaW8iLCJqdGkiOiJjMjBiYjMxMy05YWNiLTRlNWYtYThmOS0zYzI2MWE3MzgwNTUiLCJVc3VhcmlvQVBJTnVtZXJvIjoiMSIsImV4cCI6MTY5MDU1NDQ4NiwiaXNzIjoiTXJSZWFjaGVyLlNlY3VyaXR5LkJlYXJlciIsImF1ZCI6Ik1yUmVhY2hlci5TZWN1cml0eS5CZWFyZXIifQ.rp8PuZs-KqpZ1N9YzG2FxkM2u1TO2bei7eS1VZi2dMQ