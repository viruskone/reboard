namespace Reboard.Core.Application.Colors
{
    public struct RangeDouble
    {
        public double Min { get; set; }
        public double Max { get; set; }

        public RangeDouble(double minAndMaxSame)
        {
            Min = minAndMaxSame;
            Max = minAndMaxSame;
        }

        public RangeDouble(double min, double max)
        {
            Min = min;
            Max = max;
        }
    }
}