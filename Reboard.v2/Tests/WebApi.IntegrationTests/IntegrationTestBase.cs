using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reboard.Core.Application.Users.Authenticate;
using Reboard.Core.Application.Users.CreateUser;
using Reboard.Core.Domain.Reports.OutboundServices;
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

        protected MemoryReportRepository ReportRepository { get; } = new MemoryReportRepository();
        protected MemoryUserRepository UserRepository { get; } = new MemoryUserRepository();

        public IntegrationTestBase(WebApplicationFactory<Startup> factory, ITestOutputHelper outputHelper, Action<IServiceCollection> configureServices = null)
        {
            _factory = PrepareFactory(factory, Concat(
                s => ConfigureRepositoryServices(s),
                s => s.AddLogging(x => x.AddXUnit(outputHelper)),
                configureServices));
        }

        protected async Task<HttpClient> CreateAuthenticatedClient(string login, string company, string password)
        {
            await GetService<IMediator>().Send(
                new CreateUserCommand(login, password, company)
            );
            var httpClient = CreateClient();
            var auth = await ExecuteCommandAndGetResult<AuthenticateRequest, AuthenticateResponse>(httpClient, "api/users/authenticate", new AuthenticateRequest
            {
                Login = login,
                Password = password
            }, true);
            auth.Status.Should().Be(AuthenticateStatus.Success);
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", auth.Token);
            return httpClient;
        }

        protected HttpClient CreateClient() => _factory.CreateClient();

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

        protected T GetService<T>() => _factory.Services.GetService<T>();

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

        private Action<T> Concat<T>(params Action<T>[] actions)
                    => x =>
            {
                foreach (var action in actions)
                {
                    action?.Invoke(x);
                }
            };

        private void ConfigureRepositoryServices(IServiceCollection services)
        {
            services.AddSingleton<IUserRepository>(UserRepository);
            services.AddSingleton<IReportRepository>(ReportRepository);
        }

        private WebApplicationFactory<Startup> PrepareFactory(WebApplicationFactory<Startup> factory, Action<IServiceCollection> configureServices) =>
                    factory
                .WithWebHostBuilder(configuration =>
                {
                    configuration.ConfigureServices(configureServices);
                });
    }
}