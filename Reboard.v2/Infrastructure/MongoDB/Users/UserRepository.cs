using MongoDB.Driver;
using Reboard.Core.Domain.Users;
using Reboard.Core.Domain.Users.OutboundServices;
using System;
using System.Threading.Tasks;

namespace Reboard.Infrastructure.MongoDB.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<CompanyDto> _companyCollection;
        private readonly IMongoCollection<UserDto> _userCollection;

        public UserRepository(MongoConnection connection)
        {
            _userCollection = connection.GetCollection<UserDto>();
            _companyCollection = connection.GetCollection<CompanyDto>();
        }

        public async Task<User> Get(Login login)
        {
            var result = await _userCollection.FindAsync(GetFilterByLogin(login));
            var firstOrDefaultResult = await result.FirstOrDefaultAsync();
            if (firstOrDefaultResult == null) return null;
            var company = await GetCompany((CompanyId)firstOrDefaultResult.CompanyId);
            return firstOrDefaultResult.FromDto(company);
        }

        public async Task<Company> GetCompany(CompanyName name)
        {
            var filter = new FilterDefinitionBuilder<CompanyDto>().Eq(dto => dto.Name, name.Value);
            var resultCursor = await _companyCollection.FindAsync(filter);
            var resultDto = await resultCursor.FirstOrDefaultAsync();
            return resultDto?.FromDto();
        }

        public async Task Save(User user)
        {
            await _userCollection.ReplaceOneAsync(GetFilterByLogin(user.Login), user.ToDto(), new ReplaceOptions { IsUpsert = true });
        }

        public async Task Save(Company company)
        {
            var filter = new FilterDefinitionBuilder<CompanyDto>().Eq(dto => dto.Id, company.Id.Value);
            await _companyCollection.ReplaceOneAsync(filter, company.ToDto(), new ReplaceOptions { IsUpsert = true });
        }

        private async Task<Company> GetCompany(CompanyId companyId)
        {
            var filter = new FilterDefinitionBuilder<CompanyDto>().Eq(dto => dto.Id, companyId.Value);
            var resultCursor = await _companyCollection.FindAsync(filter);
            var resultDto = await resultCursor.FirstOrDefaultAsync();
            return resultDto?.FromDto();
        }

        private FilterDefinition<UserDto> GetFilterByLogin(string login)
                    => new FilterDefinitionBuilder<UserDto>().Eq(dto => dto.Login, login);
    }
}