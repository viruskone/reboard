namespace Reboard.Core.Domain.Base.Rules
{
    public interface IBusinessRule
    {
        string Message { get; }

        bool IsBroken();
    }
}