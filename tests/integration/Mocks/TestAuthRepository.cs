using Reboard.Domain.Auth;
using Reboard.Repository;
using System;

namespace Reboard.IntegrationTests.Mocks
{
    internal class TestAuthRepository : InMemoryRepository<Auth>, IAuthRepository
    {
        protected override Func<Auth, string> KeySelector { get; } = entity => entity.RequestId;
    }
}