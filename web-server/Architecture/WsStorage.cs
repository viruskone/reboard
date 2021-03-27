using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.WebSockets;

namespace Reboard.WebServer.Architecture
{
    internal static class WsStorage
    {
        private static ConcurrentDictionary<string, List<WebSocket>> storage = new ConcurrentDictionary<string, List<WebSocket>>();

        internal static void Register(string id, WebSocket webSocket) =>
            storage.AddOrUpdate(id, _ => new List<WebSocket>(new WebSocket[] { webSocket }), (_, list) => { list.Add(webSocket); return list; });

        internal static List<WebSocket> GetWs(string id) =>
            storage.ContainsKey(id)
            && storage.TryGetValue(id, out var list) ?
                list :
                new List<WebSocket>();

        internal static void Unregister(string userId, WebSocket webSocket)
        {
            if (!storage.ContainsKey(userId)) return;
            storage[userId].Remove(webSocket);
        }
    }

}