namespace Flux.Colors
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
  public struct Rgb
    : System.IEquatable<Rgb>
  {
    public static readonly Rgb Empty;
    public bool IsEmpty => Equals(Empty);

    [System.Runtime.InteropServices.FieldOffset(0)] private byte m_red;
    [System.Runtime.InteropServices.FieldOffset(1)] private byte m_green;
    [System.Runtime.InteropServices.FieldOffset(2)] private byte m_blue;

    public byte Red { get => m_red; set => m_red = value >= 0 && value <= 255 ? (byte)value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    public byte Green { get => m_green; set => m_green = value >= 0 && value <= 255 ? (byte)value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    public byte Blue { get => m_blue; set => m_blue = value >= 0 && value <= 255 ? (byte)value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

    public Rgb(byte red, byte green, byte blue)
    {
      m_red = red >= 0 && red <= 255 ? red : throw new System.ArgumentOutOfRangeException(nameof(red));
      m_green = green >= 0 && green <= 255 ? green : throw new System.ArgumentOutOfRangeException(nameof(green));
      m_blue = blue >= 0 && blue <= 255 ? blue : throw new System.ArgumentOutOfRangeException(nameof(blue));
    }

    public static Rgb Random(System.Random rng)
    {
      if (rng is null) throw new System.ArgumentNullException(nameof(rng));

      var bytes = rng.GetRandomBytes(3);

      return new Rgb(bytes[0], bytes[1], bytes[2]);
    }
    public static Rgb Random()
      => Random(Flux.Random.NumberGenerator.Crypto);

    //public double GetChroma()
    //  => System.Math.Max(System.Math.Max(m_red, m_green), m_blue) - System.Math.Min(System.Math.Min(m_red, m_green), m_blue);
    public double GetHue()
    {
      var max = System.Math.Max(System.Math.Max(m_red, m_green), m_blue);
      var min = System.Math.Min(System.Math.Min(m_red, m_green), m_blue);

      if (max == min)
        return 0;

      var range = max - min;

      var hue = 0D;

      if (max == m_red)
        hue = (((m_green - m_blue) / range) + 6) % 6; // The % operator doesn't do proper modulo on negative numbers, so we'll add 6 before using it.
      else if (max == m_green)
        hue = ((m_blue - m_red) / range) + 2;
      else if (max == m_blue)
        hue = ((m_red - m_green) / range) + 4;

      hue *= 60;

      if (hue < 0)
        hue += 360;

      return hue;
    }

    /// <summary>Converts the RGB color to grayscale using the specified method.</summary>
    public Rgb ToGrayscale(GrayscaleMethod method)
    {
      var gray = method switch
      {
        GrayscaleMethod.Average => System.Convert.ToByte((m_red + m_green + m_blue) / 3),
        GrayscaleMethod.Lightness => System.Convert.ToByte((System.Math.Max(System.Math.Max(m_red, m_green), m_blue) + System.Math.Min(System.Math.Min(m_red, m_green), m_blue)) / 2),
        GrayscaleMethod.Luminosity => System.Convert.ToByte(m_red * 0.21 + m_green * 0.72 + m_blue * 0.07),
        _ => throw new System.ArgumentOutOfRangeException(nameof(method)),
      };

      return new Rgb(gray, gray, gray);
    }

    /// <summary>Converts the RGB to a corresponding CMYK color.</summary>
    public Cmyk ToCmyk()
    {
      var red = m_red / 255d;
      var green = m_green / 255d;
      var blue = m_blue / 255d;

      var key = 1 - System.Math.Max(red, System.Math.Max(green, blue));
      var m1key = 1 - key;

      var cyan = (m1key - red) / m1key;
      var magenta = (m1key - green) / m1key;
      var yellow = (m1key - blue) / m1key;

      return new Cmyk(
        cyan < 0 ? 0 : cyan,
        magenta < 0 ? 0 : magenta,
        yellow < 0 ? 0 : yellow,
        key
      );
    }
    /// <summary>Converts the RGB to a corresponding HSI color.</summary>
    public Hsi ToHsi()
    {
      var rgb = (double)(m_red + m_green + m_blue);

      var r = m_red / rgb;
      var g = m_green / rgb;
      var b = m_blue / rgb;

      var h = System.Math.Acos(0.5 * (r - g + (r - b)) / System.Math.Pow(System.Math.Pow(r - g, 2) + (r - b) * (g - b), 0.5));
      if (b > g)
        h = 2 * System.Math.PI - h;

      return new Hsi(
        h,
        1 - 3 * Maths.Min(r, g, b),
        rgb / (3 * 255)
      );
    }
    /// <summary>Converts the RGB to a corresponding HSL color.</summary>
    public Hsl ToHsl()
    {
      var r = m_red / 255d;
      var g = m_green / 255d;
      var b = m_blue / 255d;

      var max = System.Math.Max(System.Math.Max(r, g), b);
      var min = System.Math.Min(System.Math.Min(r, g), b);

      var chroma = max - min;

      double h1;

      if (chroma == 0) h1 = 0;
      else if (max == r) h1 = (((g - b) / chroma) + 6) % 6; // The % operator doesn't do proper modulo on negative numbers, so we'll add 6 before using it.
      else if (max == g) h1 = 2 + ((b - r) / chroma);
      else h1 = 4 + ((r - g) / chroma);

      var luminosity = 0.5 * (max + min);

      return new Hsl(
        h1 * 60,
        chroma == 0 ? 0 : chroma / (1 - System.Math.Abs(2 * luminosity - 1)),
        luminosity
      );
    }
    /// <summary>Converts the RGB to a corresponding HSV color.</summary>
    public Hsv ToHsv()
    {
      var r = m_red / 255d;
      var g = m_green / 255d;
      var b = m_blue / 255d;

      var max = System.Math.Max(System.Math.Max(r, g), b);
      var min = System.Math.Min(System.Math.Min(r, g), b);

      var chroma = max - min;

      double h1;

      if (chroma == 0) h1 = 0;
      else if (max == r) h1 = (((g - b) / chroma) + 6) % 6; // The % operator doesn't do proper modulo on negative numbers, so we'll add 6 before using it.
      else if (max == g) h1 = 2 + ((b - r) / chroma);
      else h1 = 4 + ((r - g) / chroma);

      return new Hsv(
        h1 * 60,
        chroma == 0 ? 0 : chroma / max,
        max
      );
    }
    /// <summary>Converts the RGB to a corresponding HWB color.</summary>
    public Hwb ToHwb()
    {
      var r = m_red / 255d;
      var g = m_green / 255d;
      var b = m_blue / 255d;

      var max = System.Math.Max(System.Math.Max(r, g), b);
      var min = System.Math.Min(System.Math.Min(r, g), b);

      var chroma = max - min;

      double hue;

      if (chroma == 0) hue = 0;
      else if (max == r) hue = (((g - b) / chroma) + 6) % 6; // The % operator doesn't do proper modulo on negative numbers, so we'll add 6 before using it.
      else if (max == g) hue = 2 + ((b - r) / chroma);
      else hue = 4 + ((r - g) / chroma);

      hue *= 60;

      if (hue < 0)
        hue += 360;

      return new Hwb(hue, min, 1 - max);
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

    public int ToInt()
      => ((byte)(Red >> 8) << 16) | ((byte)(Green >> 8) << 8) | (byte)(Blue >> 8);

    /// <summary>Converts a Color value to a string representation of the value in hexadecimal.</summary>
    /// <param name="color">The Color to convert.</param>
    /// <returns>Returns a string representing the hex value.</returns>
    public string ToStringHtmlHex()
      => $"#{Red:X2}{Green:X2}{Blue:X2}";
    public string ToStringHtmlRgb()
      => $"rgb({Red}, {Green}, {Blue})";

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

    // Operators
    public static bool operator ==(Rgb a, Rgb b)
      => a.Equals(b);
    public static bool operator !=(Rgb a, Rgb b)
      => !a.Equals(b);

    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Rgb other)
      => m_red == other.m_red && m_green == other.m_green && m_blue == other.m_blue;

    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is Rgb o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_red, m_green, m_blue);
    public override string ToString()
      => $"<{GetType().Name}: {m_red}, {m_green}, {m_blue}>";
  }
}
