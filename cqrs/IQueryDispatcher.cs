using System.Threading.Tasks;

namespace Reboard.CQRS
{
    public interface IQueryDispatcher
    {
        Task<TResult> HandleAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    }
}