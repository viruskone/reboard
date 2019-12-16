using System;

namespace Reboard.Identity
{
    public interface ITokenGenerator
    {
        void SetName(string name);

        void SetExpiration(TimeSpan lifetime);

        string Generate();
    }
}