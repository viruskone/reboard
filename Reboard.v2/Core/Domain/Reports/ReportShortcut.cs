using Reboard.Core.Domain.Base;
using Reboard.Core.Domain.Shared.Rules;
using System.Collections.Generic;
using static Reboard.Core.Domain.Base.Rules.RuleValidator;

namespace Reboard.Core.Domain.Reports
{
    public class ReportShortcut : ValueObject
    {
        public string Value { get; }

        private ReportShortcut(string value)
        {
            Value = value;
        }

        public static explicit operator ReportShortcut(string value) => Make(value);

        public static implicit operator string(ReportShortcut title) => title.Value;

        public static ReportShortcut Make(string value)
        {
            CheckRule(new NotEmptyRule(value));
            CheckRule(new MaxLengthRule(value, 4));

            return new ReportShortcut(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}