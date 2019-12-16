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
            _collection = connection.GetCollection<UserMongoDto>("users");
        }

        public async Task<User> Create(User newEntity)
        {
            try
            {
                await _collection.InsertOneAsync(newEntity.ToDto());
            }
            catch (MongoWriteException mexc) when (mexc.WriteError.Code == 11000)
            {
                throw new DuplicateEntryException(newEntity.Email);
            }
            return await Get(newEntity.Email);
        }

        public async Task Delete(string email)
        {
            await _collection.FindOneAndDeleteAsync(GetFilterByEmail(email));
        }

        public async Task<User> Get(string email)
        {
            var result = await _collection.FindAsync(GetFilterByEmail(email));
            return (await result.FirstOrDefaultAsync()).FromDto();
        }

        public Task<IEnumerable<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<User> Update(User newEntity)
        {
            await _collection.ReplaceOneAsync(GetFilterByEmail(newEntity.Email), newEntity.ToDto());
            return await Get(newEntity.Email);
        }

        private FilterDefinition<UserMongoDto> GetFilterByEmail(string email)
            => new FilterDefinitionBuilder<UserMongoDto>().Eq(dto => dto.Email, email);
    }
}