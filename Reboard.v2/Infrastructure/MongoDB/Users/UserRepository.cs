using MongoDB.Driver;
using Reboard.Core.Domain.Users;
using Reboard.Core.Domain.Users.OutboundServices;
using System.Threading.Tasks;

namespace Reboard.Infrastructure.MongoDB.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<UserDto> _collection;

        public UserRepository(MongoConnection connection)
        {
            _collection = connection.GetCollection<UserDto>();
        }

        public async Task<User> Get(Login login)
        {
            var result = await _collection.FindAsync(GetFilterByLogin(login));
            return (await result.FirstOrDefaultAsync()).FromDto();
        }

        public async Task Save(User user)
        {
            await _collection.ReplaceOneAsync(GetFilterByLogin(user.Login), user.ToDto(), new ReplaceOptions { IsUpsert = true });
        }

        private FilterDefinition<UserDto> GetFilterByLogin(string login)
                    => new FilterDefinitionBuilder<UserDto>().Eq(dto => dto.Login, login);
    }
}