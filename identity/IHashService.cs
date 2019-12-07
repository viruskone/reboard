using System;

namespace Reboard.Identity
{
    public interface IHashService
    {
        string Encrypt(string content);
    }
}
