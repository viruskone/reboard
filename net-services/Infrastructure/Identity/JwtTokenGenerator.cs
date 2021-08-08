using Microsoft.IdentityModel.Tokens;
using Reboard.Core.Application.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Reboard.Infrastructure.Identity
{
    public class JwtTokenGenerator : ITokenGenerator
    {
        private readonly byte[] _key;
        private string name;
        private TimeSpan expiration = TimeSpan.FromMinutes(5);
        private Dictionary<string, string> claims = new Dictionary<string, string>();

        public JwtTokenGenerator(string secretKey)
        {
            _key = Encoding.UTF8.GetBytes(secretKey);
        }

        public string Generate()
        {
            var subjectClaims = new List<Claim>();
            subjectClaims.Add(new Claim(ClaimTypes.Name, name));
            subjectClaims.AddRange(claims.ToList().Select(kvp => new Claim(kvp.Key, kvp.Value)));
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(subjectClaims),
                Expires = DateTime.UtcNow.Add(expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

        public void SetExpiration(TimeSpan lifetime) => expiration = lifetime;

        public void SetName(string name) => this.name = name;

        public void AddClaim(string name, string value) => claims.Add(name, value);
    }
}