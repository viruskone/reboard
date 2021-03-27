using AspNetCore.Http.Extensions;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reboard.CQRS;
using Reboard.IntegrationTests.Mocks;
using Reboard.Repository;
using System;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using System.Linq;
using Reboard.Domain.Users.Commands;
using Reboard.Domain.Users;
using Reboard.Domain.Auth;
using System.Text;

namespace Reboard.IntegrationTests
{
    public class IntegrationTestBase : IClassFixture<WebApplicationFactory<WebServer.Program>>
    {
        private readonly WebApplicationFactory<WebServer.Program> _factory;

        private protected TestUserRepository UserRepository { get; } = new TestUserRepository();
        private protected TestAuthRepository AuthRepository { get; } = new TestAuthRepository();


        public IntegrationTestBase(WebApplicationFactory<WebServer.Program> factory, ITestOutputHelper outputHelper, Action<IServiceCollection> configureServices = null)
        {
            _factory = PrepareFactory(factory, Concat(s => ConfigureRepositoryServices(s, outputHelper), configureServices));
        }

        protected T GetService<T>() => _factory.Services.GetService<T>();

        protected HttpClient CreateClient() => _factory.CreateClient();

        protected async Task<HttpClient> CreateAuthenticatedClient(string login = "Example@domain.com", string password = "123")
        {
            var token = await CreateAuthToken(login, password);
            var client = CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return client;
        }

        private async Task<string> CreateAuthToken(string login, string password)
        {
            var client = CreateClient();

            await GetService<ICommandDispatcher>().HandleAsync(new CreateUserCommand
            {
                Request = new CreateUserRequest
                {
                    Email = login,
                    Password = password
                }
            });

            var auth = await ExecuteCommandAndGetResult<AuthenticationRequest, Auth>(client, "api/auth", new AuthenticationRequest
            {
                Login = login,
                Password = password
            }, true);
            return auth.Token;
        }

        protected async Task<WebSocket> CreateWebSocketClient(string relativeUrl, string login = "Example@domain.com", string password = "123")
        {
            var baseUri = _factory.Server.BaseAddress;
            var wsUri = new UriBuilder()
            {
                Scheme = baseUri.Scheme == Uri.UriSchemeHttps ? "wss" : "ws",
                Host = baseUri.Host,
                Port = baseUri.IsDefaultPort ? -1 : baseUri.Port,
                Path = relativeUrl
            };
            var token = await CreateAuthToken(login, password);
            var wsClient = _factory.Server.CreateWebSocketClient();
            wsClient.ConfigureRequest = request =>
            {
                request.Headers.Add("sec-websocket-protocol", "Bearer, " + token);
            };
            return await wsClient.ConnectAsync(wsUri.Uri, CancellationToken.None);
        }

        private Action<T> Concat<T>(params Action<T>[] actions)
            => x =>
            {
                foreach (var action in actions)
                {
                    action?.Invoke(x);
                }
            };

        private WebApplicationFactory<WebServer.Program> PrepareFactory(WebApplicationFactory<WebServer.Program> factory, Action<IServiceCollection> configureServices) =>
            factory
                .WithWebHostBuilder(configuration =>
                {
                    configuration.ConfigureServices(configureServices);
                });

        private void ConfigureRepositoryServices(IServiceCollection services, ITestOutputHelper outputHelper)
        {
            services.AddTransient<IUserRepository>(_ => UserRepository);
            services.AddTransient<IAuthRepository>(_ => AuthRepository);
            services.AddLogging(x => x.AddXUnit(outputHelper));
        }

        protected async Task<TResult> ExecuteCommandAndGetResult<TRequest, TResult>(HttpClient client, string commandUrl, TRequest commandPayload, bool withoutAcceptedStatus = false)
        {
            var response = await client.PostAsJsonAsync(commandUrl, commandPayload);
            if (withoutAcceptedStatus)
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK, await response.Content.ReadAsStringAsync());
                return await response.Content.ReadAsJsonAsync<TResult>();
            }
            response.StatusCode.Should().Be(HttpStatusCode.Accepted, await response.Content.ReadAsStringAsync());

            response = await WaitWhenCommandExecuted(client, response.Headers.Location);

            var result = await client.GetAsync(response.Headers.Location);
            response.StatusCode.Should().Be(HttpStatusCode.Created, await response.Content.ReadAsStringAsync());
            return await result.Content.ReadAsJsonAsync<TResult>();
        }

        protected async Task<HttpResponseMessage> WaitWhenCommandExecuted(HttpClient client, Uri checkStatusUri)
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