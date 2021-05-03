using FluentAssertions;
using Moq;
using Reboard.Core.Domain.Base;
using Reboard.Core.Domain.Shared;
using Reboard.Core.Domain.Users;
using Reboard.Core.Domain.Users.OutboundServices;
using System.Linq;
using Xunit;

namespace Reboard.Tests.Domain.UnitTests
{
    public class PaswwordTests
    {
        [Fact]
        public void correct_password_test()
        {
            var rightPassword = "qweasd77!";
            var password = Password.Make(rightPassword, new FakeHashService());
            password.EncryptedValue.Should().Be(rightPassword);
        }

        [Theory]
        [InlineData("qwe")]
        [InlineData(null)]
        public void too_short_password_test(string password)
        {
            password.Invoking(s =>
            {
                var pwd = Password.Make(s, new FakeHashService());
            }).Should().Throw<ValidationErrorException>()
            .Where(error => error.Errors.Any(error => error.Code == ValidationErrors.PasswordMinimumLength(1).Code));
        }
    }
}