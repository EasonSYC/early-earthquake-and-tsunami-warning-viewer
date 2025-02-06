using SkiaSharp;

namespace EasonEetwViewer.KyoshinMonitor;

/// <summary>
/// Provides methods to convert between different values.
/// </summary>
public static class ColourConversion
{
    /// <summary>
    /// Converts intensity to normalised height.
    /// </summary>
    /// <param name="intensity">The intensity.</param>
    /// <returns>The normalised height.</returns>
    public static double IntensityToHeight(this double intensity) => (intensity + 3) / 10;
    /// <summary>
    /// Converts PGA to normalised height.
    /// </summary>
    /// <param name="pga">The Peak Ground Acceleration.</param>
    /// <returns>The normalised height.</returns>
    public static double PgaToHeight(this double pga) => (Math.Log(pga, 10) + 2) / 5;
    /// <summary>
    /// Converts PGV to normalised height.
    /// </summary>
    /// <param name="pgv">The Peak Ground Velocity.</param>
    /// <returns>The normalised height.</returns>
    public static double PgvToHeight(this double pgv) => (Math.Log(pgv, 10) + 3) / 5;
    /// <summary>
    /// Converts PGD to normalised height.
    /// </summary>
    /// <param name="pgd">The Peak Ground Displacement.</param>
    /// <returns>The normalised height.</returns>
    public static double PgdToHeight(this double pgd) => (Math.Log(pgd, 10) + 4) / 5;
    /// <summary>
    /// Converts normalised height to intensity.
    /// </summary>
    /// <param name="height">The normalised height.</param>
    /// <returns>The intensity.</returns>
    public static double HeightToIntensity(this double height) => (10 * height) - 3;
    /// <summary>
    /// Converts normalised height to PGA.
    /// </summary>
    /// <param name="height">The normalised height.</param>
    /// <returns>The Peak Ground Acceleration.</returns>
    public static double HeightToPga(this double height) => Math.Pow(10, (5 * height) - 2);
    /// <summary>
    /// Converts normalised height to PGV. 
    /// </summary>
    /// <param name="height">The normalised height.</param>
    /// <returns>The Peak Ground Velocity.</returns>
    public static double HeightToPgv(this double height) => Math.Pow(10, (5 * height) - 3);
    /// <summary>
    /// Converts normalised height to PGD.
    /// </summary>
    /// <param name="height">The normalised height.</param>
    /// <returns>The Peak Ground Displacement.</returns>
    public static double HeightToPgd(this double height) => Math.Pow(10, (5 * height) - 4);
    /// <summary>
    /// Converts normalised height to hue.
    /// </summary>
    /// <param name="height">The normalised height.</param>
    /// <returns>The hue component of the colour.</returns>
    public static double HeightToHue(this double height) => height is <= 0.1
            ? (-150 * height) + 237
            : height is >= 0.1 and <= 0.6
                ? (222 * (height - 0.3) * (height - 0.4) * (height - 0.6) / ((0.1 - 0.3) * (0.1 - 0.4) * (0.1 - 0.6)))
                            + (115 * (height - 0.1) * (height - 0.4) * (height - 0.6) / ((0.3 - 0.1) * (0.3 - 0.4) * (0.3 - 0.6)))
                            + (79.5 * (height - 0.1) * (height - 0.3) * (height - 0.6) / ((0.4 - 0.1) * (0.4 - 0.3) * (0.4 - 0.6)))
                            + (51 * (height - 0.1) * (height - 0.3) * (height - 0.4) / ((0.6 - 0.1) * (0.6 - 0.3) * (0.6 - 0.4)))
                : height is >= 0.6 and <= 0.9 ? (-170 * height) + 153 : 0;
    /// <summary>
    /// Converts normalised height to saturation.
    /// </summary>
    /// <param name="height">The normalised height.</param>
    /// <returns>The saturation component of the colour.</returns>
    public static double HeightToSaturation(this double height) => height is <= 0.2
            ? 1
            : height is >= 0.2 and <= 0.29
                ? (-2.611 * height) + 1.522
                : height is >= 0.29 and <= 0.4 ? (1.682 * height) + 0.277 : height is >= 0.4 and <= 0.5 ? (0.5 * height) + 0.75 : 1;
    /// <summary>
    /// Converts normalised height to value.
    /// </summary>
    /// <param name="height">The normalised height.</param>
    /// <returns>The value component of the colour.</returns>
    public static double HeightToValue(this double height) => height is <= 0.1
            ? (1.8 * height) + 0.8
            : height is >= 0.1 and <= 0.172
                ? (-4.444 * height) + 1.424
                : height is >= 0.172 and <= 0.2
                            ? (5.714 * height) - 0.323
                            : height is >= 0.2 and <= 0.3
                                        ? (1.6 * height) + 0.5
                                        : height is >= 0.3 and <= 0.4
                                                    ? (0.2 * height) + 0.92
                                                    : height is >= 0.4 and <= 0.8 ? 1 : height is >= 0.8 and <= 0.9 ? (-0.3 * height) + 1.24 : (-2.9 * height) + 3.58;
    /// <summary>
    /// Converts normalised height to <see cref="SKColor"/>.
    /// </summary>
    /// <param name="height">The normalised height.</param>
    /// <returns>The colour.</returns>
    public static SKColor HeightToColour(this double height) => SKColor.FromHsv((float)HeightToHue(height), (float)HeightToSaturation(height) * 100, (float)HeightToValue(height) * 100);
    /// <summary>
    /// Converts the <see cref="SKColor"/> to the normalised height.
    /// </summary>
    /// <param name="colour">The colour.</param>
    /// <returns>The normalised height.</returns>
    public static double ColourToHeight(this SKColor colour)
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
