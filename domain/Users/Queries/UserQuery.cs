using System;
using Reboard.CQRS;

namespace Reboard.Domain.Users.Queries
{
    public class UserQuery : IQuery<User>
    {

        public string Email { get; set; }

    }
}
