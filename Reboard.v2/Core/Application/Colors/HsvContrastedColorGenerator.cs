using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reboard.Core.Application.Colors
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

        public Rgb GetContrastedColor(Rgb contrastColor, double minimumContrast)
        {
            Rgb result = null;
            do
            {
                result = _colorGenerator.Generate();
            } while (GetContrastRatio(contrastColor, result) < minimumContrast);
            return result;
        }

        public double GetContrastRatio(Rgb color, Rgb contrastColor) =>
            ContrastRatio.Contrast(color, contrastColor);
    }
}