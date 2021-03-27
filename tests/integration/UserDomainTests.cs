using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Reboard.CQRS;
using Reboard.Domain.Auth;
using Reboard.Domain.Users;
using Reboard.Domain.Users.Commands;
using Reboard.WebServer;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Reboard.IntegrationTests
{

    public class UserDomainTests : IntegrationTestBase
    {
        public UserDomainTests(WebApplicationFactory<Program> factory, ITestOutputHelper outputHelper) : base(factory, outputHelper)
        {
        }

        [Fact]
        public async Task create_user_and_authenticate()
        {
            var email = "Example@domain.com";
            var client = CreateClient();

            await GetService<ICommandDispatcher>().HandleAsync(new CreateUserCommand
            {
                Request = new CreateUserRequest
                {
                    Email = email,
                    Password = "123"
                }
            });

            var auth = await ExecuteCommandAndGetResult<AuthenticationRequest, Auth>(client, "api/auth", new AuthenticationRequest
            {
                Login = email,
                Password = "123"
            }, true);
            auth.Status.Should().Be(AuthStatus.Success);
            auth.Token.Should().NotBeEmpty();
        }

        [Fact]
        public async Task authentication_failed_test()
        {
            var email = "example@domain.com";
            var client = CreateClient();

            var auth = await ExecuteCommandAndGetResult<AuthenticationRequest, Auth>(client, "api/auth", new AuthenticationRequest
            {
                Login = email,
                Password = "abc"
            }, true);
            auth.Status.Should().Be(AuthStatus.Failed);
        }

    }
}