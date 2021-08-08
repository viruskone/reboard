namespace Reboard.Core.Application.Identity
{
    public interface ITokenFactory
    {
        ITokenGenerator Create();
    }
}