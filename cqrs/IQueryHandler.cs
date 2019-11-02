using System;
using System.Threading.Tasks;

namespace Reboard.CQRS
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {

        Task<TResult> HandleAsync(TQuery query);

    }
}
