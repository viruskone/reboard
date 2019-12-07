using System;
using System.Collections.Generic;

namespace Reboard.Domain.Users
{
    public class UserException : Exception
    {

        public enum ErrorType
        {
            UserAlreadyExist
        }

        private readonly static Dictionary<ErrorType, string> ErrorMessages = new Dictionary<ErrorType, string>{
            { ErrorType.UserAlreadyExist, "User {userId} already exist in repository" }
        };

        public ErrorType Type { get; }

        public UserException(ErrorType type, string userId) : base(MakeMessage(type, userId))
        {
            Type = type;
        }

        private static string MakeMessage(ErrorType type, string userId)
        {
            return
                ErrorMessages.ContainsKey(type) ?
                    ErrorMessages[type].Replace("{userId}", userId) :
                    $"User domain error({type}), user: {userId}";
        }

    }
}
