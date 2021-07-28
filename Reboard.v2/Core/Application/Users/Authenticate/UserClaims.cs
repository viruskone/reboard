using System;

namespace Reboard.Core.Application.Users.Authenticate
{
    public class UserClaims
    {
        public Guid CompanyId { get; set; }
        public Guid UserId { get; set; }
    }
}