using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Reboard.CQRS;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Reboard.WebServer.Architecture
{
    internal class WsProvider : IWsProvider, INotification
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<WsProvider> _logger;

        public WsProvider(IHttpContextAccessor contextAccessor, ILogger<WsProvider> logger)
        {
            _contextAccessor = contextAccessor;
            _logger = logger;
        }

        public async Task<WebSocketAcceptStatus> Accept()
        {
            if (!_contextAccessor.HttpContext.WebSockets.IsWebSocketRequest)
            {
                return WebSocketAcceptStatus.ItsNotWebSocketRequest;
            }
            var webSocket = await _contextAccessor.HttpContext.WebSockets.AcceptWebSocketAsync("Bearer");
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                await webSocket.CloseAsync(WebSocketCloseStatus.InvalidPayloadData, "Try open connection on not authorized user", CancellationToken.None);
                return WebSocketAcceptStatus.CannotAssignToUser;
            }
            WsStorage.Register(userId, webSocket);
            var status = await WebSocketWaitLoop(webSocket);
            WsStorage.Unregister(userId, webSocket);
            return status;
        }

        private string GetUserId() =>
            _contextAccessor.HttpContext.User.Identity.IsAuthenticated ?
            _contextAccessor.HttpContext.User.Identity.Name :
            string.Empty;

        private async Task<WebSocketAcceptStatus> WebSocketWaitLoop(WebSocket webSocket)
        {
            var buffer = new byte[1024];
            while (webSocket.State.HasFlag(WebSocketState.Open))
            {
                try
                {
                    var received =
                        await webSocket.ReceiveAsync(buffer, CancellationToken.None);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Receiving from WS failed");
                    return WebSocketAcceptStatus.Broken;
                }
            }

            if (webSocket.State != WebSocketState.Closed &&
                webSocket.State != WebSocketState.Aborted)
            {
                try
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure,
                        "Socket closed",
                        CancellationToken.None);
                }
                catch
                {
                    // this may throw on shutdown and can be ignored
                }
            }
            return WebSocketAcceptStatus.Closed;

        }

        public void RegisterJob(Job job)
        {
            var user = _contextAccessor.HttpContext.User.Identity.Name;
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                job.StatusChanged += async (sender, e) => await JobStatusChanged(sender, e, user);
            }
        }

        private async Task JobStatusChanged(object sender, JobStatusChangedEventArgs e, string user)
        {
            var job = sender as Job;
            var status = new JobStatus { JobId = job.Id, Status = e.NewStatus };
            var sockets = WsStorage.GetWs(user);
            foreach (var socket in sockets)
            {
                await SendJobStatus(socket, status);
            }
        }

        private async Task SendJobStatus(WebSocket socket, JobStatus status)
        {
            var txt = JsonSerializer.Serialize(status);
            await socket.SendAsync(new ArraySegment<byte>(Encoding.Default.GetBytes(txt)), WebSocketMessageType.Text, true, CancellationToken.None);
        }

    }

}