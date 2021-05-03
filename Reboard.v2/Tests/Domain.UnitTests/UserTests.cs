using FluentAssertions;
using Moq;
using Reboard.Core.Application.Identity;
using Reboard.Core.Application.Users.Authenticate;
using Reboard.Core.Application.Users.CreateUser;
using Reboard.Core.Domain.Base;
using Reboard.Core.Domain.Shared;
using Reboard.Core.Domain.Users;
using Reboard.Core.Domain.Users.OutboundServices;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Reboard.Tests.Domain.UnitTests
{
    public class UserTests
    {
        [Fact]
        public async Task correct_user_test()
        {
            var rightPassword = "qweasd77!";
            var rightLogin = "user@domain.com";

            var uniqueChecker = new Mock<IUserUniqueLoginChecker>();
            uniqueChecker.Setup(checker => checker.IsUnique(It.IsAny<string>())).ReturnsAsync(true);

            var user = await User.CreateNew(rightLogin, rightPassword, uniqueChecker.Object);

            user.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task create_user_handler()
        {
            var uniqueChecker = new Mock<IUserUniqueLoginChecker>();
            var repository = new Mock<IUserRepository>();
            var handler = new CreateUserHandler(uniqueChecker.Object, repository.Object);
            var request = new CreateUserCommand("some@domain.com", "veryhard4break");

            uniqueChecker.Setup(mock => mock.IsUnique(It.IsAny<string>())).ReturnsAsync(true);
            await handler.Handle(request, CancellationToken.None);

            repository.Verify(mock => mock.Save(It.Is<User>(user => user.Login == request.Login && user.Password == request.Password)));
        }

        [Fact]
        public async Task duplicate_user_test()
        {
            var rightPassword = "qweasd77!";
            var rightLogin = "user@domain.com";

            var uniqueChecker = new Mock<IUserUniqueLoginChecker>();
            uniqueChecker.Setup(checker => checker.IsUnique(It.IsAny<string>())).ReturnsAsync(false);

            Func<Task> userAct = async () => await User.CreateNew(rightLogin, rightPassword, uniqueChecker.Object);

            (await userAct.Should()
                .ThrowAsync<ValidationErrorException>())
                .Where(exception => exception.Errors.Any(error => error.Code == ValidationErrors.LoginMustBeUnique().Code));
        }

        [Fact]
        public async Task success_authenticate()
        {
            var repository = new Mock<IUserRepository>();
            var tokenFactory = new Mock<ITokenFactory>();
            var tokenGenerator = new Mock<ITokenGenerator>();
            var handler = new AuthenticateCommandHandler(repository.Object, tokenFactory.Object);
            var request = new AuthenticateCommand("some@domain.com", "veryhard4break");

            repository.Setup(mock => mock.ValidatePassword(It.IsAny<Login>(), It.IsAny<Password>())).ReturnsAsync(true);
            tokenFactory.Setup(mock => mock.Create()).Returns(tokenGenerator.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            result.Status.Should().Be(AuthenticateStatus.Success);
        }
    }
}