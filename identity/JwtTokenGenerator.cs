using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Reboard.Identity
{
    public class JwtTokenGenerator : ITokenGenerator
    {
        private readonly byte[] _key;
        private string name;
        private TimeSpan expiration;

        public JwtTokenGenerator(string secretKey)
        {
            _key = Encoding.UTF8.GetBytes(secretKey);
        }

        public string Generate()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, name)
                }),
                Expires = DateTime.UtcNow.Add(expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

        public void SetExpiration(TimeSpan lifetime) => expiration = lifetime;

        public void SetName(string name) => this.name = name;
    }
}