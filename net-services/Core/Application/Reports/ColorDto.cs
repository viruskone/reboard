namespace Reboard.Core.Application.Reports
{
    public class ColorDto
    {
        public int Blue { get; set; }
        public int Green { get; set; }
        public int Red { get; set; }

        public ColorDto()
        {
        }

        public ColorDto(int red, int green, int blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }
    }
}