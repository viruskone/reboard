namespace Reboard.Identity
{
    public interface ITokenFactory
    {
        ITokenGenerator Create();
    }
}