using Reboard.Core.Application.Users.Authenticate;

namespace Reboard.Presentation.WebApi.Users
{
    public interface IUserAccessor
    {
        UserClaims Get();
    }
}