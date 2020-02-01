using System;

namespace Reboard.Colors
{
    internal static class ContrastRatio
    {

        private static double LumOne(int value, double multi)
        {
            var v1 = value / 255d;
            v1 =
                v1 <= 0.03928 ?
                v1 / 12.92 :
                Math.Pow((v1 + 0.055) / 1.055, 2.4);
            return v1 * multi;
        }

        private static double Luminance(Rgb color) =>
            LumOne(color.Red, 0.2126)
            + LumOne(color.Green, 0.7152)
            + LumOne(color.Blue, 0.0722);

        internal static double Contrast(Rgb rgb1, Rgb rgb2)
        {
            var l1 = Luminance(rgb1) + 0.05;
            var l2 = Luminance(rgb2) + 0.05;
            return l1 > l2 ? l1 / l2 : l2 / l1;
        }
    }
}