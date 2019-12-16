using MongoDB.Bson;
using MongoDB.Driver;
using Reboard.Repository.Mongo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reboard.Repository.Auth.Mongo
{
    public class MongoAuthRepository : IAuthRepository
    {
        private readonly IMongoCollection<FailedAuthDto> _failedAuthCollection;

        public MongoAuthRepository(MongoConnection connection)
        {
            _failedAuthCollection = connection.GetCollection<FailedAuthDto>("failed_auths");
        }

        public async Task<Domain.Auth.Auth> Create(Domain.Auth.Auth newEntity)
        {
            var id = new ObjectId();
            switch (newEntity.Status)
            {
                case Domain.Auth.AuthStatus.Failed:
                    var dto = newEntity.ToFailedDto(id);
                    await _failedAuthCollection.InsertOneAsync(dto);
                    break;
            }
            return await Get(id.ToString());
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Domain.Auth.Auth> Get(string id)
        {
            var result = await _failedAuthCollection.FindAsyncById(id);
            return (await result.FirstOrDefaultAsync()).FromDto();
        }

        public Task<IEnumerable<Domain.Auth.Auth>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Auth.Auth> Update(Domain.Auth.Auth newEntity)
        {
            throw new NotImplementedException();
        }
    }
}