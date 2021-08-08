using Reboard.Core.Domain.Base;
using Reboard.Core.Domain.Shared.Rules;
using System.Collections.Generic;
using static Reboard.Core.Domain.Base.Rules.RuleValidator;

namespace Reboard.Core.Domain.Shared
{
    public class Color : ValueObject
    {
        public int Blue { get; }
        public int Green { get; }
        public int Red { get; }

        private Color(int red, int green, int blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public static Color Make(int red, int green, int blue)
        {
            CheckRule(new InRangeRule(0, 255, red));
            CheckRule(new InRangeRule(0, 255, green));
            CheckRule(new InRangeRule(0, 255, blue));

            return new Color(red, green, blue);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new System.NotImplementedException();
        }
    }
}