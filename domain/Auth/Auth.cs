using System;

namespace Reboard.Domain.Auth
{
    public class Auth
    {
        public string RequestId { get; set; }
        public AuthStatus Status { get; set; }
        public DateTime Time { get; set; }
        public string Token { get; set; }
    }
}