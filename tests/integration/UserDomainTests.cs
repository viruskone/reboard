using AspNetCore.Http.Extensions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Reboard.CQRS;
using Reboard.Domain.Auth;
using Reboard.Domain.Dummy;
using Reboard.Domain.Users;
using Reboard.Domain.Users.Commands;
using Reboard.WebServer;
using System.Net;
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
            var client = CreateClient();

            await GetService<ICommandDispatcher>().HandleAsync(new CreateUserCommand
            {
                Request = new CreateUserRequest
                {
                    Login = nameof(create_user_and_authenticate),
                    Password = "123"
                }
            });

            var auth = await ExecuteCommandAndGetResult<AuthenticationRequest, Auth>(client, "api/auth", new AuthenticationRequest
            {
                Login = nameof(create_user_and_authenticate),
                Password = "123"
            }, true);
            auth.Status.Should().Be(AuthStatus.Success);
            auth.Token.Should().NotBeEmpty();
        }

        [Fact]
        public async Task authentication_failed_test()
        {
            var email = "noexist@domain.com";
            var client = CreateClient();

            var auth = await ExecuteCommandAndGetResult<AuthenticationRequest, Auth>(client, "api/auth", new AuthenticationRequest
            {
                Login = email,
                Password = "abc"
            }, true);
            auth.Status.Should().Be(AuthStatus.Failed);
        }

        [Fact]
        public async Task create_user_and_validate_claims()
        {
            var client = CreateClient();

            var request = new CreateUserRequest
            {
                Login = nameof(create_user_and_validate_claims),
                Company = "Company " + nameof(create_user_and_validate_claims),
                Password = "123"
            };
            await GetService<ICommandDispatcher>().HandleAsync(new CreateUserCommand
            {
                Request = request
            });

            client = await CreateAuthenticatedClient(nameof(create_user_and_validate_claims));
            var result = await client.PostAsJsonAsync("api/dummy/user", new DummyUserRequest { Login = request.Login, Company = request.Company });

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}