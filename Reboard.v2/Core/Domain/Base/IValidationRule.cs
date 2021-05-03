using Reboard.Core.Domain.Base.Errors;
using System.Threading.Tasks;

namespace Reboard.Core.Domain.Base
{
    public interface IValidationErrorMaker
    {
        ValidationError GetError();
    }

    public interface IValidationRule<T> : IValidationErrorMaker
    {
        bool IsBroken(T value);
    }

    public interface IAsyncValidationRule<T> : IValidationErrorMaker
    {
        Task<bool> IsBroken(T value);
    }
}