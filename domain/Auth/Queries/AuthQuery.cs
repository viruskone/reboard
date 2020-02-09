using Reboard.CQRS;

namespace Reboard.Domain.Auth.Queries
{
    public class AuthQuery : IQuery<Auth>
    {

        public string Id { get; set; }

    }
}
