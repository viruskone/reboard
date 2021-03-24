using AspNetCore.Http.Extensions;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reboard.CQRS;
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
    public class IntegrationTestBase : IClassFixture<WebApplicationFactory<WebServer.Program>>
    {
        private readonly WebApplicationFactory<WebServer.Program> _factory;
        private readonly ITestOutputHelper _outputHelper;

        private protected TestUserRepository UserRepository { get; } = new TestUserRepository();
        private protected TestAuthRepository AuthRepository { get; } = new TestAuthRepository();

        protected ICommandDispatcher dispatcher;

        public IntegrationTestBase(WebApplicationFactory<WebServer.Program> factory, ITestOutputHelper outputHelper)
        {
            _factory = factory;
            _outputHelper = outputHelper;
        }

        protected HttpClient CreateClient()
        {
            var factory = _factory
                .WithWebHostBuilder(configuration =>
                {
                    configuration.ConfigureServices(services =>
                    {
                        services.AddTransient<IUserRepository>(_ => UserRepository);
                        services.AddTransient<IAuthRepository>(_ => AuthRepository);
                    });
                    configuration.ConfigureLogging(x => x.AddXUnit(_outputHelper));
                });

            dispatcher = factory.Services.GetService(typeof(ICommandDispatcher)) as ICommandDispatcher;
            return factory.CreateClient();
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