using System;

namespace Reboard.WebServer.Architecture
{
    public interface IUniqueIdFactory
    {
        string Next();
    }
}
