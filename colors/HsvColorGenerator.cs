using System;

namespace Reboard.Colors
{

    public class HsvColorGenerator
    {
        private readonly RangeDouble _saturation;
        private readonly RangeDouble _value;

        private readonly Random _random = new Random(DateTime.Now.Millisecond);

        public HsvColorGenerator(RangeDouble saturation, RangeDouble value)
        {
            _saturation = saturation;
            _value = value;
        }

        public Domain.Color Generate()
        {
            var hsv = new Hsv(_random.Next(0, 360), InRange(_saturation), InRange(_value));
            var rgb = hsv.ToRgb();
            return new Domain.Color
            {
                Red = rgb.Red,
                Green = rgb.Green,
                Blue = rgb.Blue
            };
        }

        private double InRange(RangeDouble range) => _random.NextDouble() * (range.Max - range.Min) + range.Min;

    }

}
