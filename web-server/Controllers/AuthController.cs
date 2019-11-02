using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Reboard.Domain.Auth;
using Reboard.WebServer.Options;

namespace Reboard.WebServer.Controllers
{

    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly string _secretKey;

        public AuthController(IOptions<AppSettings> appSettings)
        {
            _secretKey = appSettings.Value.Secret;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResult>> Login(AuthenticationRequest request)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, request.Login)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return await Task.FromResult(new AuthenticationResult
            {
                Id = 1,
                Name = request.Login,
                Token = tokenString
            });
        }


    }
}