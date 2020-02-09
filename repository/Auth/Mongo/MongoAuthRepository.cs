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
        private readonly IMongoCollection<SuccessAuthDto> _successAuthCollection;

        public MongoAuthRepository(MongoConnection connection)
        {
            _failedAuthCollection = connection.GetCollection<FailedAuthDto>();
            _successAuthCollection = connection.GetCollection<SuccessAuthDto>();
        }

        public async Task<Domain.Auth.Auth> Create(Domain.Auth.Auth newEntity)
        {
            switch (newEntity.Status)
            {
                case Domain.Auth.AuthStatus.Failed:
                    var failedDto = newEntity.ToFailedDto();
                    await _failedAuthCollection.InsertOneAsync(failedDto);
                    break;
                case Domain.Auth.AuthStatus.Success:
                    var successDto = newEntity.ToSuccessDto();
                    await _successAuthCollection.InsertOneAsync(successDto);
                    break;
            }
            return await Get(newEntity.RequestId);
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Domain.Auth.Auth> Get(string id)
        {
            var failedDto = await GetFromCollection(_failedAuthCollection, id);
            if (failedDto != null)
                return failedDto.FromDto();
 
            var successDto = await GetFromCollection(_successAuthCollection, id);
            if (successDto != null)
                return successDto.FromDto();
 
            return null;
        }

        private async Task<T> GetFromCollection<T>(IMongoCollection<T> collection, string id) where T : AuthDto
        {
            var result = await collection.FindAsyncById(id);
            var dto = await result.FirstOrDefaultAsync();
            return dto;
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