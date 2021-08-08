using Reboard.Core.Domain.Base;
using System;

namespace Reboard.Core.Domain.Shared
{
    public class CompanyId : OneValueObject<Guid>
    {
        private CompanyId(Guid id) : base(id)
        {
        }

        public static implicit operator CompanyId(Guid value) => Make(value);

        public static implicit operator Guid(CompanyId id) => id.Value;

        public static CompanyId Make(Guid value)
        {
            return new CompanyId(value);
        }
    }
}