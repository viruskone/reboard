using AspNetCore.Http.Extensions;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reboard.Domain.Auth;
using Reboard.Domain.Users;
using Reboard.IntegrationTests.Mocks;
using Reboard.Repository;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Reboard.IntegrationTests
{
    public class UserDomainTests : IClassFixture<WebApplicationFactory<Reboard.WebServer.Program>>
    {
        private readonly WebApplicationFactory<Reboard.WebServer.Program> _factory;
        private readonly ITestOutputHelper _outputHelper;

        public UserDomainTests(WebApplicationFactory<Reboard.WebServer.Program> factory, ITestOutputHelper outputHelper)
        {
            _factory = factory;
            _outputHelper = outputHelper;
        }

        [Fact]
        public async Task create_user_and_authenticate()
        {
            var email = "example@domain.com";
            var client = _factory
                .WithWebHostBuilder(configuration =>
                {
                    configuration.ConfigureServices(services =>
                    {
                        services.AddTransient<IUserRepository, TestUserRepository>();
                        services.AddTransient<IAuthRepository, TestAuthRepository>();
                    });
                    configuration.ConfigureLogging(x => x.AddXUnit(_outputHelper));
                })
                .CreateClient();
            var user = await ExecuteCommandAndGetResult<CreateUserRequest, User>(client, "api/user", new CreateUserRequest
            {
                Email = email,
                Password = "123"
            });
            user.Email.Should().Be(email);

            var auth = await ExecuteCommandAndGetResult<AuthenticationRequest, Auth>(client, "api/auth", new AuthenticationRequest
            {
                Login = email,
                Password = "123"
            });
            auth.Status.Should().Be(AuthStatus.Success);
            auth.Token.Should().NotBeEmpty();
        }

        private async Task<TResult> ExecuteCommandAndGetResult<TRequest, TResult>(HttpClient client, string commandUrl, TRequest commandPayload)
        {
            var response = await client.PostAsJsonAsync(commandUrl, commandPayload);
            response.StatusCode.Should().Be(HttpStatusCode.Accepted, await response.Content.ReadAsStringAsync());

            response = await WaitWhenCommandExecuted(client, response.Headers.Location);

            var result = await client.GetAsync(response.Headers.Location);
            response.StatusCode.Should().Be(HttpStatusCode.Created, await response.Content.ReadAsStringAsync());
            return await result.Content.ReadAsJsonAsync<TResult>();
        }

        private async Task<HttpResponseMessage> WaitWhenCommandExecuted(HttpClient client, Uri checkStatusUri)
        {
            while (true)
            {
                await Task.Delay(100);
                var response = await client.GetAsync(checkStatusUri);
                response.IsSuccessStatusCode.Should().BeTrue(await response.Content.ReadAsStringAsync());
                if (response.StatusCode != HttpStatusCode.Accepted)
                    return response;
            }
        }
    }
}