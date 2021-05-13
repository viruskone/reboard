namespace Reboard.Core.Application.Colors
{
    internal struct Hsv
    {
        public int Hue { get; }
        public double Saturation { get; }
        public double Value { get; }

        internal Hsv(int hue, double saturation, double value)
        {
            Hue = hue;
            Saturation = saturation;
            Value = value;
        }
    }
}