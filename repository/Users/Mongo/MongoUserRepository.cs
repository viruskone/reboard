using MongoDB.Driver;
using Reboard.Domain.Users;
using Reboard.Repository.Exceptions;
using Reboard.Repository.Mongo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reboard.Repository.Users.Mongo
{
    public class MongoUserRepository : IUserRepository
    {
        private readonly IMongoCollection<UserMongoDto> _collection;

        public MongoUserRepository(MongoConnection connection)
        {
            _collection = connection.GetCollection<UserMongoDto>();
        }

        public async Task<User> Create(User newEntity)
        {
            try
            {
                await _collection.InsertOneAsync(newEntity.ToDto());
            }
            catch (MongoWriteException mexc) when (mexc.WriteError.Code == 11000)
            {
                throw new DuplicateEntryException(newEntity.Login);
            }
            return await Get(newEntity.Login);
        }

        public async Task Delete(string login)
        {
            await _collection.FindOneAndDeleteAsync(GetFilterByLogin(login));
        }

        public async Task<User> Get(string login)
        {
            var result = await _collection.FindAsync(GetFilterByLogin(login));
            return (await result.FirstOrDefaultAsync()).FromDto();
        }

        public Task<IEnumerable<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<User> Update(User newEntity)
        {
            await _collection.ReplaceOneAsync(GetFilterByLogin(newEntity.Login), newEntity.ToDto());
            return await Get(newEntity.Login);
        }

        private FilterDefinition<UserMongoDto> GetFilterByLogin(string login)
            => new FilterDefinitionBuilder<UserMongoDto>().Eq(dto => dto.Login, login);
    }
}