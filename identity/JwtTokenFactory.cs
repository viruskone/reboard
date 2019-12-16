﻿namespace Reboard.Identity
{
    public class JwtTokenFactory : ITokenFactory
    {
        private readonly string _secretKey;

        public JwtTokenFactory(string secretKey)
        {
            _secretKey = secretKey;
        }

        public ITokenGenerator Create() => new JwtTokenGenerator(_secretKey);
    }
}