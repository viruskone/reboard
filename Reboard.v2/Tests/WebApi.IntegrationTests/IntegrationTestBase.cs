using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reboard.Core.Domain.Users.OutboundServices;
using Reboard.Presentation.WebApi;
using Reboard.Tests.WebApi.IntegrationTests.Mocks;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Reboard.Tests.WebApi.IntegrationTests
{
    public class IntegrationTestBase : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public IntegrationTestBase(WebApplicationFactory<Startup> factory, ITestOutputHelper outputHelper, Action<IServiceCollection> configureServices = null)
        {
            _factory = PrepareFactory(factory, Concat(s => ConfigureRepositoryServices(s, outputHelper), configureServices));
        }

        protected T GetService<T>() => _factory.Services.GetService<T>();

        protected HttpClient CreateClient() => _factory.CreateClient();

        //protected async Task<HttpClient> CreateAuthenticatedClient(string login = "Example@domain.com", string password = "123")
        //{
        //    var token = await CreateAuthToken(login, password);
        //    var client = CreateClient();
        //    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        //    return client;
        //}

        //private async Task<string> CreateAuthToken(string login, string password)
        //{
        //    var client = CreateClient();

        //    await GetService<ICommandDispatcher>().HandleAsync(new CreateUserCommand
        //    {
        //        Request = new CreateUserRequest
        //        {
        //            Login = login,
        //            Password = password
        //        }
        //    });

        //    var auth = await ExecuteCommandAndGetResult<AuthenticationRequest, Auth>(client, "api/auth", new AuthenticationRequest
        //    {
        //        Login = login,
        //        Password = password
        //    }, true);
        //    return auth.Token;
        //}

        //protected async Task<WebSocket> CreateWebSocketClient(string relativeUrl, string login = "Example@domain.com", string password = "123")
        //{
        //    var baseUri = _factory.Server.BaseAddress;
        //    var wsUri = new UriBuilder()
        //    {
        //        Scheme = baseUri.Scheme == Uri.UriSchemeHttps ? "wss" : "ws",
        //        Host = baseUri.Host,
        //        Port = baseUri.IsDefaultPort ? -1 : baseUri.Port,
        //        Path = relativeUrl
        //    };
        //    var token = await CreateAuthToken(login, password);
        //    var wsClient = _factory.Server.CreateWebSocketClient();
        //    wsClient.ConfigureRequest = request =>
        //    {
        //        request.Headers.Add("sec-websocket-protocol", "Bearer, " + token);
        //    };
        //    return await wsClient.ConnectAsync(wsUri.Uri, CancellationToken.None);
        //}

        private Action<T> Concat<T>(params Action<T>[] actions)
            => x =>
            {
                foreach (var action in actions)
                {
                    action?.Invoke(x);
                }
            };

        private WebApplicationFactory<Startup> PrepareFactory(WebApplicationFactory<Startup> factory, Action<IServiceCollection> configureServices) =>
            factory
                .WithWebHostBuilder(configuration =>
                {
                    configuration.ConfigureServices(configureServices);
                });

        private void ConfigureRepositoryServices(IServiceCollection services, ITestOutputHelper outputHelper)
        {
            services.AddTransient<IUserRepository, MemoryUserRepository>();
            //services.AddTransient<IAuthRepository>(_ => AuthRepository);
            services.AddLogging(x => x.AddXUnit(outputHelper));
        }

        protected async Task<TResult> ExecuteCommandAndGetResult<TRequest, TResult>(HttpClient client, string commandUrl, TRequest commandPayload, bool withoutAcceptedStatus = false)
        {
            var response = await client.PostAsJsonAsync(commandUrl, commandPayload);
            if (withoutAcceptedStatus)
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK, await response.Content.ReadAsStringAsync());
                return await response.Content.ReadAsAsync<TResult>();
            }
            response.StatusCode.Should().Be(HttpStatusCode.Accepted, await response.Content.ReadAsStringAsync());

            response = await WaitWhenCommandExecuted(client, response.Headers.Location);

            var result = await client.GetAsync(response.Headers.Location);
            response.StatusCode.Should().Be(HttpStatusCode.Created, await response.Content.ReadAsStringAsync());
            return await result.Content.ReadAsAsync<TResult>();
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