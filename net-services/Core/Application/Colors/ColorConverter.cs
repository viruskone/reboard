using System;

namespace Reboard.Core.Application.Colors
{
    internal static class ColorConverter
    {
        internal static Rgb ToRgb(this Hsv color)
        {
            // ######################################################################
            // T. Nathan Mundhenk
            // mundhenk@usc.edu
            // C/C++ Macro HSV to RGB

            double hue = color.Hue;
            while (hue < 0) { hue += 360; };
            while (hue >= 360) { hue -= 360; };
            double red, green, blue;
            if (color.Value <= 0)
            { red = green = blue = 0; }
            else if (color.Saturation <= 0)
            {
                red = green = blue = color.Value;
            }
            else
            {
                double hf = hue / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = color.Value * (1 - color.Saturation);
                double qv = color.Value * (1 - color.Saturation * f);
                double tv = color.Value * (1 - color.Saturation * (1 - f));
                switch (i)
                {
                    // Red is the dominant color
                    case 0:
                        red = color.Value;
                        green = tv;
                        blue = pv;
                        break;

                    // Green is the dominant color
                    case 1:
                        red = qv;
                        green = color.Value;
                        blue = pv;
                        break;

                    case 2:
                        red = pv;
                        green = color.Value;
                        blue = tv;
                        break;

                    // Blue is the dominant color
                    case 3:
                        red = pv;
                        green = qv;
                        blue = color.Value;
                        break;

                    case 4:
                        red = tv;
                        green = pv;
                        blue = color.Value;
                        break;

                    // Red is the dominant color
                    case 5:
                        red = color.Value;
                        green = pv;
                        blue = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.
                    case 6:
                        red = color.Value;
                        green = tv;
                        blue = pv;
                        break;

                    case -1:
                        red = color.Value;
                        green = pv;
                        blue = qv;
                        break;

                    // The color is not defined, we should throw an error.
                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        red = green = blue = color.Value; // Just pretend its black/white
                        break;
                }
            }
            return new Rgb(
                Clamp((int)(red * 255.0)),
                Clamp((int)(green * 255.0)),
                Clamp((int)(blue * 255.0))
            );
        }

        /// <summary>
        /// Clamp a value to 0-255
        /// </summary>
        private static int Clamp(int i)
        {
            if (i < 0) return 0;
            if (i > 255) return 255;
            return i;
        }
    }
}