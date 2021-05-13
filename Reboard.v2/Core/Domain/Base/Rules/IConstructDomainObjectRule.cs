namespace Reboard.Core.Domain.Base.Rules
{
    public interface IConstructDomainObjectRule
    {
        string Reason { get; }

        bool IsInvalid();
    }
}