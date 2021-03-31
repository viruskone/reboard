using Reboard.Domain.Users;
using System;

namespace Reboard.WebServer.Architecture
{
    public interface IUserAccessor
    {
        User Get();
    }

}
