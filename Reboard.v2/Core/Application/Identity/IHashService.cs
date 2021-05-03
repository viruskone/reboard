namespace Reboard.Core.Application.Identity
{
    public interface IHashService
    {
        string Encrypt(string content);
    }
}