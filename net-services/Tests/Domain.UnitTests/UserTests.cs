using FluentAssertions;
using Moq;
using Reboard.Core.Application.Identity;
using Reboard.Core.Application.Users.Authenticate;
using Reboard.Core.Application.Users.CreateUser;
using Reboard.Core.Domain.Base.Rules;
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
        public async Task commandhandler_create_user()
        {
            var uniqueChecker = new Mock<IUserUniqueLoginChecker>();
            var companyUniqueChecker = new Mock<ICompanyUniqueNameChecker>();
            var repository = new Mock<IUserRepository>();
            var handler = new CreateUserHandler(uniqueChecker.Object, repository.Object, new FakeHashService(), companyUniqueChecker.Object);
            var request = new CreateUserCommand("some@domain.com", "veryhard4break!", "INC");

            uniqueChecker.Setup(mock => mock.IsUnique(It.IsAny<string>())).ReturnsAsync(true);
            companyUniqueChecker.Setup(mock => mock.IsUnique(It.IsAny<string>())).ReturnsAsync(true);
            await handler.Handle(request, CancellationToken.None);

            repository.Verify(mock => mock.Save(It.Is<User>(user => user.Login == request.Login && user.Password == request.Password)));
            repository.Verify(mock => mock.Save(It.Is<Company>(company => company.Name == request.CompanyName)));
        }

        [Fact]
        public void domain_create_user()
        {
            var rightPassword = "qweasd77!";
            var rightLogin = "user@domain.com";

            var uniqueChecker = new Mock<IUserUniqueLoginChecker>();
            uniqueChecker.Setup(checker => checker.IsUnique(It.IsAny<string>())).ReturnsAsync(true);

            var user = User.CreateNew((Login)rightLogin, Password.MakeNew(rightPassword, new FakeHashService()), Company.Make(Guid.NewGuid(), (CompanyName)"INC"), uniqueChecker.Object);

            user.Id.Value.Should().NotBeEmpty();
        }

        [Fact]
        public void domain_duplicate_user()
        {
            var rightPassword = "qweasd77!";
            var rightLogin = "user@domain.com";

            var uniqueChecker = new Mock<IUserUniqueLoginChecker>();
            uniqueChecker.Setup(checker => checker.IsUnique(It.IsAny<string>())).ReturnsAsync(false);

            Action userAct = () => User.CreateNew((Login)rightLogin, Password.MakeNew(rightPassword, new FakeHashService()), Company.Make(Guid.NewGuid(), (CompanyName)"INC"), uniqueChecker.Object);

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
            var request = new AuthenticateCommand("some@domain.com", "veryhard4break!");

            repository.Setup(mock => mock.Get(It.IsAny<Login>())).ReturnsAsync(User.Make(Guid.NewGuid(), (Login)request.Login, Password.MakeFromEncrypted(request.Password), Company.Make(Guid.NewGuid(), (CompanyName)"INC")));
            tokenFactory.Setup(mock => mock.Create()).Returns(tokenGenerator.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            result.Status.Should().Be(AuthenticateStatus.Success);
        }
    }
}