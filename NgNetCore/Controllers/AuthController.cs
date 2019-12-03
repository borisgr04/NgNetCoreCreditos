﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace NgNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        
        [HttpPost]
        [Route("[action]")]
        public ActionResult<string> Login(LoginViewModel model)
        {
            // Tu código para validar que el usuario ingresado es válido

            // Asumamos que tenemos un usuario válido
            var user = new User
            {
                Name = "Test",
                Email = "test@kodoti.com",
                UserId = "a79b2e64-a4de-4f3a-8cf6-a68ba400db24"
            };

            // Leemos el secret_key desde nuestro appseting
            var secretKey = _configuration.GetValue<string>("SecretKey");
            var key = Encoding.ASCII.GetBytes(secretKey);

            // Creamos los claims (pertenencias, características) del usuario
            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.UserId));
            claims.AddClaim(new Claim(ClaimTypes.Email, user.Email));


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                // Nuestro token va a durar un día
                Expires = DateTime.UtcNow.AddDays(1),
                // Credenciales para generar el token usando nuestro secretykey y el algoritmo hash 256
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(createdToken);
        }
    }

    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
    }
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}