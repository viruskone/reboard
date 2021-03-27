namespace Reboard.WebServer.Architecture
{
    public enum WebSocketAcceptStatus
    {
        ItsNotWebSocketRequest,
        Broken,
        Closed,
        CannotAssignToUser
    }

}