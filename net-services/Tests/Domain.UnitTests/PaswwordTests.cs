using FluentAssertions;
using Moq;
using Reboard.Core.Domain.Base;
using Reboard.Core.Domain.Base.Rules;
using Reboard.Core.Domain.Users;
using Reboard.Core.Domain.Users.OutboundServices;
using System.Linq;
using Xunit;

namespace Reboard.Tests.Domain.UnitTests
{
    public class PaswwordTests
    {
        [Fact]
        public void domain_correct_password()
        {
            var rightPassword = "qweasd77!";
            var password = Password.MakeNew(rightPassword, new FakeHashService());
            password.EncryptedValue.Should().Be(rightPassword);
        }
    }
}