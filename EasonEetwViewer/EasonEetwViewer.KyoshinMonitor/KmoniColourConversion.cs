using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;

namespace EasonEetwViewer.KyoshinMonitor;
public class KmoniColourConversion
{
    public static double IntensityToHeight(double intensity) => (intensity + 3) / 10;
    public static double PgaToHeight(double pga) => (Math.Log(pga, 10) + 2) / 5;
    public static double PgvToHeight(double pgv) => (Math.Log(pgv, 10) + 3) / 5;
    public static double PgdToHeight(double pgd) => (Math.Log(pgd, 10) + 4) / 5;
    public static double HeightToIntensity(double height) => 10 * height - 3;
    public static double HeightToPga(double height) => Math.Pow(10, 5 * height - 2);
    public static double HeightToPgv(double height) => Math.Pow(10, 5 * height - 3);
    public static double HeightToPgd(double height) => Math.Pow(10, 5 * height - 4);
    public static double HeightToHue(double height)
    {
        if (height is <= 0.1)
        {
            return -150 * height + 237;
        }
        else if (height is >= 0.1 and <= 0.6)
        {
            return 222 * (height - 0.3) * (height - 0.4) * (height - 0.6) / ((0.1 - 0.3) * (0.1 - 0.4) * (0.1 - 0.6))
                + 115 * (height - 0.1) * (height - 0.4) * (height - 0.6) / ((0.3 - 0.1) * (0.3 - 0.4) * (0.3 - 0.6))
                + 79.5 * (height - 0.1) * (height - 0.3) * (height - 0.6) / ((0.4 - 0.1) * (0.4 - 0.3) * (0.4 - 0.6))
                + 51 * (height - 0.1) * (height - 0.3) * (height - 0.4) / ((0.6 - 0.1) * (0.6 - 0.3) * (0.6 - 0.4));
        }
        else if (height is >= 0.6 and <= 0.9)
        {
            return -170 * height + 153;
        }
        else
        {
            return 0;
        }
    }
    public static double HeightToSaturation(double height)
    {
        if (height is <= 0.2)
        {
            return 1;
        }
        else if (height is >= 0.2 and <= 0.29)
        {
            return -2.611 * height + 1.522;
        }
        else if (height is >= 0.29 and <= 0.4)
        {
            return 1.682 * height + 0.277;
        }
        else if (height is >= 0.4 and <= 0.5)
        {
            return 0.5 * height + 0.75;
        }
        else
        {
            return 1;
        }
    }
    public static double HeightToValue(double height)
    {
        if (height is <= 0.1)
        {
            return 1.8 * height + 0.8;
        }
        else if (height is >= 0.1 and <= 0.172)
        {
            return -4.444 * height + 1.424;
        }
        else if (height is >= 0.172 and <= 0.2)
        {
            return 5.714 * height - 0.323;
        }
        else if (height is >= 0.2 and <= 0.3)
        {
            return 1.6 * height + 0.5;
        }
        else if (height is >= 0.3 and <= 0.4)
        {
            return 0.2 * height + 0.92;
        }
        else if (height is >= 0.4 and <= 0.8)
        {
            return 1;
        }
        else if (height is >= 0.8 and <= 0.9)
        {
            return -0.3 * height + 1.24;
        }
        else
        {
            return -2.9 * height + 3.58;
        }
    }
    public static double ColourToHeight(SKColor colour)
    {
        colour.ToHsv(out float h, out float _, out float v);

        v /= 100; // Since SkiaSharp use percentage for v without the pecentage sign

        if (h == 0) // Use v
        {
            return (v - 3.58) / -2.9;
        }
        else // Use h
        {
            if (h >= 222)
            {
                return (h - 237) / -150;
            }

            if (h <= 51)
            {
                return (h - 153) / -170;
            }

            // Binary Search for Numerical Inverse Cubic

            double left = 0.1;
            double right = 0.6;
            const double heightEpsilon = 0.01;
            const double hueEpsilon = 0.5;
            while (right - left >= heightEpsilon)
            {
                double mid = (right + left) / 2;
                double calculatedHue = HeightToHue(mid);
                if (Math.Abs(calculatedHue - h) <= hueEpsilon)
                {
                    return mid;
                }
                if (calculatedHue > h)
                {
                    left = mid;
                }
                if (calculatedHue < h)
                {
                    right = mid;
                }
            }
            return (right + left) / 2;
        }
    }
}
