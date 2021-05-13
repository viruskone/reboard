using Reboard.Core.Domain.Base;
using System;

namespace Reboard.Core.Domain.Shared
{
    public class UserId : OneValueObject<Guid>
    {
        private UserId(Guid id) : base(id)
        {
        }

        public static explicit operator UserId(Guid value) => Make(value);

        public static implicit operator Guid(UserId id) => id.Value;

        public static UserId Make(Guid value)
        {
            return new UserId(value);
        }
    }
}