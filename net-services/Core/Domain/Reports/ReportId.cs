using Reboard.Core.Domain.Base;
using System;

namespace Reboard.Core.Domain.Reports
{
    public class ReportId : OneValueObject<Guid>
    {
        private ReportId(Guid id) : base(id)
        {
        }

        public static implicit operator ReportId(Guid value) => Make(value);

        public static implicit operator Guid(ReportId id) => id.Value;

        public static ReportId Make(Guid value)
        {
            return new ReportId(value);
        }
    }
}