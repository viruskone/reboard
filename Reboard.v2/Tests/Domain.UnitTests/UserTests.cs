using FluentAssertions;
using Moq;
using Reboard.Core.Application.Identity;
using Reboard.Core.Application.Users.Authenticate;
using Reboard.Core.Application.Users.CreateUser;
using Reboard.Core.Domain.Base.Rules;
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
        public async Task commandhandler_create_user()
        {
            var uniqueChecker = new Mock<IUserUniqueLoginChecker>();
            var repository = new Mock<IUserRepository>();
            var handler = new CreateUserHandler(uniqueChecker.Object, repository.Object, new FakeHashService());
            var request = new CreateUserCommand("some@domain.com", "veryhard4break");

            uniqueChecker.Setup(mock => mock.IsUnique(It.IsAny<string>())).ReturnsAsync(true);
            await handler.Handle(request, CancellationToken.None);

            repository.Verify(mock => mock.Save(It.Is<User>(user => user.Login == request.Login && user.Password == request.Password)));
        }

        [Fact]
        public void domain_create_user()
        {
            var rightPassword = "qweasd77!";
            var rightLogin = "user@domain.com";

            var uniqueChecker = new Mock<IUserUniqueLoginChecker>();
            uniqueChecker.Setup(checker => checker.IsUnique(It.IsAny<string>())).ReturnsAsync(true);

            var user = User.CreateNew(rightLogin, Password.MakeNew(rightPassword, new FakeHashService()), uniqueChecker.Object);

            user.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void domain_duplicate_user()
        {
            var rightPassword = "qweasd77!";
            var rightLogin = "user@domain.com";

            var uniqueChecker = new Mock<IUserUniqueLoginChecker>();
            uniqueChecker.Setup(checker => checker.IsUnique(It.IsAny<string>())).ReturnsAsync(false);

            Action userAct = () => User.CreateNew(rightLogin, Password.MakeNew(rightPassword, new FakeHashService()), uniqueChecker.Object);

            (userAct.Should()
                .Throw<BusinessRuleValidationException>())
                .Where(exception => exception.BrokenRule.GetType() == typeof(LoginMustBeUniqueRule));
        }

        [Fact]
        public async Task handler_success_authenticate()
        {
            var repository = new Mock<IUserRepository>();
            var tokenFactory = new Mock<ITokenFactory>();
            var tokenGenerator = new Mock<ITokenGenerator>();
            var handler = new AuthenticateCommandHandler(repository.Object, tokenFactory.Object, new FakeHashService());
            var request = new AuthenticateCommand("some@domain.com", "veryhard4break");

            repository.Setup(mock => mock.Get(It.IsAny<Login>())).ReturnsAsync(User.Make(request.Login, Password.MakeFromEncrypted(request.Password)));
            tokenFactory.Setup(mock => mock.Create()).Returns(tokenGenerator.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            result.Status.Should().Be(AuthenticateStatus.Success);
        }
    }
}