using FluentAssertions;
using Reboard.Core.Domain.Base;
using Reboard.Core.Domain.Shared;
using Reboard.Core.Domain.Users;
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
            var password = (Password)rightPassword;
            password.Value.Should().Be(rightPassword);
        }

        [Theory]
        [InlineData("qwe")]
        [InlineData(null)]
        public void too_short_password_test(string password)
        {
            password.Invoking(s =>
            {
                var pwd = (Password)s;
            }).Should().Throw<ValidationErrorException>()
            .Where(error => error.Errors.Any(error => error.Code == ValidationErrors.PasswordMinimumLength(1).Code));
        }
    }
}