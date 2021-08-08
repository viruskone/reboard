namespace Reboard.Core.Application.Colors
{
    public class Rgb
    {
        public int Blue { get; set; }
        public int Green { get; set; }
        public int Red { get; set; }

        public Rgb()
        {
        }

        public Rgb(int red, int green, int blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }
    }
}