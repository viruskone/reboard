using System;

namespace Reboard.Identity
{
    public interface ITokenGenerator
    {
        void SetName(string name);

        void SetExpiration(TimeSpan lifetime);

        void AddClaim(string name, string value);

        string Generate();
    }
}