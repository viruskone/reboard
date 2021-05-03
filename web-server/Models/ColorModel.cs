using Reboard.Domain;

namespace Reboard.WebServer.Models
{
    public class ColorModel
    {
        public object Red { get; internal set; }
        public object Green { get; internal set; }
        public object Blue { get; internal set; }
    }
    internal static class ColorModelAdapter
    {
        internal static ColorModel FromDomain(this Color color) =>
            new ColorModel
            {
                Red = color.Red,
                Green = color.Green,
                Blue = color.Blue,
            };
    }
}
