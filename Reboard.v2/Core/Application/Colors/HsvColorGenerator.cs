using System;

namespace Reboard.Core.Application.Colors
{
    public class HsvColorGenerator
    {
        private readonly Random _random = new Random(DateTime.Now.Millisecond);
        private readonly RangeDouble _saturation;
        private readonly RangeDouble _value;

        public HsvColorGenerator(RangeDouble saturation, RangeDouble value)
        {
            _saturation = saturation;
            _value = value;
        }

        public Rgb Generate()
        {
            var hsv = new Hsv(_random.Next(0, 360), InRange(_saturation), InRange(_value));
            var rgb = hsv.ToRgb();
            return rgb;
        }

        private double InRange(RangeDouble range) => _random.NextDouble() * (range.Max - range.Min) + range.Min;
    }
}