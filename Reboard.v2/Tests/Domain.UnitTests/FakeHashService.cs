using Reboard.Core.Domain.Users.OutboundServices;

namespace Reboard.Tests.Domain.UnitTests
{
    public class FakeHashService : IHashService
    {
        public string Encrypt(string content) => content;
    }
}