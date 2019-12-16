using Reboard.Domain.Users;
using Reboard.Repository;
using System;

namespace Reboard.IntegrationTests.Mocks
{

    internal class TestUserRepository : InMemoryRepository<User>, IUserRepository
    {
        protected override Func<User, string> KeySelector { get; } = entity => entity.Email;
    }
}