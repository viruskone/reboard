using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Reboard.Core.Application.Users.Authenticate;
using Reboard.Core.Application.Users.CreateUser;
using Reboard.Presentation.WebApi;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Reboard.Tests.WebApi.IntegrationTests
{
    public class UserTests : IntegrationTestBase
    {
        public UserTests(WebApplicationFactory<Startup> factory, ITestOutputHelper outputHelper)
            : base(factory, outputHelper)
        {
        }

        [Fact]
        public async Task create_user_and_authenticate()
        {
            var client = CreateClient();

            await GetService<IMediator>().Send(
                new CreateUserCommand(nameof(create_user_and_authenticate), "qweasd77!")
            );

            var auth = await ExecuteCommandAndGetResult<AuthenticateRequest, AuthenticateResponse>(client, "api/users/authenticate", new AuthenticateRequest
            {
                Login = nameof(create_user_and_authenticate),
                Password = "qweasd77!"
            }, true);
            auth.Status.Should().Be(AuthenticateStatus.Success);
            auth.Token.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task create_user_and_try_authenticate_with_too_short_password()
        {
            var client = CreateClient();

            await GetService<IMediator>().Send(
                new CreateUserCommand(nameof(create_user_and_authenticate), "qweasd77!")
            );

            var response = await client.PostAsJsonAsync("api/users/authenticate", new AuthenticateRequest
            {
                Login = nameof(create_user_and_authenticate),
                Password = ""
            });
            var responseText = await response.Content.ReadAsStringAsync();
            response.StatusCode.Should().Be(StatusCodes.Status422UnprocessableEntity);
        }
    }
}