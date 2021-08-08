using Reboard.Core.Domain.Base;
using Reboard.Core.Domain.Shared.Rules;
using System.Collections.Generic;
using System.Diagnostics;
using static Reboard.Core.Domain.Base.Rules.RuleValidator;

namespace Reboard.Core.Domain.Reports
{
    public class ReportTitle : ValueObject
    {
        public string Value { get; }

        private ReportTitle(string value)
        {
            Value = value;
        }

        public static explicit operator ReportTitle(string value) => Make(value);

        public static implicit operator string(ReportTitle title) => title.Value;

        public static ReportTitle Make(string value)
        {
            CheckRule(new NotEmptyRule(value));
            return new ReportTitle(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}