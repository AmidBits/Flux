namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns the chroma [0, 1] and other related values for the <see cref="System.Drawing.Color"/> as out parameters in alpha, red, green, blue, min, and max.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Chrominance"/></para>
    /// </summary>
    public static double ComputeChroma(this System.Drawing.Color source, out double alpha, out double red, out double green, out double blue, out double min, out double max)
    {
      (min, max) = ComputeMinMax(source, out alpha, out red, out green, out blue);

      return double.Clamp(max - min, 0, 1);
    }

    /// <summary>
    /// <para>Returns the hue [0, 360] and other related values for the <see cref="System.Drawing.Color"/> as out parameters in alpha, red, green, blue, min, max, and chroma.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Hue"/></para>
    /// </summary>
    public static double ComputeHue(this System.Drawing.Color source, out double alpha, out double red, out double green, out double blue, out double min, out double max, out double chroma)
    {
      chroma = ComputeChroma(source, out alpha, out red, out green, out blue, out min, out max);

      double hue;

      if (chroma == 0) // No range, hue = 0.
        return 0;
      else if (max == red)
        hue = ((green - blue) / chroma + 6) % 6; // The % operator doesn't do proper modulo on negative numbers, so we'll add 6 before using it.
      else if (max == green)
        hue = 2 + (blue - red) / chroma;
      else // if (max == blue) // No need for comparison, at this point blue must be max.
        hue = 4 + (red - green) / chroma;

      hue *= 60; // Convert to [0, 360] range.

      if (hue < 0)
        hue += 360; // If negative wrap-around to a positive degree in the [0, 360] range.

      return double.Clamp(hue, 0, 360);
    }

    /// <summary>
    /// <para>Returns the min/max of the red, green and blue values from the <see cref="System.Drawing.Color"/>. Also returns the color as unit values in the out parameters alpha, red, green, blue.</para>
    /// </summary>
    public static (double Min, double Max) ComputeMinMax(this System.Drawing.Color source, out double alpha, out double red, out double green, out double blue)
    {
      (alpha, red, green, blue) = ToArgb(source);

      return (
        double.Min(double.Min(red, green), blue),
        double.Max(double.Max(red, green), blue)
      );
    }

    /// <summary>
    /// <para>Returns the luma for the RGB value, using the specified coefficients.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Luma_(video)"/></para>
    /// </summary>
    public static double ComputeLuma(this System.Drawing.Color source, double rc, double gc, double bc) => rc * source.R + gc * source.G + bc * source.B;

    /// <summary>
    /// <para>Returns the luma for the RGB value, using Adobe/SMPTE 240M coefficients.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Adobe_RGB_color_space"/></para>
    /// </summary>
    public static double ComputeLuma240(this System.Drawing.Color source) => ComputeLuma(source, 0.212, 0.701, 0.087);

    /// <summary>
    /// <para>Returns the luma for the RGB value, using Rec.601 coefficients.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Rec._601"/></para>
    /// </summary>
    public static double ComputeLuma601(this System.Drawing.Color source) => ComputeLuma(source, 0.2989, 0.5870, 0.1140);

    /// <summary>
    /// <para>Returns the luma for the RGB value, using Rec.709 coefficients.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Rec._709"/></para>
    /// </summary>
    public static double ComputeLuma709(this System.Drawing.Color source) => ComputeLuma(source, 0.2126, 0.7152, 0.0722);

    /// <summary>
    /// <para>Returns the luma for the RGB value, using Rec.2020 coefficients.</para>
    /// </summary>
    public static double ComputeLuma2020(this System.Drawing.Color source) => ComputeLuma(source, 0.2627, 0.6780, 0.0593);

    /// <summary>
    /// <para>Returns the chroma and hue [0.0, 360.0] for the RGB value.</para>
    /// </summary>
    public static (double Chroma2, double Hue2) ComputeSecondaryChromaAndHue(this System.Drawing.Color source)
    {
      var (a, r, g, b) = ToArgb(source);

      var alpha = (2 * r - g - b) / 2;
      var beta = (double.Sqrt(3) / 2) * (g - b);

      return (
        double.Sqrt(alpha * alpha + beta * beta),
        double.RadiansToDegrees(double.Atan2(beta, alpha)).WrapAround(0, 360)
      );
    }

    /// <summary>
    /// <para>Creates ACMYK unit values corresponding to the <see cref="System.Drawing.Color"/>.</para>
    /// </summary>
    public static (double A, double C, double M, double Y, double K) ToAcmyk(this System.Drawing.Color source)
    {
      var (_, max) = ComputeMinMax(source, out var a, out var r, out var g, out var b);

      var k = 1 - max;
      var ki = 1 - k;
      var c = double.Clamp(ki - r, 0, 1) / ki;
      var m = double.Clamp(ki - g, 0, 1) / ki;
      var y = double.Clamp(ki - b, 0, 1) / ki;

      return (a, c, m, y, k);
    }

    /// <summary>
    /// <para>Creates AHSI unit values corresponding to the <see cref="System.Drawing.Color"/>.</para>
    /// </summary>
    public static (double A, double H, double S, double I) ToAhsi(this System.Drawing.Color source)
    {
      var (min, _) = source.ComputeMinMax(out var alpha, out var red, out var green, out var blue);

      var i = (red + green + blue) / 3;
      var s = i == 0 ? 0 : 1 - (min / i);

      return (
        alpha,
        source.GetHue(),
        s,
        i
      );
    }

    /// <summary>
    /// <para>Creates AHSL unit values corresponding to the <see cref="System.Drawing.Color"/>.</para>
    /// </summary>
    public static (double A, double H, double S, double L) ToAhsl(this System.Drawing.Color source)
    {
      var chroma = source.ComputeChroma(out var alpha, out var red, out var green, out var blue, out var min, out var max);

      var l = 0.5 * (max + min);
      var s = l == 0 || l == 1 ? 0 : double.Clamp(chroma / (1 - double.Abs(2 * l - 1)), 0, 1);

      return (
        alpha,
        source.GetHue(),
        s,
        l
      );
    }

    /// <summary>
    /// <para>Creates AHSV unit values corresponding to the <see cref="System.Drawing.Color"/>.</para>
    /// </summary>
    public static (double A, double H, double S, double V) ToAhsv(this System.Drawing.Color source)
    {
      var chroma = source.ComputeChroma(out var alpha, out var red, out var green, out var blue, out var min, out var max);

      return (
        alpha,
        source.GetHue(),
        max == 0 ? 0 : chroma / max, // S
        max // V
      );
    }

    /// <summary>
    /// <para>Creates AHWB unit values corresponding to the <see cref="System.Drawing.Color"/>.</para>
    /// </summary>
    public static (double A, double H, double W, double B) ToAhwb(this System.Drawing.Color source)
    {
      var (min, max) = ComputeMinMax(source, out var a, out var _, out var _, out var _);

      return (a, source.GetHue(), min, 1 - max);
    }

    /// <summary>
    /// <para>Creates ARGB unit values corresponding to the <see cref="System.Drawing.Color"/>.</para>
    /// </summary>
    public static (double A, double R, double G, double B) ToArgb(this System.Drawing.Color source)
      => (
        double.Clamp(source.A / 255d, 0, 1),
        double.Clamp(source.R / 255d, 0, 1),
        double.Clamp(source.G / 255d, 0, 1),
        double.Clamp(source.B / 255d, 0, 1)
      );

    /// <summary>
    /// <para>Creates grayscale ARGB unit values corresponding to the <see cref="System.Drawing.Color"/> using the specified grayscale method.</para>
    /// <para><see href="https://onlinetools.com/image/grayscale-image"/></para>
    /// </summary>
    public static (double A, double R, double G, double B) ToArgbGrayscale(this System.Drawing.Color source, Flux.Colors.GrayscaleMethod method)
    {
      const double OneThird = 1d / 3d;

      return method switch
      {
        Flux.Colors.GrayscaleMethod.Average => ToArgbScaled(source, 1, OneThird, OneThird, OneThird),
        Flux.Colors.GrayscaleMethod.Luminosity601 => ToArgbScaled(source, 1, 0.30, 0.59, 0.11),
        Flux.Colors.GrayscaleMethod.Luminosity709 => ToArgbScaled(source, 1, 0.21, 0.72, 0.07),
        _ => throw new System.ArgumentOutOfRangeException(nameof(method))
      };
    }

    /// <summary>
    /// <para>Creates scaled ARGB unit values corresponding to the <see cref="System.Drawing.Color"/>.</para>
    /// </summary>
    public static (double A, double R, double G, double B) ToArgbScaled(this System.Drawing.Color source, double sa, double sr, double sg, double sb)
    {
      (sa, sr, sg, sb) = ToArgb(source);

      return (sa * sa, sr * sr, sg * sg, sb * sb);
    }

    public static string ToHtmlColorString(this System.Drawing.Color source) => $"rgb({source.R}, {source.G}, {source.B})";

    public static string ToHtmlHexString(this System.Drawing.Color source) => $"#{source.R:X2}{source.G:X2}{source.B:X2}";
  }
}
