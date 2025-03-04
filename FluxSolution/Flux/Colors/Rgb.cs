namespace Flux.Colors
{
  public readonly record struct Rgb
    : IHtmlColorFormattable
  {
    private readonly byte m_blue;
    private readonly byte m_green;
    private readonly byte m_red;

    public Rgb(int red, int green, int blue)
    {
      m_red = red >= 0 && red <= 255 ? (byte)red : throw new System.ArgumentOutOfRangeException(nameof(red));
      m_green = green >= 0 && green <= 255 ? (byte)green : throw new System.ArgumentOutOfRangeException(nameof(green));
      m_blue = blue >= 0 && blue <= 255 ? (byte)blue : throw new System.ArgumentOutOfRangeException(nameof(blue));
    }
    public Rgb(int rgb) : this((byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb) { }
    public Rgb(System.ReadOnlySpan<byte> rgb) : this(rgb[0], rgb[1], rgb[2]) { }
    public Rgb(System.Random rng) : this((rng ?? System.Random.Shared).GetRandomBytes(3)) { }

    public int Red => m_red;
    public int Green => m_green;
    public int Blue => m_blue;

    /// <summary>Returns the chroma for the RGB value.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Chrominance"/>
    public double GetChroma(out double min, out double max, out double r, out double g, out double b)
    {
      r = System.Math.Clamp(m_red / 255d, 0, 1);
      g = System.Math.Clamp(m_green / 255d, 0, 1);
      b = System.Math.Clamp(m_blue / 255d, 0, 1);

      min = System.Math.Min(System.Math.Min(r, g), b);
      max = System.Math.Max(System.Math.Max(r, g), b);

      return System.Math.Clamp(max - min, 0, 1);
    }

    /// <summary>Returns the hue [0, 360] for the RGB value.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Hue"/>
    public double GetHue(out double min, out double max, out double r, out double g, out double b, out double chroma)
    {
      chroma = GetChroma(out min, out max, out r, out g, out b);

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

    /// <summary>Returns the luma for the RGB value, using the specified coefficients.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Luma_(video)"/>
    public double GetLuma(double rc, double gc, double bc)
    {
      var r = m_red / 255d;
      var g = m_green / 255d;
      var b = m_blue / 255d;

      return rc * r + gc * g + bc * b;
    }

    /// <summary>Returns the luma for the RGB value, using Rec.601 coefficients.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Rec._601"/>
    public double GetLuma601() => GetLuma(0.2989, 0.5870, 0.1140);

    /// <summary>Returns the luma for the RGB value, using Adobe/SMPTE 240M coefficients.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Adobe_RGB_color_space"/>
    public double GetLuma240() => GetLuma(0.212, 0.701, 0.087);

    /// <summary>Returns the luma for the RGB value, using Rec.709 coefficients.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Rec._709"/>
    public double GetLuma709() => GetLuma(0.2126, 0.7152, 0.0722);

    /// <summary>Returns the luma for the RGB value, using Rec.2020 coefficients.</summary>
    public double GetLuma2020() => GetLuma(0.2627, 0.6780, 0.0593);

    public double GetNormalizedChroma(out double min, out double max, out double r, out double g, out double b)
    {
      r = m_red / 255d;
      g = m_green / 255d;
      b = m_blue / 255d;

      max = System.Math.Max(System.Math.Max(r, g), b);
      min = System.Math.Min(System.Math.Min(r, g), b);

      return max - min;
    }

    /// <summary>Returns the chroma and hue [0.0, 360.0] for the RGB value.</summary>
    public void GetSecondaryChromaAndHue(out double chroma2, out double hue2)
    {
      var r = m_red / 255d;
      var g = m_green / 255d;
      var b = m_blue / 255d;

      var alpha = (2 * r - g - b) / 2;
      var beta = (System.Math.Sqrt(3) / 2) * (g - b);

      chroma2 = System.Math.Sqrt(alpha * alpha + beta * beta);
      hue2 = double.RadiansToDegrees(System.Math.Atan2(beta, alpha)).WrapAround(0, 360);
    }

    /// <summary>Converts the RGB color to grayscale using the specified method.
    /// <para><see href="https://onlinetools.com/image/grayscale-image"/></para>
    /// </summary>
    public Rgb ToGrayscale(GrayscaleMethod method)
    {
      switch (method)
      {
        case GrayscaleMethod.Average: return new(m_red / 3, m_green / 3, m_blue / 3);
        //case GrayscaleMethod.Lightness: new((System.Math.Max(System.Math.Max(m_red, m_green), m_blue) + System.Math.Min(System.Math.Min(m_red, m_green), m_blue)) / 2);
        case GrayscaleMethod.Luminosity601: return new Rgb((byte)(m_red * 0.30), (byte)(m_green * 0.59), (byte)(m_blue * 0.11));
        case GrayscaleMethod.Luminosity709: return new Rgb((byte)(m_red * 0.21), (byte)(m_green * 0.72), (byte)(m_blue * 0.07));
        default: throw new System.ArgumentOutOfRangeException(nameof(method));
      };
    }

    /// <summary>Creates a CMYK color corresponding to the RGB instance.</summary>
    public Cmyk ToCmyk()
    {
      GetNormalizedChroma(out var _, out var max, out var r, out var g, out var b);
      var k = 1 - max;
      var ki = 1 - k;
      var c = System.Math.Clamp(ki - r, 0, 1) / ki;
      var m = System.Math.Clamp(ki - g, 0, 1) / ki;
      var y = System.Math.Clamp(ki - b, 0, 1) / ki;
      return new(c, m, y, k);
    }

    /// <summary>Creates an HSI color corresponding to the RGB instance.</summary>
    public Hsi ToHsi()
    {
      var h = GetHue(out var min, out var _, out var r, out var g, out var b, out var _);
      var i = (r + g + b) / 3;
      var s = i == 0 ? 0 : 1 - (min / i);
      return new(h, s, i);
    }

    /// <summary>Creates an HSL color corresponding to the RGB instance.</summary>
    public Hsl ToHsl()
    {
      var h = GetHue(out var min, out var max, out var _, out var _, out var _, out var chroma);
      var l = 0.5 * (max + min);
      var s = l == 0 || l == 1 ? 0 : System.Math.Clamp(chroma / (1 - System.Math.Abs(2 * l - 1)), 0, 1);
      return new(h, s, l);
    }

    /// <summary>Creates an HSV color corresponding to the RGB instance.</summary>
    public Hsv ToHsv()
    {
      var h = GetHue(out _, out var max, out _, out _, out _, out var chroma);
      var s = max == 0 ? 0 : chroma / max;
      var v = max;
      return new(h, s, v);
    }

    /// <summary>Creates an HWB color corresponding to the RGB instance.</summary>
    public Hwb ToHwb()
    {
      var h = GetHue(out var min, out var max, out _, out _, out _, out _);
      var w = min;
      var b = 1 - max;
      return new(h, w, b);
    }

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

    public int ToInt() => ((Red << 16) & 0x00FF0000) | ((Green << 8) & 0x0000FF00) | (Blue & 0x000000FF);

    #region Static methods

    //public static double GetChroma(byte red, byte green, byte blue, out double r, out double g, out double b, out double min, out double max)
    //{
    //  r = System.Math.Clamp(red / 255d, 0, 1);
    //  g = System.Math.Clamp(green / 255d, 0, 1);
    //  b = System.Math.Clamp(blue / 255d, 0, 1);

    //  max = System.Math.Max(System.Math.Max(r, g), b);
    //  min = System.Math.Min(System.Math.Min(r, g), b);

    //  return System.Math.Clamp(max - min, 0, 1);
    //}
    //public static double GetHue(byte red, byte green, byte blue, byte min, byte max)
    //{
    //  if (min == max) // No range, hue = 0.
    //    return 0;

    //  double hue;

    //  var chroma = (double)(max - min);

    //  if (chroma == 0) // No color range.
    //    return 0;
    //  else if (max == red)
    //    hue = ((green - blue) / chroma + 6) % 6;
    //  else if (max == green)
    //    hue = 2 + (blue - red) / chroma;
    //  else // if (max == blue) // No need for comparison, blue must be max.
    //    hue = 4 + (red - green) / chroma;

    //  hue *= 60;

    //  if (hue < 0)
    //    hue += 360;

    //  return hue;
    //}
    //public static double GetNormalizedChroma(byte red, byte green, byte blue, out double min, out double max, out double r, out double g, out double b)
    //{
    //  r = red / 255d;
    //  g = green / 255d;
    //  b = blue / 255d;

    //  max = System.Math.Max(System.Math.Max(r, g), b);
    //  min = System.Math.Min(System.Math.Min(r, g), b);

    //  return max - min;
    //}
    //public static double GetNormalizedHue(double nred, double ngreen, double nblue, double min, double max)
    //{
    //  if (min == max) // No range, hue = 0.
    //    return 0;

    //  double hue;

    //  var chroma = max - min;

    //  if (chroma == 0) // No range, hue = 0.
    //    return 0;
    //  else if (max == nred)
    //    hue = ((ngreen - nblue) / chroma + 6) % 6; // The % operator doesn't do proper modulo on negative numbers, so we'll add 6 before using it.
    //  else if (max == ngreen)
    //    hue = 2 + (nblue - nred) / chroma;
    //  else // if (max == blue) // No need for comparison, at this point blue must be max.
    //    hue = 4 + (nred - ngreen) / chroma;

    //  hue *= 60; // Convert to [0, 360] range.

    //  if (hue < 0)
    //    hue += 360; // If negative wrap-around to a positive degree in the [0, 360] range.

    //  return hue;
    //}

    /// <summary>
    /// Returns a color based on XAML color string.
    /// </summary>
    /// <param name="colorString">The color string. Any format used in XAML should work.</param>
    /// <returns>Parsed color</returns>
    //public static Rgb Parse(string colorString)
    //{
    //  if (string.IsNullOrEmpty(colorString)) throw new System.ArgumentNullException(nameof(colorString));

    //  if (colorString[0] == '#')
    //  {
    //    switch (colorString)
    //    {
    //      case var cs9 when cs9.Length == 9 && System.Convert.ToUInt32(cs9.Substring(1), 16) is var ci9:
    //        return new Rgb((byte)((ci9 >> 16) & 0xff), (byte)((ci9 >> 8) & 0xff), (byte)(ci9 & 0xff), (byte)(ci9 >> 24));
    //      case var cs7 when cs7.Length == 7 && System.Convert.ToUInt32(cs7.Substring(1), 16) is var ci7:
    //        return new Rgb((byte)((ci7 >> 16) & 0xff), (byte)((ci7 >> 8) & 0xff), (byte)(ci7 & 0xff), 255);
    //      case var cs5 when cs5.Length == 5 && System.Convert.ToUInt16(cs5.Substring(1), 16) is var c:
    //        var a5 = (byte)(c >> 12);
    //        var r5 = (byte)((c >> 8) & 0xf);
    //        var g5 = (byte)((c >> 4) & 0xf);
    //        var b5 = (byte)(c & 0xf);
    //        a5 = (byte)(a5 << 4 | a5);
    //        r5 = (byte)(r5 << 4 | r5);
    //        g5 = (byte)(g5 << 4 | g5);
    //        b5 = (byte)(b5 << 4 | b5);
    //        return new Rgb(r5, g5, b5, a5);
    //      case var cs4 when cs4.Length == 4 && System.Convert.ToUInt16(cs4.Substring(1), 16) is var ci4:
    //        var r4 = (byte)((ci4 >> 8) & 0xf);
    //        var g4 = (byte)((ci4 >> 4) & 0xf);
    //        var b4 = (byte)(ci4 & 0xf);
    //        r4 = (byte)(r4 << 4 | r4);
    //        g4 = (byte)(g4 << 4 | g4);
    //        b4 = (byte)(b4 << 4 | b4);
    //        return new Rgb(r4, g4, b4, 255);
    //      default:
    //        throw new System.FormatException($"The {colorString} string passed in the colorString argument is not a recognized Color format.");
    //    }
    //  }

    //  if (colorString.Length > 3 && colorString.StartsWith(@"sc#", System.StringComparison.OrdinalIgnoreCase))
    //  {
    //    return (colorString.Substring(3).Split(',')) switch
    //    {
    //      var s4 when s4.Length == 4 => new Rgb((byte)(double.Parse(s4[1], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) * 255), (byte)(double.Parse(s4[2], System.Globalization.CultureInfo.CurrentCulture) * 255), (byte)(double.Parse(s4[3], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) * 255), (byte)(double.Parse(s4[0], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) * 255)),
    //      var s3 when s3.Length == 3 => new Rgb((byte)(double.Parse(s3[0], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) * 255), (byte)(double.Parse(s3[1], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) * 255), (byte)(double.Parse(s3[2], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) * 255), 255),
    //      _ => throw new System.FormatException($"The {colorString} string passed in the colorString argument is not a recognized Color format (sc#[scA,]scR,scG,scB)."),
    //    };
    //  }

    //  throw new System.FormatException($"The {colorString} string passed in the colorString argument is not a recognized Color.");
    //}

    #endregion // Static methods

    #region Implemented interfaces

    public string ToHtmlColorString() => $"rgb({Red}, {Green}, {Blue})";

    public string ToHtmlHexString() => $"#{Red:X2}{Green:X2}{Blue:X2}";

    #endregion // Implemented interfaces

    public override string ToString() => $"{GetType().Name} {{ Red = {m_red}, Green = {m_green}, Blue = {m_blue} }}";
  }
}
