using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reboard.IntegrationTests.Mocks
{
    internal abstract class InMemoryRepository<T>
    {
        private readonly static Dictionary<string, T> InMemoryDb = new Dictionary<string, T>();

        protected abstract Func<T, string> KeySelector { get; }

        public Task<T> Create(T newEntity)
        {
            if (!InMemoryDb.ContainsKey(KeySelector(newEntity)))
                InMemoryDb.Add(KeySelector(newEntity), newEntity);
            return Task.FromResult(newEntity);
        }

        public Task Delete(string id)
        {
            InMemoryDb.Remove(id);
            return Task.CompletedTask;
        }

        public Task<T> Get(string id)
            => Task.FromResult(InMemoryDb.ContainsKey(id) ? InMemoryDb[id] : default);

        public Task<IEnumerable<T>> GetAll()
        {
            return Task.FromResult((IEnumerable<T>)InMemoryDb.Values.ToList());
        }

        public Task<T> Update(T newEntity)
        {
            InMemoryDb[KeySelector(newEntity)] = newEntity;
            return Task.FromResult(newEntity);
        }
    }
}