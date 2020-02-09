using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reboard.Domain
{
    public interface IRepository<TModel>
    {
        Task<IEnumerable<TModel>> GetAll();

        Task<TModel> Get(string id);

        Task<TModel> Update(TModel newEntity);

        Task<TModel> Create(TModel newEntity);

        Task Delete(string id);
    }
}