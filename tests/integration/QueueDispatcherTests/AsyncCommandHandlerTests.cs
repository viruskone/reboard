using AspNetCore.Http.Extensions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Reboard.CQRS;
using Reboard.Domain.Dummy;
using Reboard.WebServer;
using Reboard.WebServer.Architecture;
using System;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Reboard.IntegrationTests.QueueDispatcherTests
{
    public class AsyncCommandHandlerTests : IntegrationTestBase
    {
        public AsyncCommandHandlerTests(WebApplicationFactory<Program> factory, ITestOutputHelper outputHelper)
            : base(factory, outputHelper, new Action<IServiceCollection>(ConfigureServices)) { }

        [Fact]
        public async Task dispatch_command_and_get_result_by_ws()
        {
            var queueDispatcher = GetService<IQueueCommandDispatcher>();
            queueDispatcher.Should().NotBeNull();

            var socket = await CreateWebSocketClient("/api/command/ws", nameof(dispatch_command_and_get_result_by_ws));
            socket.State.Should().Be(WebSocketState.Open);


            var client = await CreateAuthenticatedClient(nameof(dispatch_command_and_get_result_by_ws));
            var dummyResponse = await client.PostAsJsonAsync("api/dummy/command", new DummyRequest { PastAsResponse = "abc" });
            dummyResponse.StatusCode.Should().Be(HttpStatusCode.Accepted);

            JobStatus status = null;
            do
            {
                var buffer = new byte[1024];
                var receivedTask = socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                DummyCommandHandler.Event.Set();
                var result = await receivedTask;
                status = JsonSerializer.Deserialize<JobStatus>(Encoding.Default.GetString(buffer, 0, result.Count));
            } while (status.Status == CommandStatus.Running);
            status.Status.Should().Be(CommandStatus.Completed);

        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddCqrs(typeof(AsyncCommandHandlerTests).Assembly);
        }

    }

}
