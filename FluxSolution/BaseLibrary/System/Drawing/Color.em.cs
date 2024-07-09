namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns the ARGB unit values.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Chrominance"/>
    public (double A, double R, double G, double B) ComputeArgb(this System.Drawing.Color source)
      => (
        System.Math.Clamp(source.A / 255d, 0, 1),
        System.Math.Clamp(source.R / 255d, 0, 1),
        System.Math.Clamp(source.G / 255d, 0, 1),
        System.Math.Clamp(source.B / 255d, 0, 1)
      );

    /// <summary>Returns the chroma and associated values for the RGB value.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Chrominance"/>
    public double ComputeChroma(this System.Drawing.Color source, out double a, out double r, out double g, out double b, out double min, out double max)
    {
      (min, max) = ComputeMinMax(source, out a, out r, out g, out b);

      return System.Math.Clamp(max - min, 0, 1);
    }

    /// <summary>Returns the hue [0, 360] for the RGB value.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Hue"/>
    public double ComputeHue(this System.Drawing.Color source, out double a, out double r, out double g, out double b, out double min, out double max, out double chroma)
    {
      chroma = ComputeChroma(source, out a, out r, out g, out b, out min, out max);

      double hue;

      if (chroma == 0) // No range, hue = 0.
        return 0;
      else if (max == r)
        hue = ((g - b) / chroma + 6) % 6; // The % operator doesn't do proper modulo on negative numbers, so we'll add 6 before using it.
      else if (max == g)
        hue = 2 + (b - r) / chroma;
      else // if (max == blue) // No need for comparison, at this point blue must be max.
        hue = 4 + (r - g) / chroma;

      hue *= 60; // Convert to [0, 360] range.

      if (hue < 0)
        hue += 360; // If negative wrap-around to a positive degree in the [0, 360] range.

      return hue;
    }

    /// <summary>Returns the chroma and associated values for the RGB value.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Chrominance"/>
    public (double Min, double Max) ComputeMinMax(this System.Drawing.Color source, out double a, out double r, out double g, out double b)
    {
      (a, r, g, b) = ComputeArgb(source);

      return (
        System.Math.Min(System.Math.Min(r, g), b),
        System.Math.Max(System.Math.Max(r, g), b)
      );
    }

    /// <summary>Returns the luma for the RGB value, using the specified coefficients.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Luma_(video)"/>
    public double ComputeLuma(this System.Drawing.Color source, double rc, double gc, double bc) => rc * source.R + gc * source.G + bc * source.B;

    /// <summary>Returns the luma for the RGB value, using Rec.601 coefficients.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Rec._601"/>
    public double GetLuma601(this System.Drawing.Color source) => ComputeLuma(source, 0.2989, 0.5870, 0.1140);

    /// <summary>Returns the luma for the RGB value, using Adobe/SMPTE 240M coefficients.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Adobe_RGB_color_space"/>
    public double GetLuma240(this System.Drawing.Color source) => ComputeLuma(source, 0.212, 0.701, 0.087);

    /// <summary>Returns the luma for the RGB value, using Rec.709 coefficients.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Rec._709"/>
    public double GetLuma709(this System.Drawing.Color source) => ComputeLuma(source, 0.2126, 0.7152, 0.0722);

    /// <summary>Returns the luma for the RGB value, using Rec.2020 coefficients.</summary>
    public double GetLuma2020(this System.Drawing.Color source) => ComputeLuma(source, 0.2627, 0.6780, 0.0593);

    /// <summary>Creates a CMYK color corresponding to the RGB instance.</summary>
    public (double A, double C, double M, double Y, double K) ToAcmyk(this System.Drawing.Color source)
    {
      var (_, max) = ComputeMinMax(source, out var a, out var r, out var g, out var b);

      var k = 1 - max;
      var ki = 1 - k;
      var c = System.Math.Clamp(ki - r, 0, 1) / ki;
      var m = System.Math.Clamp(ki - g, 0, 1) / ki;
      var y = System.Math.Clamp(ki - b, 0, 1) / ki;

      return (a, c, m, y, k);
    }

    /// <summary>Converts the RGB color to grayscale using the specified method.
    /// <para><see href="https://onlinetools.com/image/grayscale-image"/></para>
    /// </summary>
    public (double A, double R, double G, double B) ToGrayscale(this System.Drawing.Color source, GrayscaleMethod method)
    {
      (a, r, g, b) = ComputeArgb(source);

      return method switch
      {
        GrayscaleMethod.Average => (a, r / 3d, g / 3d, b / 3d),
        GrayscaleMethod.Luminosity601 => (a, r * 0.30, g * 0.59, b * 0.11),
        GrayscaleMethod.Luminosity709 => (a, r * 0.21, g * 0.72, b * 0.07),
        _ => throw new System.ArgumentOutOfRangeException(nameof(method))
      };
    }

    /// <summary>Creates an HWB color corresponding to the RGB instance.</summary>
    public (double A, double H, double W, double B) ToHwb(this System.Drawing.Color source)
    {
      var (min, max) = ComputeMinMax(source, out var a, out var _, out var _, out var _);
      
      return (a, source.GetHue(), min, 1 - max);
    }
  }
}
