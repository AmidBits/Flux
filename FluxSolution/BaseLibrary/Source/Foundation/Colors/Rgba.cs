namespace Flux.Colors
{
  /// <summary>Rgba is the same as Rgb with the addition of an alpha channel.</summary>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
  public struct Rgba
    : System.IEquatable<Rgba>
  {
    public static readonly Rgba Empty;
    public bool IsEmpty => Equals(Empty);

    [System.Runtime.InteropServices.FieldOffset(0)] private Rgb m_rgb;
    [System.Runtime.InteropServices.FieldOffset(3)] private byte m_alpha;

    public Rgb RGB { get => m_rgb; set => m_rgb = value; }
    public int Alpha { get => m_alpha; set => m_alpha = value >= 0 && value <= 255 ? (byte)value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

    public Rgba(Rgb rgb, int alpha)
    {
      m_rgb = rgb;
      m_alpha = alpha >= 0 && alpha <= 255 ? (byte)alpha : throw new System.ArgumentOutOfRangeException(nameof(alpha));
    }
    public Rgba(int red, int green, int blue, int alpha)
      : this(new Rgb(red, green, blue), alpha)
    { }

    public static Rgba Random(System.Random rng)
    {
      if (rng is null) throw new System.ArgumentNullException(nameof(rng));

      var bytes = rng.GetRandomBytes(4);

      return new Rgba(bytes[0], bytes[1], bytes[2], bytes[3]);
    }
    public static Rgba Random()
      => Random(Flux.Random.NumberGenerator.Crypto);

    /// <summary>Converts the RGB color to grayscale using the specified method.</summary>
    public Rgba ToGrayscale(GrayscaleMethod method)
      => new Rgba(m_rgb.ToGrayscale(method), Alpha);

    /// <summary>Creates a CMYKA color corresponding to the RGBA instance.</summary>
    public Cmyka ToCmyka()
      => new Cmyka(RGB.ToCmyk(), Alpha / 255.0);
    /// <summary>Creates an HSIA color corresponding to the RGBA instance.</summary>
    public Hsia ToHsia()
      => new Hsia(RGB.ToHsi(), Alpha / 255.0);
    /// <summary>Creates an HSLA color corresponding to the RGBA instance.</summary>
    public Hsla ToHsla()
      => new Hsla(RGB.ToHsl(), Alpha / 255.0);
    /// <summary>Creates an HSV color corresponding to the RGBA instance.</summary>
    public Hsva ToHsva()
      => new Hsva(RGB.ToHsv(), Alpha / 255.0);
    //https://stackoverflow.com/questions/29832317/converting-hsb-to-rgb
    // http://alvyray.com/Papers/CG/HWB_JGTv208.pdf#:~:text=HWB%20To%20and%20From%20RGB%20The%20full%20transforms,min%28%20R%20%2C%20G%20%2C%20B%20%29.%20
    //public Hwb ToHwb()
    //{
    //  var red = m_red / 255.0;
    //  var green = m_green / 255.0;
    //  var blue = m_blue / 255.0;

    //  var max = System.Math.Max(red, System.Math.Max(green, blue));
    //  var min = System.Math.Min(red, System.Math.Min(green, blue));

    //  var b = 1 - max;

    //  if (max == min)
    //    return new Hwb(-1, min, b);

    //  var f = red == min ? green - blue : (green == min ? blue - red : red - green);
    //  var i = red == min ? 3 : (green == min ? 5 : 1);

    //  return new Hwb(i - f / (max - min), min, b, Alpha / 255.0);
    //}

    public int ToInt()
    {
      var a = Alpha + 1;

      return (Alpha << 24) | ((byte)((RGB.Red * a) >> 8) << 16) | ((byte)((RGB.Green * a) >> 8) << 8) | (byte)((RGB.Blue * a) >> 8);
    }

    /// <summary>Converts a Color value to a string representation of the value in hexadecimal.</summary>
    /// <param name="color">The Color to convert.</param>
    /// <returns>Returns a string representing the hex value.</returns>
    public string ToStringHtmlHex()
      => $"#{Alpha:X2}{RGB.Red:X2}{RGB.Green:X2}{RGB.Blue:X2}";
    public string ToStringHtmlRgba()
      => $"rgba({RGB.Red}, {RGB.Green}, {RGB.Blue}, {Alpha / 255.0})";
    /// <summary>Converts a Color value to a scRGB string representation of the value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/ScRGB"/>
    /// <param name="color">The Color to convert.</param>
    /// <returns>Returns a string representing the hex value.</returns>
    public string ToStringHtmlSc()
      => $"sc#{Alpha / 255F}{RGB.Red / 255F}{RGB.Green / 255F}{RGB.Blue / 255F}";

    /// <summary>
    /// Returns a color based on XAML color string.
    /// </summary>
    /// <param name="colorString">The color string. Any format used in XAML should work.</param>
    /// <returns>Parsed color</returns>
    public static Rgba Parse(string colorString)
    {
      if (string.IsNullOrEmpty(colorString)) throw new System.ArgumentNullException(nameof(colorString));

      if (colorString[0] == '#')
      {
        switch (colorString)
        {
          case var cs9 when cs9.Length == 9 && System.Convert.ToUInt32(cs9.Substring(1), 16) is var ci9:
            return new Rgba((byte)((ci9 >> 16) & 0xff), (byte)((ci9 >> 8) & 0xff), (byte)(ci9 & 0xff), (byte)(ci9 >> 24));
          case var cs7 when cs7.Length == 7 && System.Convert.ToUInt32(cs7.Substring(1), 16) is var ci7:
            return new Rgba((byte)((ci7 >> 16) & 0xff), (byte)((ci7 >> 8) & 0xff), (byte)(ci7 & 0xff), 255);
          case var cs5 when cs5.Length == 5 && System.Convert.ToUInt16(cs5.Substring(1), 16) is var c:
            var a5 = (byte)(c >> 12);
            var r5 = (byte)((c >> 8) & 0xf);
            var g5 = (byte)((c >> 4) & 0xf);
            var b5 = (byte)(c & 0xf);
            a5 = (byte)(a5 << 4 | a5);
            r5 = (byte)(r5 << 4 | r5);
            g5 = (byte)(g5 << 4 | g5);
            b5 = (byte)(b5 << 4 | b5);
            return new Rgba(r5, g5, b5, a5);
          case var cs4 when cs4.Length == 4 && System.Convert.ToUInt16(cs4.Substring(1), 16) is var ci4:
            var r4 = (byte)((ci4 >> 8) & 0xf);
            var g4 = (byte)((ci4 >> 4) & 0xf);
            var b4 = (byte)(ci4 & 0xf);
            r4 = (byte)(r4 << 4 | r4);
            g4 = (byte)(g4 << 4 | g4);
            b4 = (byte)(b4 << 4 | b4);
            return new Rgba(r4, g4, b4, 255);
          default:
            throw new System.FormatException($"The {colorString} string passed in the colorString argument is not a recognized Color format.");
        }
      }

      if (colorString.Length > 3 && colorString.StartsWith(@"sc#", System.StringComparison.OrdinalIgnoreCase))
      {
        return (colorString.Substring(3).Split(',')) switch
        {
          var s4 when s4.Length == 4 => new Rgba((byte)(double.Parse(s4[1], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) * 255), (byte)(double.Parse(s4[2], System.Globalization.CultureInfo.CurrentCulture) * 255), (byte)(double.Parse(s4[3], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) * 255), (byte)(double.Parse(s4[0], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) * 255)),
          var s3 when s3.Length == 3 => new Rgba((byte)(double.Parse(s3[0], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) * 255), (byte)(double.Parse(s3[1], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) * 255), (byte)(double.Parse(s3[2], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) * 255), 255),
          _ => throw new System.FormatException($"The {colorString} string passed in the colorString argument is not a recognized Color format (sc#[scA,]scR,scG,scB)."),
        };
      }

      throw new System.FormatException($"The {colorString} string passed in the colorString argument is not a recognized Color.");
    }

    #region Overloaded operators
    public static bool operator ==(Rgba a, Rgba b)
      => a.Equals(b);
    public static bool operator !=(Rgba a, Rgba b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Rgba other)
      => RGB == other.RGB && Alpha == other.Alpha;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Rgba o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(RGB, Alpha);
    public override string ToString()
      => $"<{GetType().Name}: {RGB.Red}, {RGB.Green}, {RGB.Blue}, {Alpha}>";
    #endregion Object overrides
  }

  //internal static class Color
  //{
  //  /// <summary>Convert the specified color to a shade of gray.</summary>
  //  /// <seealso cref="https://www.johndcook.com/blog/2009/08/24/algorithms-convert-color-grayscale/"/>
  //  //public static (byte alpha, byte red, byte green, byte blue) ColorToGrayscale(byte alpha, byte red, byte green, byte blue, GrayscaleMethod method)
  //  //{
  //  //  var gray = 0;

  //  //  switch (method)
  //  //  {
  //  //    case GrayscaleMethod.Average:
  //  //      gray = (red + green + blue) / 3;
  //  //      break;
  //  //    case GrayscaleMethod.Lightness:
  //  //      gray = (System.Math.Max(System.Math.Max(red, green), blue) + System.Math.Min(System.Math.Min(red, green), blue)) / 2;
  //  //      break;
  //  //    case GrayscaleMethod.Luminosity:
  //  //      gray = (int)(red * 0.21 + green * 0.72 + blue * 0.07);
  //  //      break;
  //  //  }

  //  //  return (alpha, (byte)gray, (byte)gray, (byte)gray);
  //  //}

  //  /// <summary>Returns a Color struct based on HSI model.</summary>
  //  /// <see cref="https://en.wikipedia.org/wiki/HSL_and_HSV#From_HSI"/>
  //  /// <param name="hue">0..360 range hue</param>
  //  /// <param name="saturation">0..1 range saturation</param>
  //  /// <param name="intensity">0..1 range intensity</param>
  //  /// <param name="alpha">0..1 alpha</param>
  //  /// <returns>A Color object</returns>
  //  //public static (byte alpha, byte red, byte green, byte blue) HsiToRgb(double hue, double saturation, double intensity, double alpha = 1.0)
  //  //{
  //  //  if (hue < 0 || hue > 360) { throw new System.ArgumentOutOfRangeException(nameof(hue)); }
  //  //  if (saturation < 0 || saturation > 1) { throw new System.ArgumentOutOfRangeException(nameof(saturation)); }
  //  //  if (intensity < 0 || intensity > 1) { throw new System.ArgumentOutOfRangeException(nameof(intensity)); }
  //  //  if (alpha < 0 || alpha > 1) { throw new System.ArgumentOutOfRangeException(nameof(alpha)); }

  //  //  double hue1 = hue / 60;
  //  //  double z = 1 - System.Math.Abs((hue1 % 2) - 1);
  //  //  double chroma = (3 * intensity * saturation) / (1 + z);
  //  //  double x = chroma * z;

  //  //  double m = intensity * (1 - saturation);
  //  //  double r1 = m, g1 = m, b1 = m;

  //  //  switch (hue1)
  //  //  {
  //  //    case var v1 when v1 < 1:
  //  //      r1 += chroma;
  //  //      g1 += x;
  //  //      break;
  //  //    case var v2 when v2 < 2:
  //  //      r1 += x;
  //  //      g1 += chroma;
  //  //      break;
  //  //    case var v3 when v3 < 3:
  //  //      g1 += chroma;
  //  //      b1 += x;
  //  //      break;
  //  //    case var v4 when v4 < 4:
  //  //      g1 += x;
  //  //      b1 += chroma;
  //  //      break;
  //  //    case var v5 when v5 < 5:
  //  //      r1 += x;
  //  //      b1 += chroma;
  //  //      break;
  //  //    default: // h1 <= 6 //
  //  //      r1 += chroma;
  //  //      b1 += x;
  //  //      break;
  //  //  }

  //  //  return ((byte)(255 * alpha), (byte)(255 * r1), (byte)(255 * g1), (byte)(255 * b1));
  //  //}

  //  /// <summary>Returns a Color struct based on HSL model.</summary>
  //  /// <see cref="https://en.wikipedia.org/wiki/HSL_and_HSV#From_HSL"/>
  //  /// <param name="hue">0..360 range hue</param>
  //  /// <param name="saturation">0..1 range saturation</param>
  //  /// <param name="lightness">0..1 range lightness</param>
  //  /// <param name="alpha">0..1 alpha</param>
  //  /// <returns>A Color object</returns>
  //  //public static (byte alpha, byte red, byte green, byte blue) HslToRgb(double hue, double saturation, double lightness, double alpha = 1.0)
  //  //{
  //  //  if (hue < 0 || hue > 360) { throw new System.ArgumentOutOfRangeException(nameof(hue)); }
  //  //  if (saturation < 0 || saturation > 1) { throw new System.ArgumentOutOfRangeException(nameof(saturation)); }
  //  //  if (lightness < 0 || lightness > 1) { throw new System.ArgumentOutOfRangeException(nameof(lightness)); }
  //  //  if (alpha < 0 || alpha > 1) { throw new System.ArgumentOutOfRangeException(nameof(alpha)); }

  //  //  double chroma = (1 - System.Math.Abs((2 * lightness) - 1)) * saturation;
  //  //  double hue1 = hue / 60;
  //  //  double x = chroma * (1 - System.Math.Abs((hue1 % 2) - 1));

  //  //  double m = lightness - (0.5 * chroma);
  //  //  double r1 = m, g1 = m, b1 = m;

  //  //  switch (hue1)
  //  //  {
  //  //    case var v1 when v1 < 1:
  //  //      r1 += chroma;
  //  //      g1 += x;
  //  //      break;
  //  //    case var v2 when v2 < 2:
  //  //      r1 += x;
  //  //      g1 += chroma;
  //  //      break;
  //  //    case var v3 when v3 < 3:
  //  //      g1 += chroma;
  //  //      b1 += x;
  //  //      break;
  //  //    case var v4 when v4 < 4:
  //  //      g1 += x;
  //  //      b1 += chroma;
  //  //      break;
  //  //    case var v5 when v5 < 5:
  //  //      r1 += x;
  //  //      b1 += chroma;
  //  //      break;
  //  //    default: // h1 <= 6 //
  //  //      r1 += chroma;
  //  //      b1 += x;
  //  //      break;
  //  //  }

  //  //  return ((byte)(255 * alpha), (byte)(255 * r1), (byte)(255 * g1), (byte)(255 * b1));
  //  //}

  //  /// <summary>Returns a Color struct based on HSV model.</summary>
  //  /// <see cref="https://en.wikipedia.org/wiki/HSL_and_HSV#From_HSV"/>
  //  /// <param name="hue">0..360 range hue</param>
  //  /// <param name="saturation">0..1 range saturation</param>
  //  /// <param name="value">0..1 range value</param>
  //  /// <param name="alpha">0..1 alpha</param>
  //  /// <returns>A Color object</returns>
  //  //public static (byte alpha, byte red, byte green, byte blue) HsvToRgb(double hue, double saturation, double value, double alpha = 1.0)
  //  //{
  //  //  if (hue < 0 || hue > 360) { throw new System.ArgumentOutOfRangeException(nameof(hue)); }
  //  //  if (saturation < 0 || saturation > 1) { throw new System.ArgumentOutOfRangeException(nameof(saturation)); }
  //  //  if (value < 0 || value > 1) { throw new System.ArgumentOutOfRangeException(nameof(value)); }
  //  //  if (alpha < 0 || alpha > 1) { throw new System.ArgumentOutOfRangeException(nameof(alpha)); }

  //  //  double chroma = value * saturation;
  //  //  double h1 = hue / 60;
  //  //  double x = chroma * (1 - System.Math.Abs((h1 % 2) - 1));

  //  //  double m = value - chroma;
  //  //  double r1 = m, g1 = m, b1 = m;

  //  //  switch (h1)
  //  //  {
  //  //    case var v1 when v1 < 1:
  //  //      r1 += chroma;
  //  //      g1 += x;
  //  //      break;
  //  //    case var v2 when v2 < 2:
  //  //      r1 += x;
  //  //      g1 += chroma;
  //  //      break;
  //  //    case var v3 when v3 < 3:
  //  //      g1 += chroma;
  //  //      b1 += x;
  //  //      break;
  //  //    case var v4 when v4 < 4:
  //  //      g1 += x;
  //  //      b1 += chroma;
  //  //      break;
  //  //    case var v5 when v5 < 5:
  //  //      r1 += x;
  //  //      b1 += chroma;
  //  //      break;
  //  //    default: // h1 <= 6 //
  //  //      r1 += chroma;
  //  //      b1 += x;
  //  //      break;
  //  //  }

  //  //  return ((byte)(255 * alpha), (byte)(255 * r1), (byte)(255 * g1), (byte)(255 * b1));
  //  //}

  //  /// <summary>
  //  /// Returns a color based on XAML color string.
  //  /// </summary>
  //  /// <param name="colorString">The color string. Any format used in XAML should work.</param>
  //  /// <returns>Parsed color</returns>
  //  //public static (double red, double green, double blue, double alpha) StringToRgb(string colorString)
  //  //{
  //  //  if (string.IsNullOrEmpty(colorString)) throw new System.ArgumentNullException(nameof(colorString));

  //  //  if (colorString[0] == '#')
  //  //  {
  //  //    switch (colorString)
  //  //    {
  //  //      case var cs9 when cs9.Length == 9 && System.Convert.ToUInt32(cs9.Substring(1), 16) is var ci9:
  //  //        return ((byte)(ci9 >> 24), (byte)((ci9 >> 16) & 0xff), (byte)((ci9 >> 8) & 0xff), (byte)(ci9 & 0xff));
  //  //      case var cs7 when cs7.Length == 7 && System.Convert.ToUInt32(cs7.Substring(1), 16) is var ci7:
  //  //        return (255, (byte)((ci7 >> 16) & 0xff), (byte)((ci7 >> 8) & 0xff), (byte)(ci7 & 0xff));
  //  //      case var cs5 when cs5.Length == 5 && System.Convert.ToUInt16(cs5.Substring(1), 16) is var c:
  //  //        var a5 = (byte)(c >> 12);
  //  //        var r5 = (byte)((c >> 8) & 0xf);
  //  //        var g5 = (byte)((c >> 4) & 0xf);
  //  //        var b5 = (byte)(c & 0xf);
  //  //        a5 = (byte)(a5 << 4 | a5);
  //  //        r5 = (byte)(r5 << 4 | r5);
  //  //        g5 = (byte)(g5 << 4 | g5);
  //  //        b5 = (byte)(b5 << 4 | b5);
  //  //        return (a5, r5, g5, b5);
  //  //      case var cs4 when cs4.Length == 4 && System.Convert.ToUInt16(cs4.Substring(1), 16) is var ci4:
  //  //        var r4 = (byte)((ci4 >> 8) & 0xf);
  //  //        var g4 = (byte)((ci4 >> 4) & 0xf);
  //  //        var b4 = (byte)(ci4 & 0xf);
  //  //        r4 = (byte)(r4 << 4 | r4);
  //  //        g4 = (byte)(g4 << 4 | g4);
  //  //        b4 = (byte)(b4 << 4 | b4);
  //  //        return (255, r4, g4, b4);
  //  //      default:
  //  //        throw new System.FormatException($"The {colorString} string passed in the colorString argument is not a recognized Color format.");
  //  //    }
  //  //  }

  //  //  if (colorString.Length > 3 && colorString.StartsWith(@"sc#", System.StringComparison.OrdinalIgnoreCase))
  //  //  {
  //  //    return (colorString.Substring(3).Split(',')) switch
  //  //    {
  //  //      var s4 when s4.Length == 4 => ((byte)(double.Parse(s4[0], System.Globalization.CultureInfo.CurrentCulture) * 255), (byte)(double.Parse(s4[1], System.Globalization.CultureInfo.CurrentCulture) * 255), (byte)(double.Parse(s4[2], System.Globalization.CultureInfo.CurrentCulture) * 255), (byte)(double.Parse(s4[3], System.Globalization.CultureInfo.CurrentCulture) * 255)),
  //  //      var s3 when s3.Length == 3 => (255, (byte)(double.Parse(s3[0], System.Globalization.CultureInfo.CurrentCulture) * 255), (byte)(double.Parse(s3[1], System.Globalization.CultureInfo.CurrentCulture) * 255), (byte)(double.Parse(s3[2], System.Globalization.CultureInfo.CurrentCulture) * 255)),
  //  //      _ => throw new System.FormatException($"The {colorString} string passed in the colorString argument is not a recognized Color format (sc#[scA,]scR,scG,scB)."),
  //  //    };
  //  //  }

  //  //  throw new System.FormatException($"The {colorString} string passed in the colorString argument is not a recognized Color.");
  //  //}

  //  /// <summary>Converts a Color value to a string representation of the value in hexadecimal.</summary>
  //  /// <param name="color">The Color to convert.</param>
  //  /// <returns>Returns a string representing the hex value.</returns>
  //  //public static string RgbToHex(byte alpha, byte red, byte green, byte blue) => $"#{alpha:X2}{red:X2}{green:X2}{blue:X2}";

  //  /// <summary>Converts a Color value to a scRGB string representation of the value.</summary>
  //  /// <see cref="https://en.wikipedia.org/wiki/ScRGB"/>
  //  /// <param name="color">The Color to convert.</param>
  //  /// <returns>Returns a string representing the hex value.</returns>
  //  //public static string RgbToSc(byte alpha, byte red, byte green, byte blue) => $"sc#{(alpha / 255F)}{(red / 255F)}{(green / 255F)}{(blue / 255F)}";

  //  /// <summary>Converts an RGBA Color the HSL representation.</summary>
  //  /// <param name="color">The Color to convert.</param>
  //  /// <returns>HslColor.</returns>
  //  //public static (double alpha, double hue, double saturation, double lightness) RgbToHsl(byte red, byte green, byte blue, byte alpha)
  //  //{
  //  //  const double toDouble = 1.0 / 255;

  //  //  var r = toDouble * red;
  //  //  var g = toDouble * green;
  //  //  var b = toDouble * blue;
  //  //  var max = System.Math.Max(System.Math.Max(r, g), b);
  //  //  var min = System.Math.Min(System.Math.Min(r, g), b);
  //  //  var chroma = max - min;
  //  //  double h1;

  //  //  if (chroma == 0) h1 = 0;
  //  //  else if (max == r) h1 = (((g - b) / chroma) + 6) % 6; // The % operator doesn't do proper modulo on negative numbers, so we'll add 6 before using it.
  //  //  else if (max == g) h1 = 2 + ((b - r) / chroma);
  //  //  else h1 = 4 + ((r - g) / chroma);

  //  //  var L = 0.5 * (max + min);
  //  //  var S = chroma == 0 ? 0 : chroma / (1 - System.Math.Abs((2 * L) - 1));
  //  //  var H = 60 * h1;
  //  //  var A = toDouble * alpha;

  //  //  return (A, H, S, L);
  //  //}

  //  /// <summary>Converts an RGBA Color the HSV representation.</summary>
  //  /// <param name="color">Color to convert.</param>
  //  /// <returns>HsvColor</returns>
  //  //public static (double alpha, double hue, double saturation, double value) RgbToHsv(byte alpha, byte red, byte green, byte blue)
  //  //{
  //  //  const double toDouble = 1.0 / 255;

  //  //  var r = toDouble * red;
  //  //  var g = toDouble * green;
  //  //  var b = toDouble * blue;
  //  //  var max = System.Math.Max(System.Math.Max(r, g), b);
  //  //  var min = System.Math.Min(System.Math.Min(r, g), b);
  //  //  var chroma = max - min;
  //  //  double h1;

  //  //  if (chroma == 0)
  //  //  {
  //  //    h1 = 0;
  //  //  }
  //  //  else if (max == r) // The % operator doesn't do proper modulo on negative numbers, so we'll add 6 before using it.
  //  //  {
  //  //    h1 = (((g - b) / chroma) + 6) % 6;
  //  //  }
  //  //  else if (max == g)
  //  //  {
  //  //    h1 = 2 + ((b - r) / chroma);
  //  //  }
  //  //  else
  //  //  {
  //  //    h1 = 4 + ((r - g) / chroma);
  //  //  }

  //  //  var V = max;
  //  //  var S = chroma == 0 ? 0 : chroma / max;
  //  //  var H = 60 * h1;
  //  //  var A = toDouble * alpha;

  //  //  return (A, H, S, V);
  //  //}

  //  /// <summary>Returns the color value as a premultiplied Int32 - 4 byte ARGB structure.</summary>
  //  /// <param name="color">the Color to convert</param>
  //  /// <returns>Returns a int representing the color.</returns>
  //  //public static int RgbToInt(byte alpha, byte red, byte green, byte blue) => alpha + 1 is var a ? (alpha << 24) | ((byte)((red * a) >> 8) << 16) | ((byte)((green * a) >> 8) << 8) | (byte)((blue * a) >> 8) : throw new System.Exception();
  //}
}
