namespace Reboard.Colors
{
    internal struct Rgb
    {
        public int Red { get; }
        public int Green { get; }
        public int Blue { get; }

        internal Rgb(int red, int green, int blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }
    }

}
