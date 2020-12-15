namespace Flux.Coloring
{
  public struct Rgb
    : System.IEquatable<Rgb>
  {
    public static readonly Rgb Empty;
    public bool IsEmpty => Equals(Empty);

    #region Properties
    private int? m_alpha;
    public int Alpha { get => m_alpha ?? 255; set => m_alpha = value >= 0 && value <= 255 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

    private int m_red;
    public int Red { get => m_red; set => m_red = value >= 0 && value <= 255 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

    private int m_green;
    public int Green { get => m_green; set => m_green = value >= 0 && value <= 255 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

    private int m_blue;
    public int Blue { get => m_blue; set => m_blue = value >= 0 && value <= 255 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    #endregion Properties

    public Rgb(int alpha, int red, int green, int blue)
    {
      m_alpha = alpha >= 0 && alpha <= 255 ? alpha : throw new System.ArgumentOutOfRangeException(nameof(alpha));
      m_red = red >= 0 && red <= 255 ? red : throw new System.ArgumentOutOfRangeException(nameof(red));
      m_green = green >= 0 && green <= 255 ? green : throw new System.ArgumentOutOfRangeException(nameof(green));
      m_blue = blue >= 0 && blue <= 255 ? blue : throw new System.ArgumentOutOfRangeException(nameof(blue));
    }

    public double GetChroma()
      => System.Math.Max(System.Math.Max(m_red, m_green), m_blue) - System.Math.Min(System.Math.Min(m_red, m_green), m_blue);
    public static double GetHue()
      => 0;

    /// <summary>Plain average of all colors.</summary>
    public Rgb ToGrayscaleAverage()
      => (m_red + m_green + m_blue) / 3 is var gray ? new Rgb(Alpha, gray, gray, gray) : throw new System.Exception();
    /// <summary>Averages the most prominent and least prominent colors.</summary>
    public Rgb ToGrayscaleLightness()
      => (System.Math.Max(System.Math.Max(m_red, m_green), m_blue) + System.Math.Min(System.Math.Min(m_red, m_green), m_blue)) / 2 is var gray ? new Rgb(Alpha, gray, gray, gray) : throw new System.Exception();
    /// <summary>Weighted average based on human perception.</summary>
    public Rgb ToGrayscaleLuminosity()
      => (int)(m_red * 0.21 + m_green * 0.72 + m_blue * 0.07) is var gray ? new Rgb(Alpha, gray, gray, gray) : throw new System.Exception();

    /// <summary>Converts an RGBA Color the HSL representation.</summary>
    /// <param name="color">The Color to convert.</param>
    /// <returns>HslColor.</returns>
    public Hsl ToHsl()
    {
      const double toDouble = 1.0 / 255;

      var r = toDouble * Red;
      var g = toDouble * Green;
      var b = toDouble * Blue;
      var max = System.Math.Max(System.Math.Max(r, g), b);
      var min = System.Math.Min(System.Math.Min(r, g), b);
      var chroma = max - min;
      double h1;

      if (chroma == 0) h1 = 0;
      else if (max == r) h1 = (((g - b) / chroma) + 6) % 6; // The % operator doesn't do proper modulo on negative numbers, so we'll add 6 before using it.
      else if (max == g) h1 = 2 + ((b - r) / chroma);
      else h1 = 4 + ((r - g) / chroma);

      var L = 0.5 * (max + min);
      var S = chroma == 0 ? 0 : chroma / (1 - System.Math.Abs((2 * L) - 1));
      var H = 60 * h1;
      var A = toDouble * Alpha;

      return new Hsl(A, H, S, L);
    }
    /// <summary>Converts an RGBA Color the HSV representation.</summary>
    /// <param name="color">Color to convert.</param>
    /// <returns>HsvColor</returns>
    public Hsv ToHsv()
    {
      const double toDouble = 1.0 / 255;

      var r = toDouble * Red;
      var g = toDouble * Green;
      var b = toDouble * Blue;
      var max = System.Math.Max(System.Math.Max(r, g), b);
      var min = System.Math.Min(System.Math.Min(r, g), b);
      var chroma = max - min;
      double h1;

      if (chroma == 0) h1 = 0;
      else if (max == r) h1 = (((g - b) / chroma) + 6) % 6; // The % operator doesn't do proper modulo on negative numbers, so we'll add 6 before using it.
      else if (max == g) h1 = 2 + ((b - r) / chroma);
      else h1 = 4 + ((r - g) / chroma);

      var V = max;
      var S = chroma == 0 ? 0 : chroma / max;
      var H = 60 * h1;
      var A = toDouble * Alpha;

      return new Hsv(A, H, S, V);
    }

    public int ToInt()
      => Alpha + 1 is var a ? (Alpha << 24) | ((byte)((Red * a) >> 8) << 16) | ((byte)((Green * a) >> 8) << 8) | (byte)((Blue * a) >> 8) : throw new System.Exception();

    /// <summary>Converts a Color value to a string representation of the value in hexadecimal.</summary>
    /// <param name="color">The Color to convert.</param>
    /// <returns>Returns a string representing the hex value.</returns>
    public string ToStringHex()
      => $"#{Alpha:X2}{Red:X2}{Green:X2}{Blue:X2}";

    /// <summary>Converts a Color value to a scRGB string representation of the value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/ScRGB"/>
    /// <param name="color">The Color to convert.</param>
    /// <returns>Returns a string representing the hex value.</returns>
    public string ToStringSc()
      => $"sc#{(Alpha / 255F)}{(Red / 255F)}{(Green / 255F)}{(Blue / 255F)}";

    /// <summary>
    /// Returns a color based on XAML color string.
    /// </summary>
    /// <param name="colorString">The color string. Any format used in XAML should work.</param>
    /// <returns>Parsed color</returns>
    public static Rgb Parse(string colorString)
    {
      if (string.IsNullOrEmpty(colorString)) throw new System.ArgumentNullException(nameof(colorString));

      if (colorString[0] == '#')
      {
        switch (colorString)
        {
          case var cs9 when cs9.Length == 9 && System.Convert.ToUInt32(cs9.Substring(1), 16) is var ci9:
            return new Rgb((byte)(ci9 >> 24), (byte)((ci9 >> 16) & 0xff), (byte)((ci9 >> 8) & 0xff), (byte)(ci9 & 0xff));
          case var cs7 when cs7.Length == 7 && System.Convert.ToUInt32(cs7.Substring(1), 16) is var ci7:
            return new Rgb(255, (byte)((ci7 >> 16) & 0xff), (byte)((ci7 >> 8) & 0xff), (byte)(ci7 & 0xff));
          case var cs5 when cs5.Length == 5 && System.Convert.ToUInt16(cs5.Substring(1), 16) is var c:
            var a5 = (byte)(c >> 12);
            var r5 = (byte)((c >> 8) & 0xf);
            var g5 = (byte)((c >> 4) & 0xf);
            var b5 = (byte)(c & 0xf);
            a5 = (byte)(a5 << 4 | a5);
            r5 = (byte)(r5 << 4 | r5);
            g5 = (byte)(g5 << 4 | g5);
            b5 = (byte)(b5 << 4 | b5);
            return new Rgb(a5, r5, g5, b5);
          case var cs4 when cs4.Length == 4 && System.Convert.ToUInt16(cs4.Substring(1), 16) is var ci4:
            var r4 = (byte)((ci4 >> 8) & 0xf);
            var g4 = (byte)((ci4 >> 4) & 0xf);
            var b4 = (byte)(ci4 & 0xf);
            r4 = (byte)(r4 << 4 | r4);
            g4 = (byte)(g4 << 4 | g4);
            b4 = (byte)(b4 << 4 | b4);
            return new Rgb(255, r4, g4, b4);
          default:
            throw new System.FormatException($"The {colorString} string passed in the colorString argument is not a recognized Color format.");
        }
      }

      if (colorString.Length > 3 && colorString.StartsWith(@"sc#", System.StringComparison.OrdinalIgnoreCase))
      {
				return (colorString.Substring(3).Split(',')) switch
				{
					var s4 when s4.Length == 4 => new Rgb((byte)(double.Parse(s4[0], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) * 255), (byte)(double.Parse(s4[1], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) * 255), (byte)(double.Parse(s4[2], System.Globalization.CultureInfo.CurrentCulture) * 255), (byte)(double.Parse(s4[3], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) * 255)),
					var s3 when s3.Length == 3 => new Rgb(255, (byte)(double.Parse(s3[0], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) * 255), (byte)(double.Parse(s3[1], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) * 255), (byte)(double.Parse(s3[2], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) * 255)),
					_ => throw new System.FormatException($"The {colorString} string passed in the colorString argument is not a recognized Color format (sc#[scA,]scR,scG,scB)."),
				};
			}

      throw new System.FormatException($"The {colorString} string passed in the colorString argument is not a recognized Color.");
    }

    // Operators
    public static bool operator ==(Rgb a, Rgb b)
      => a.Equals(b);
    public static bool operator !=(Rgb a, Rgb b)
      => !a.Equals(b);

    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Rgb other)
      => Alpha == other.Alpha && Red == other.Red && Green == other.Green && Blue == other.Blue;

    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is Rgb o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Alpha, Red, Green, Blue);
    public override string ToString()
      => $"<{Alpha}, {Red}, {Green}, {Blue}>";
  }

  public enum GrayscaleMethod
  {
    /// <summary>Plain average of all colors.</summary>
    Average,
    /// <summary>Averages the most prominent and least prominent colors.</summary>
    Lightness,
    /// <summary>Weighted average based on human perception.</summary>
    Luminosity
  }

  internal static class Color
  {
    /// <summary>Convert the specified color to a shade of gray.</summary>
    /// <seealso cref="https://www.johndcook.com/blog/2009/08/24/algorithms-convert-color-grayscale/"/>
    public static (byte alpha, byte red, byte green, byte blue) ColorToGrayscale(byte alpha, byte red, byte green, byte blue, GrayscaleMethod method)
    {
      var gray = 0;

      switch (method)
      {
        case GrayscaleMethod.Average:
          gray = (red + green + blue) / 3;
          break;
        case GrayscaleMethod.Lightness:
          gray = (System.Math.Max(System.Math.Max(red, green), blue) + System.Math.Min(System.Math.Min(red, green), blue)) / 2;
          break;
        case GrayscaleMethod.Luminosity:
          gray = (int)(red * 0.21 + green * 0.72 + blue * 0.07);
          break;
      }

      return (alpha, (byte)gray, (byte)gray, (byte)gray);
    }

    /// <summary>Returns a Color struct based on HSI model.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/HSL_and_HSV#From_HSI"/>
    /// <param name="hue">0..360 range hue</param>
    /// <param name="saturation">0..1 range saturation</param>
    /// <param name="intensity">0..1 range intensity</param>
    /// <param name="alpha">0..1 alpha</param>
    /// <returns>A Color object</returns>
    public static (byte alpha, byte red, byte green, byte blue) HsiToRgb(double hue, double saturation, double intensity, double alpha = 1.0)
    {
      if (hue < 0 || hue > 360) { throw new System.ArgumentOutOfRangeException(nameof(hue)); }
      if (saturation < 0 || saturation > 1) { throw new System.ArgumentOutOfRangeException(nameof(saturation)); }
      if (intensity < 0 || intensity > 1) { throw new System.ArgumentOutOfRangeException(nameof(intensity)); }
      if (alpha < 0 || alpha > 1) { throw new System.ArgumentOutOfRangeException(nameof(alpha)); }

      double hue1 = hue / 60;
      double z = 1 - System.Math.Abs((hue1 % 2) - 1);
      double chroma = (3 * intensity * saturation) / (1 + z);
      double x = chroma * z;

      double m = intensity * (1 - saturation);
      double r1 = m, g1 = m, b1 = m;

      switch (hue1)
      {
        case var v1 when v1 < 1:
          r1 += chroma;
          g1 += x;
          break;
        case var v2 when v2 < 2:
          r1 += x;
          g1 += chroma;
          break;
        case var v3 when v3 < 3:
          g1 += chroma;
          b1 += x;
          break;
        case var v4 when v4 < 4:
          g1 += x;
          b1 += chroma;
          break;
        case var v5 when v5 < 5:
          r1 += x;
          b1 += chroma;
          break;
        default: // h1 <= 6 //
          r1 += chroma;
          b1 += x;
          break;
      }

      return ((byte)(255 * alpha), (byte)(255 * r1), (byte)(255 * g1), (byte)(255 * b1));
    }

    /// <summary>Returns a Color struct based on HSL model.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/HSL_and_HSV#From_HSL"/>
    /// <param name="hue">0..360 range hue</param>
    /// <param name="saturation">0..1 range saturation</param>
    /// <param name="lightness">0..1 range lightness</param>
    /// <param name="alpha">0..1 alpha</param>
    /// <returns>A Color object</returns>
    public static (byte alpha, byte red, byte green, byte blue) HslToRgb(double hue, double saturation, double lightness, double alpha = 1.0)
    {
      if (hue < 0 || hue > 360) { throw new System.ArgumentOutOfRangeException(nameof(hue)); }
      if (saturation < 0 || saturation > 1) { throw new System.ArgumentOutOfRangeException(nameof(saturation)); }
      if (lightness < 0 || lightness > 1) { throw new System.ArgumentOutOfRangeException(nameof(lightness)); }
      if (alpha < 0 || alpha > 1) { throw new System.ArgumentOutOfRangeException(nameof(alpha)); }

      double chroma = (1 - System.Math.Abs((2 * lightness) - 1)) * saturation;
      double hue1 = hue / 60;
      double x = chroma * (1 - System.Math.Abs((hue1 % 2) - 1));

      double m = lightness - (0.5 * chroma);
      double r1 = m, g1 = m, b1 = m;

      switch (hue1)
      {
        case var v1 when v1 < 1:
          r1 += chroma;
          g1 += x;
          break;
        case var v2 when v2 < 2:
          r1 += x;
          g1 += chroma;
          break;
        case var v3 when v3 < 3:
          g1 += chroma;
          b1 += x;
          break;
        case var v4 when v4 < 4:
          g1 += x;
          b1 += chroma;
          break;
        case var v5 when v5 < 5:
          r1 += x;
          b1 += chroma;
          break;
        default: // h1 <= 6 //
          r1 += chroma;
          b1 += x;
          break;
      }

      return ((byte)(255 * alpha), (byte)(255 * r1), (byte)(255 * g1), (byte)(255 * b1));
    }

    /// <summary>Returns a Color struct based on HSV model.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/HSL_and_HSV#From_HSV"/>
    /// <param name="hue">0..360 range hue</param>
    /// <param name="saturation">0..1 range saturation</param>
    /// <param name="value">0..1 range value</param>
    /// <param name="alpha">0..1 alpha</param>
    /// <returns>A Color object</returns>
    public static (byte alpha, byte red, byte green, byte blue) HsvToRgb(double hue, double saturation, double value, double alpha = 1.0)
    {
      if (hue < 0 || hue > 360) { throw new System.ArgumentOutOfRangeException(nameof(hue)); }
      if (saturation < 0 || saturation > 1) { throw new System.ArgumentOutOfRangeException(nameof(saturation)); }
      if (value < 0 || value > 1) { throw new System.ArgumentOutOfRangeException(nameof(value)); }
      if (alpha < 0 || alpha > 1) { throw new System.ArgumentOutOfRangeException(nameof(alpha)); }

      double chroma = value * saturation;
      double h1 = hue / 60;
      double x = chroma * (1 - System.Math.Abs((h1 % 2) - 1));

      double m = value - chroma;
      double r1 = m, g1 = m, b1 = m;

      switch (h1)
      {
        case var v1 when v1 < 1:
          r1 += chroma;
          g1 += x;
          break;
        case var v2 when v2 < 2:
          r1 += x;
          g1 += chroma;
          break;
        case var v3 when v3 < 3:
          g1 += chroma;
          b1 += x;
          break;
        case var v4 when v4 < 4:
          g1 += x;
          b1 += chroma;
          break;
        case var v5 when v5 < 5:
          r1 += x;
          b1 += chroma;
          break;
        default: // h1 <= 6 //
          r1 += chroma;
          b1 += x;
          break;
      }

      return ((byte)(255 * alpha), (byte)(255 * r1), (byte)(255 * g1), (byte)(255 * b1));
    }

    /// <summary>
    /// Returns a color based on XAML color string.
    /// </summary>
    /// <param name="colorString">The color string. Any format used in XAML should work.</param>
    /// <returns>Parsed color</returns>
    public static (double red, double green, double blue, double alpha) StringToRgb(string colorString)
    {
      if (string.IsNullOrEmpty(colorString)) throw new System.ArgumentNullException(nameof(colorString));

      if (colorString[0] == '#')
      {
        switch (colorString)
        {
          case var cs9 when cs9.Length == 9 && System.Convert.ToUInt32(cs9.Substring(1), 16) is var ci9:
            return ((byte)(ci9 >> 24), (byte)((ci9 >> 16) & 0xff), (byte)((ci9 >> 8) & 0xff), (byte)(ci9 & 0xff));
          case var cs7 when cs7.Length == 7 && System.Convert.ToUInt32(cs7.Substring(1), 16) is var ci7:
            return (255, (byte)((ci7 >> 16) & 0xff), (byte)((ci7 >> 8) & 0xff), (byte)(ci7 & 0xff));
          case var cs5 when cs5.Length == 5 && System.Convert.ToUInt16(cs5.Substring(1), 16) is var c:
            var a5 = (byte)(c >> 12);
            var r5 = (byte)((c >> 8) & 0xf);
            var g5 = (byte)((c >> 4) & 0xf);
            var b5 = (byte)(c & 0xf);
            a5 = (byte)(a5 << 4 | a5);
            r5 = (byte)(r5 << 4 | r5);
            g5 = (byte)(g5 << 4 | g5);
            b5 = (byte)(b5 << 4 | b5);
            return (a5, r5, g5, b5);
          case var cs4 when cs4.Length == 4 && System.Convert.ToUInt16(cs4.Substring(1), 16) is var ci4:
            var r4 = (byte)((ci4 >> 8) & 0xf);
            var g4 = (byte)((ci4 >> 4) & 0xf);
            var b4 = (byte)(ci4 & 0xf);
            r4 = (byte)(r4 << 4 | r4);
            g4 = (byte)(g4 << 4 | g4);
            b4 = (byte)(b4 << 4 | b4);
            return (255, r4, g4, b4);
          default:
            throw new System.FormatException($"The {colorString} string passed in the colorString argument is not a recognized Color format.");
        }
      }

      if (colorString.Length > 3 && colorString.StartsWith(@"sc#", System.StringComparison.OrdinalIgnoreCase))
      {
				return (colorString.Substring(3).Split(',')) switch
				{
					var s4 when s4.Length == 4 => ((byte)(double.Parse(s4[0], System.Globalization.CultureInfo.CurrentCulture) * 255), (byte)(double.Parse(s4[1], System.Globalization.CultureInfo.CurrentCulture) * 255), (byte)(double.Parse(s4[2], System.Globalization.CultureInfo.CurrentCulture) * 255), (byte)(double.Parse(s4[3], System.Globalization.CultureInfo.CurrentCulture) * 255)),
					var s3 when s3.Length == 3 => (255, (byte)(double.Parse(s3[0], System.Globalization.CultureInfo.CurrentCulture) * 255), (byte)(double.Parse(s3[1], System.Globalization.CultureInfo.CurrentCulture) * 255), (byte)(double.Parse(s3[2], System.Globalization.CultureInfo.CurrentCulture) * 255)),
					_ => throw new System.FormatException($"The {colorString} string passed in the colorString argument is not a recognized Color format (sc#[scA,]scR,scG,scB)."),
				};
			}

      throw new System.FormatException($"The {colorString} string passed in the colorString argument is not a recognized Color.");
    }

    /// <summary>Converts a Color value to a string representation of the value in hexadecimal.</summary>
    /// <param name="color">The Color to convert.</param>
    /// <returns>Returns a string representing the hex value.</returns>
    public static string RgbToHex(byte alpha, byte red, byte green, byte blue) => $"#{alpha:X2}{red:X2}{green:X2}{blue:X2}";

    /// <summary>Converts a Color value to a scRGB string representation of the value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/ScRGB"/>
    /// <param name="color">The Color to convert.</param>
    /// <returns>Returns a string representing the hex value.</returns>
    public static string RgbToSc(byte alpha, byte red, byte green, byte blue) => $"sc#{(alpha / 255F)}{(red / 255F)}{(green / 255F)}{(blue / 255F)}";

    /// <summary>Converts an RGBA Color the HSL representation.</summary>
    /// <param name="color">The Color to convert.</param>
    /// <returns>HslColor.</returns>
    public static (double alpha, double hue, double saturation, double lightness) RgbToHsl(byte red, byte green, byte blue, byte alpha)
    {
      const double toDouble = 1.0 / 255;

      var r = toDouble * red;
      var g = toDouble * green;
      var b = toDouble * blue;
      var max = System.Math.Max(System.Math.Max(r, g), b);
      var min = System.Math.Min(System.Math.Min(r, g), b);
      var chroma = max - min;
      double h1;

      if (chroma == 0) h1 = 0;
      else if (max == r) h1 = (((g - b) / chroma) + 6) % 6; // The % operator doesn't do proper modulo on negative numbers, so we'll add 6 before using it.
      else if (max == g) h1 = 2 + ((b - r) / chroma);
      else h1 = 4 + ((r - g) / chroma);

      var L = 0.5 * (max + min);
      var S = chroma == 0 ? 0 : chroma / (1 - System.Math.Abs((2 * L) - 1));
      var H = 60 * h1;
      var A = toDouble * alpha;

      return (A, H, S, L);
    }

    /// <summary>Converts an RGBA Color the HSV representation.</summary>
    /// <param name="color">Color to convert.</param>
    /// <returns>HsvColor</returns>
    public static (double alpha, double hue, double saturation, double value) RgbToHsv(byte alpha, byte red, byte green, byte blue)
    {
      const double toDouble = 1.0 / 255;

      var r = toDouble * red;
      var g = toDouble * green;
      var b = toDouble * blue;
      var max = System.Math.Max(System.Math.Max(r, g), b);
      var min = System.Math.Min(System.Math.Min(r, g), b);
      var chroma = max - min;
      double h1;

      if (chroma == 0)
      {
        h1 = 0;
      }
      else if (max == r) // The % operator doesn't do proper modulo on negative numbers, so we'll add 6 before using it.
      {
        h1 = (((g - b) / chroma) + 6) % 6;
      }
      else if (max == g)
      {
        h1 = 2 + ((b - r) / chroma);
      }
      else
      {
        h1 = 4 + ((r - g) / chroma);
      }

      var V = max;
      var S = chroma == 0 ? 0 : chroma / max;
      var H = 60 * h1;
      var A = toDouble * alpha;

      return (A, H, S, V);
    }

    /// <summary>Returns the color value as a premultiplied Int32 - 4 byte ARGB structure.</summary>
    /// <param name="color">the Color to convert</param>
    /// <returns>Returns a int representing the color.</returns>
    public static int RgbToInt(byte alpha, byte red, byte green, byte blue) => alpha + 1 is var a ? (alpha << 24) | ((byte)((red * a) >> 8) << 16) | ((byte)((green * a) >> 8) << 8) | (byte)((blue * a) >> 8) : throw new System.Exception();
  }
}
