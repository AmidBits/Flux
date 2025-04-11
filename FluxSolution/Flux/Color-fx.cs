namespace Flux
{
  public static partial class Color
  {
    public enum GrayscaleMethod
    {
      /// <summary>Plain average of all colors.</summary>
      Average,
      ///// <summary>Averages the most prominent and least prominent colors.</summary>
      //Lightness,
      /// <summary>
      /// <para>A weighted average based on human perception.</para>
      /// <para>The ITU-R BT.601 formula (also known as PAL/NTSC formula) utilizes the formula: <code>0.30 * Red + 0.59 * Green + 0.11 * Blue</code></para>
      /// </summary>
      Luminosity601,
      /// <summary>
      /// <para>A second weighted average based on human perception.</para>
      /// <para>The ITU-R BT.709 formula (also known as HDTV formula), which applies different weights to the color channels according to the formula: <code>0.21 * Red + 0.72 * Green + 0.07 * Blue</code></para>
      /// </summary>
      Luminosity709,
    }

    /// <summary>
    /// <para>Returns the chroma [0, 1] and other related values for the <see cref="System.Drawing.Color"/> as out parameters in alpha, red, green, blue, min, and max.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Chrominance"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="alpha"></param>
    /// <param name="red"></param>
    /// <param name="green"></param>
    /// <param name="blue"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static double ComputeChroma(this System.Drawing.Color source, out double alpha, out double red, out double green, out double blue, out double min, out double max)
    {
      (min, max) = ComputeMinMax(source, out alpha, out red, out green, out blue);

      return double.Clamp(max - min, 0, 1);
    }

    /// <summary>
    /// <para>Returns the hue [0, 360] and other related values for the <see cref="System.Drawing.Color"/> as out parameters in alpha, red, green, blue, min, max, and chroma.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Hue"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="alpha"></param>
    /// <param name="red"></param>
    /// <param name="green"></param>
    /// <param name="blue"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="chroma"></param>
    /// <returns></returns>
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
    /// <param name="source"></param>
    /// <param name="alpha"></param>
    /// <param name="red"></param>
    /// <param name="green"></param>
    /// <param name="blue"></param>
    /// <returns></returns>
    public static (double Min, double Max) ComputeMinMax(this System.Drawing.Color source, out double alpha, out double red, out double green, out double blue)
    {
      (alpha, red, green, blue) = source.ToArgbUnitIntervals();

      return (
        double.Min(double.Min(red, green), blue),
        double.Max(double.Max(red, green), blue)
      );
    }

    /// <summary>
    /// <para>Returns the luma for the RGB value, using the specified coefficients.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Luma_(video)"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="rc"></param>
    /// <param name="gc"></param>
    /// <param name="bc"></param>
    /// <returns></returns>
    public static double ComputeLuma(this System.Drawing.Color source, double rc, double gc, double bc)
      => rc * source.R + gc * source.G + bc * source.B;

    /// <summary>
    /// <para>Returns the luma for the RGB value, using Adobe/SMPTE 240M coefficients.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Adobe_RGB_color_space"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static double ComputeLuma240(this System.Drawing.Color source) => ComputeLuma(source, 0.212, 0.701, 0.087);

    /// <summary>
    /// <para>Returns the luma for the RGB value, using Rec.601 coefficients.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Rec._601"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static double ComputeLuma601(this System.Drawing.Color source) => ComputeLuma(source, 0.2989, 0.5870, 0.1140);

    /// <summary>
    /// <para>Returns the luma for the RGB value, using Rec.709 coefficients.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Rec._709"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static double ComputeLuma709(this System.Drawing.Color source) => ComputeLuma(source, 0.2126, 0.7152, 0.0722);

    /// <summary>
    /// <para>Returns the luma for the RGB value, using Rec.2020 coefficients.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static double ComputeLuma2020(this System.Drawing.Color source) => ComputeLuma(source, 0.2627, 0.6780, 0.0593);

    /// <summary>
    /// <para>Returns the chroma and hue [0.0, 360.0] for the RGB value.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static (double Chroma2, double Hue2) ComputeSecondaryChromaAndHue(this System.Drawing.Color source)
    {
      var (_, r, g, b) = source.ToArgbUnitIntervals();

      var alpha = (2 * r - g - b) / 2;
      var beta = (double.Sqrt(3) / 2) * (g - b);

      return (
        double.Sqrt(alpha * alpha + beta * beta),
        double.RadiansToDegrees(double.Atan2(beta, alpha)).WrapAround(0, 360)
      );
    }

    /// <summary>
    /// <para>Convert HSL unit values to HSV unit values.</para>
    /// </summary>
    /// <param name="hue"></param>
    /// <param name="saturation"></param>
    /// <param name="lightness"></param>
    /// <returns></returns>
    public static (double Hue, double Saturation, double Value) ConvertHslToHsv(double hue, double saturation, double lightness)
    {
      var value = lightness + saturation * double.Min(lightness, 1 - lightness);

      return (
        hue,
        value == 0 ? 0 : 2 * (1 - lightness / value), // Saturation
        value
      );
    }

    /// <summary>
    /// <summary>Convert HSV unit values to HSL unit values.</summary>
    /// </summary>
    /// <param name="hue"></param>
    /// <param name="saturation"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static (double Hue, double Saturation, double Lightness) ConvertHsvToHsl(double hue, double saturation, double value)
    {
      var lightness = value * (1 - saturation / 2);

      return (
        hue,
        lightness == 0 || lightness == 1 ? 0 : (value - lightness) / double.Min(lightness, 1 - lightness), // Saturation
        lightness
      );
    }

    /// <summary>
    /// <para>Converts HSV unit values to HWB unit values.</para>
    /// </summary>
    /// <param name="hue"></param>
    /// <param name="saturation"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static (double Hue, double White, double Black) ConvertHsvToHwb(double hue, double saturation, double value)
      => (
        hue,
        (1 - saturation) * value, // White
        1 - value // Black
      );

    /// <summary>
    /// <para>Converts HWB unit values to HSV unit values.</para>
    /// </summary>
    /// <param name="hue"></param>
    /// <param name="white"></param>
    /// <param name="black"></param>
    /// <returns></returns>
    public static (double Hue, double Saturation, double Value) ConvertHwbToHsv(double hue, double white, double black)
    {
      var value = 1 - black;

      return (
        hue,
        value > 0 ? double.Max(1 - (white / value), 0) : 0, // Saturation
        value
      );
    }

    /// <summary>
    /// <para>Creates a new <see cref="System.Drawing.Color"/> from ACMYK unit values.</para>
    /// </summary>
    /// <param name="alpha"></param>
    /// <param name="cyan"></param>
    /// <param name="magenta"></param>
    /// <param name="yellow"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static System.Drawing.Color FromAcmyk(double alpha, double cyan, double magenta, double yellow, double key)
    {
      var yek = 1 - key;

      return System.Drawing.Color.FromArgb(
        System.Convert.ToByte(255 * alpha),
        System.Convert.ToByte(255 * (1 - cyan) * yek),
        System.Convert.ToByte(255 * (1 - magenta) * yek),
        System.Convert.ToByte(255 * (1 - yellow) * yek)
      );
    }

    /// <summary>
    /// <para>Creates a new <see cref="System.Drawing.Color"/> from AHSI unit values.</para>
    /// </summary>
    /// <param name="alpha"></param>
    /// <param name="hue"></param>
    /// <param name="saturation"></param>
    /// <param name="intensity"></param>
    /// <returns></returns>
    public static System.Drawing.Color FromAhsi(double alpha, double hue, double saturation, double intensity)
    {
      var c = 3 * intensity * saturation / (1 + (1 - double.Abs((hue / 60 % 2) - 1)));
      var h = hue / 60;
      var x = c * (1 - double.Abs((h % 2) - 1));

      var m = intensity * (1 - saturation);

      var r = m;
      var g = m;
      var b = m;

      switch (h)
      {
        case var v1 when v1 < 1:
          r += c;
          g += x;
          break;
        case var v2 when v2 < 2:
          r += x;
          g += c;
          break;
        case var v3 when v3 < 3:
          g += c;
          b += x;
          break;
        case var v4 when v4 < 4:
          g += x;
          b += c;
          break;
        case var v5 when v5 < 5:
          r += x;
          b += c;
          break;
        default: // h1 <= 6 //
          r += c;
          b += x;
          break;
      }

      return System.Drawing.Color.FromArgb(
        System.Convert.ToByte(255 * alpha),
        System.Convert.ToByte(255 * r),
        System.Convert.ToByte(255 * g),
        System.Convert.ToByte(255 * b)
      );
    }

    /// <summary>
    /// <para>Creates a new <see cref="System.Drawing.Color"/> from HSL unit values.</para>
    /// </summary>
    /// <param name="alpha"></param>
    /// <param name="hue"></param>
    /// <param name="saturation"></param>
    /// <param name="lightness"></param>
    /// <returns></returns>
    public static System.Drawing.Color FromAhsl(double alpha, double hue, double saturation, double lightness)
    {
      var c = (1 - double.Abs(2 * lightness - 1)) * saturation;
      var h = hue / 60;
      var x = c * (1 - double.Abs((h % 2) - 1));

      var m = lightness - (0.5 * c);

      var r = m;
      var g = m;
      var b = m;

      switch (h)
      {
        case var v1 when v1 < 1:
          r += c;
          g += x;
          break;
        case var v2 when v2 < 2:
          r += x;
          g += c;
          break;
        case var v3 when v3 < 3:
          g += c;
          b += x;
          break;
        case var v4 when v4 < 4:
          g += x;
          b += c;
          break;
        case var v5 when v5 < 5:
          r += x;
          b += c;
          break;
        default: // h1 <= 6 //
          r += c;
          b += x;
          break;
      }

      return System.Drawing.Color.FromArgb(
        System.Convert.ToByte(255 * alpha),
        System.Convert.ToByte(255 * r),
        System.Convert.ToByte(255 * g),
        System.Convert.ToByte(255 * b)
       );
    }

    /// <summary>
    /// <para>Creates a new <see cref="System.Drawing.Color"/> from AHSV unit values.</para>
    /// </summary>
    /// <param name="alpha"></param>
    /// <param name="hue"></param>
    /// <param name="saturation"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static System.Drawing.Color FromAhsv(double alpha, double hue, double saturation, double value)
    {
      var c = value * saturation;
      var h = hue / 60;
      var x = c * (1 - double.Abs((h % 2) - 1));

      var m = value - c;

      var r = m;
      var g = m;
      var b = m;

      switch (h)
      {
        case var v1 when v1 < 1:
          r += c;
          g += x;
          break;
        case var v2 when v2 < 2:
          r += x;
          g += c;
          break;
        case var v3 when v3 < 3:
          g += c;
          b += x;
          break;
        case var v4 when v4 < 4:
          g += x;
          b += c;
          break;
        case var v5 when v5 < 5:
          r += x;
          b += c;
          break;
        default: // h1 <= 6 //
          r += c;
          b += x;
          break;
      }

      return System.Drawing.Color.FromArgb(
        System.Convert.ToByte(255 * alpha),
        System.Convert.ToByte(255 * r),
        System.Convert.ToByte(255 * g),
        System.Convert.ToByte(255 * b)
       );
    }

    /// <summary>
    /// <para>Creates a new <see cref="System.Drawing.Color"/> from AHWB unit values.</para>
    /// </summary>
    /// <param name="alpha"></param>
    /// <param name="hue"></param>
    /// <param name="white"></param>
    /// <param name="black"></param>
    /// <returns></returns>
    public static System.Drawing.Color FromAhwb(double alpha, double hue, double white, double black)
    {
      var h1 = hue / 360;
      var wh = white;
      var bl = black;

      if (wh + bl is var ratio && ratio > 1)
      {
        wh /= ratio;
        bl /= ratio;
      }

      var whole = (int)(h1 * 6);
      var fraction = (h1 * 6) - whole;

      if ((whole & 1) == 1)
        fraction = 1 - fraction;

      var v = 1 - bl; // Unit inverse.

      var n = wh + fraction * (v - wh);

      double r, g, b;

      switch (whole)
      {
        default:
        case 6:
        case 0: r = v; g = n; b = wh; break;
        case 1: r = n; g = v; b = wh; break;
        case 2: r = wh; g = v; b = n; break;
        case 3: r = wh; g = n; b = v; break;
        case 4: r = n; g = wh; b = v; break;
        case 5: r = v; g = wh; b = n; break;
      }

      return System.Drawing.Color.FromArgb(
        System.Convert.ToByte(255 * alpha),
        System.Convert.ToByte(255 * r),
        System.Convert.ToByte(255 * g),
        System.Convert.ToByte(255 * b)
      );
    }

    /// <summary>
    /// <para>Creates a new <see cref="System.Drawing.Color"/> from a 4-byte <see cref="System.ReadOnlySpan{T}"/>.</para>
    /// </summary>
    /// <param name="argb"></param>
    /// <returns></returns>
    public static System.Drawing.Color FromArgbBytes(System.ReadOnlySpan<byte> argb)
      => System.Drawing.Color.FromArgb(argb[0], argb[1], argb[2], argb[3]);

    /// <summary>
    /// <para>Creates a new <see cref="System.Drawing.Color"/> from a <see cref="System.Random"/> number generator.</para>
    /// </summary>
    /// <param name="rng"></param>
    /// <returns></returns>
    public static System.Drawing.Color FromRandom(System.Random? rng = null)
      => FromArgbBytes((rng ?? System.Random.Shared).GetRandomBytes(4));

    /// <summary>
    /// <para>Creates ACMYK unit values from a <see cref="System.Drawing.Color"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static (double Alpha, double Cyan, double Magenta, double Yellow, double Key) ToAcmyk(this System.Drawing.Color source)
    {
      var (_, max) = ComputeMinMax(source, out var alpha, out var red, out var green, out var blue);

      var key = 1 - max;
      var yek = 1 - key;

      return (
        alpha,
        double.Clamp(yek - red, 0, 1) / yek, // Cyan
        double.Clamp(yek - green, 0, 1) / yek, // Magenta
        double.Clamp(yek - blue, 0, 1) / yek, // Yellow
        key
      );
    }

    /// <summary>
    /// <para>Creates AHSI unit values from a <see cref="System.Drawing.Color"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static (double Alpha, double Hue, double Saturation, double Intensity) ToAhsi(this System.Drawing.Color source)
    {
      var hue = source.ComputeHue(out var alpha, out var red, out var green, out var blue, out var min, out var _, out var _);

      var intensity = (red + green + blue) / 3;

      return (
        alpha,
        hue,
        (intensity == 0) ? 0 : 1 - (min / intensity), // Saturation
        intensity
      );
    }

    /// <summary>
    /// <para>Creates AHSL unit values from a <see cref="System.Drawing.Color"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static (double Alpha, double Hue, double Saturation, double Lightness) ToAhsl(this System.Drawing.Color source)
    {
      var hue = source.ComputeHue(out var alpha, out var _, out var _, out var _, out var min, out var max, out var chroma);

      var lightness = 0.5 * (max + min);

      return (
        alpha,
        hue,
        (lightness == 0 || lightness == 1) ? 0 : double.Clamp(chroma / (1 - double.Abs(2 * lightness - 1)), 0, 1), // Saturation
        lightness
      );
    }

    /// <summary>
    /// <para>Creates AHSV unit values from a <see cref="System.Drawing.Color"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static (double Alpha, double Hue, double Saturation, double Value) ToAhsv(this System.Drawing.Color source)
    {
      var hue = source.ComputeHue(out var alpha, out var _, out var _, out var _, out var _, out var max, out var chroma);

      return (
        alpha,
        hue,
        max == 0 ? 0 : chroma / max, // Saturation
        max // Value
      );
    }

    /// <summary>
    /// <para>Creates AHWB unit values from a <see cref="System.Drawing.Color"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static (double Alpha, double Hue, double White, double Black) ToAhwb(this System.Drawing.Color source)
    {
      var hue = source.ComputeHue(out var alpha, out var _, out var _, out var _, out var min, out var max, out var _);

      return (
        alpha,
        hue,
        min, // White
        1 - max // Black
      );
    }

    /// <summary>
    /// <para>Creates grayscale ARGB unit values from a <see cref="System.Drawing.Color"/> using the specified grayscale method.</para>
    /// <para><see href="https://onlinetools.com/image/grayscale-image"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="method"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static (double Alpha, double Red, double Green, double Blue) ToArgbGrayscale(this System.Drawing.Color source, GrayscaleMethod method)
    {
      const double OneThird = 1d / 3d;

      return method switch
      {
        GrayscaleMethod.Average => ToArgbScaled(source, 1, OneThird, OneThird, OneThird),
        GrayscaleMethod.Luminosity601 => ToArgbScaled(source, 1, 0.30, 0.59, 0.11),
        GrayscaleMethod.Luminosity709 => ToArgbScaled(source, 1, 0.21, 0.72, 0.07),
        _ => throw new System.ArgumentOutOfRangeException(nameof(method))
      };
    }

    /// <summary>
    /// <para>Creates (unit) scaled ARGB unit values from a <see cref="System.Drawing.Color"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="sa"></param>
    /// <param name="sr"></param>
    /// <param name="sg"></param>
    /// <param name="sb"></param>
    /// <returns></returns>
    public static (double Alpha, double Red, double Green, double Blue) ToArgbScaled(this System.Drawing.Color source, double sa, double sr, double sg, double sb)
    {
      var (a, r, g, b) = source.ToArgbUnitIntervals();

      return (sa * a, sr * r, sg * g, sb * b);
    }

    /// <summary>
    /// <para>Creates ARGB unit values from a <see cref="System.Drawing.Color"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static (double Alpha, double Red, double Green, double Blue) ToArgbUnitIntervals(this System.Drawing.Color source)
      => (
        double.Clamp(source.A / 255d, 0, 1),
        double.Clamp(source.R / 255d, 0, 1),
        double.Clamp(source.G / 255d, 0, 1),
        double.Clamp(source.B / 255d, 0, 1)
      );

    /// <summary>
    /// <para>Creates a new grayscale <see cref="System.Drawing.Color"/> instance from another <see cref="System.Drawing.Color"/> instance.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="method"></param>
    /// <returns></returns>
    public static System.Drawing.Color ToGrayscale(this System.Drawing.Color source, GrayscaleMethod method)
    {
      var (A, R, G, B) = source.ToArgbGrayscale(method);

      return System.Drawing.Color.FromArgb(
        byte.CreateChecked(255 * A),
        byte.CreateChecked(255 * R),
        byte.CreateChecked(255 * G),
        byte.CreateChecked(255 * B)
      );
    }

    /// <summary>
    /// <para>Creates an HTML color string in the format "rgb(n,n,n)".</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string ToHtmlColorString(this System.Drawing.Color source) => $"rgb({source.R}, {source.G}, {source.B})";

    /// <summary>
    /// <para>Creates an HTML hexadecimal string in the format "#xxxxxx".</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string ToHtmlHexString(this System.Drawing.Color source) => $"#{source.R:X2}{source.G:X2}{source.B:X2}";
  }
}
