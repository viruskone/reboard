using System.Runtime.CompilerServices;
using Reboard.Domain;

[assembly: InternalsVisibleTo("Reboard.UnitTests")]
namespace Reboard.Colors
{
    public class HsvContrastedColorGenerator
    {
        private readonly HsvColorGenerator _colorGenerator;

        public HsvContrastedColorGenerator(double saturation, double value)
            : this(new RangeDouble(saturation), new RangeDouble(value)) { }
            
        public HsvContrastedColorGenerator(RangeDouble saturation, RangeDouble value)
        {
            _colorGenerator = new HsvColorGenerator(saturation, value);
        }

        public Color GetContrastedColor(Color contrastColor, double minimumContrast)
        {
            Color result = null;
            do
            {
                result = _colorGenerator.Generate();
            } while (GetContrastRatio(contrastColor, result) < minimumContrast);
            return result;
        }

        public double GetContrastRatio(Color color, Color contrastColor) =>
            ContrastRatio.Contrast(Convert(color), Convert(contrastColor));

        private Rgb Convert(Color color) => new Rgb(color.Red, color.Green, color.Blue);

    }

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
