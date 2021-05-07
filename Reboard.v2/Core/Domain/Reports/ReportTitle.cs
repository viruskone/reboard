using Reboard.Core.Domain.Base;
using System;
using System.Collections.Generic;

namespace Reboard.Core.Domain.Reports
{
    public class ReportTitle : ValueObject
    {
        public string Value { get; }

        private ReportTitle(string value)
        {
            Value = value;
        }

        public static implicit operator ReportTitle(string value) => Make(value);

        public static implicit operator string(ReportTitle title) => title.Value;

        public static ReportTitle Make(string value)
        {
            return new ReportTitle(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}