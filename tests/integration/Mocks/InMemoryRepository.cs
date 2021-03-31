using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reboard.IntegrationTests.Mocks
{
    internal abstract class InMemoryRepository<T>
    {
        private readonly Dictionary<string, T> InMemoryDb = new Dictionary<string, T>();

        protected abstract Func<T, string> KeySelector { get; }

        public Task<T> Create(T newEntity)
        {
            try
            {
                InMemoryDb.TryAdd(KeySelector(newEntity), newEntity);
            }
            catch (NullReferenceException)
            {
                if (InMemoryDb == null)
                    throw new Exception("Puste DB");
                if(newEntity == null)
                    throw new Exception("Puste entity");
                if (KeySelector == null)
                    throw new Exception("Pusty selector");
            }
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