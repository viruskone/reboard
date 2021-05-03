namespace Reboard.Core.Domain.Users.OutboundServices
{
    public interface IHashService
    {
        string Encrypt(string content);
    }
}