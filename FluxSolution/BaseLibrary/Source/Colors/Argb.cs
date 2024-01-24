namespace Flux.Colors
{
  /// <summary>Rgba is the same as Rgb with the addition of an alpha channel.</summary>
  public readonly record struct Argb
    : IHtmlColorFormattable
  {
    private readonly byte m_alpha;
    private readonly Rgb m_rgb;

    public Argb(int alpha, Rgb rgb)
    {
      m_alpha = alpha >= 0 && alpha <= 255 ? (byte)alpha : throw new System.ArgumentOutOfRangeException(nameof(alpha));
      m_rgb = rgb;
    }
    public Argb(int alpha, int red, int green, int blue) : this(alpha, new Rgb(red, green, blue)) { }
    public Argb(int argb) : this((byte)(argb >> 24), (byte)((argb >> 16) & 0xFF), (byte)((argb >> 8) & 0xFF), (byte)(argb & 0xFF)) { }
    public Argb(System.ReadOnlySpan<byte> argb) : this(argb[0], argb[1], argb[2], argb[3]) { }
    public Argb(System.Random rng) : this((rng ?? System.Random.Shared).GetRandomBytes(4)) { }

    public byte Alpha { get => m_alpha; init => m_alpha = value; }
    public Rgb RGB { get => m_rgb; init => m_rgb = value; }

    /// <summary>Converts the RGB color to grayscale using the specified method.</summary>
    public Argb ToGrayscale(GrayscaleMethod method) => new(m_alpha, m_rgb.ToGrayscale(method));

    /// <summary>Creates a CMYKA color corresponding to the RGBA instance.</summary>
    public Acmyk ToAcmyk() => new(m_alpha / 255d, RGB.ToCmyk());

    /// <summary>Creates an HSIA color corresponding to the RGBA instance.</summary>
    public Ahsi ToAhsi() => new(m_alpha / 255d, RGB.ToHsi());

    /// <summary>Creates an HSLA color corresponding to the RGBA instance.</summary>
    public Ahsl ToAhsl() => new(m_alpha / 255d, RGB.ToHsl());

    /// <summary>Creates an HSVA color corresponding to the RGBA instance.</summary>
    public Ahsv ToAhsv() => new(m_alpha / 255d, RGB.ToHsv());

    /// <summary>Creates an HWBA color corresponding to the RGBA instance.</summary>
    public Ahwb ToAhwb() => new(m_alpha / 255d, RGB.ToHwb());

    public int ToInt() => (m_alpha << 24) | RGB.ToInt();

    #region Static methods

    /// <summary>Creates a new RGBA color by parsing the specified string.</summary>
    /// <param name="colorString">The color string. Any format used in XAML should work.</param>
    public static Argb Parse(string colorString)
    {
      if (string.IsNullOrEmpty(colorString)) throw new System.ArgumentNullException(nameof(colorString));

      if (colorString[0] == '#')
      {
        switch (colorString)
        {
          case var cs9 when cs9.Length == 9 && System.Convert.ToUInt32(cs9[1..], 16) is var ci9:
            return new Argb((byte)((ci9 >> 16) & 0xff), (byte)((ci9 >> 8) & 0xff), (byte)(ci9 & 0xff), (byte)(ci9 >> 24));
          case var cs7 when cs7.Length == 7 && System.Convert.ToUInt32(cs7[1..], 16) is var ci7:
            return new Argb((byte)((ci7 >> 16) & 0xff), (byte)((ci7 >> 8) & 0xff), (byte)(ci7 & 0xff), 255);
          case var cs5 when cs5.Length == 5 && System.Convert.ToUInt16(cs5[1..], 16) is var c:
            var a5 = (byte)(c >> 12);
            var r5 = (byte)((c >> 8) & 0xf);
            var g5 = (byte)((c >> 4) & 0xf);
            var b5 = (byte)(c & 0xf);
            a5 = (byte)(a5 << 4 | a5);
            r5 = (byte)(r5 << 4 | r5);
            g5 = (byte)(g5 << 4 | g5);
            b5 = (byte)(b5 << 4 | b5);
            return new Argb(r5, g5, b5, a5);
          case var cs4 when cs4.Length == 4 && System.Convert.ToUInt16(cs4[1..], 16) is var ci4:
            var r4 = (byte)((ci4 >> 8) & 0xf);
            var g4 = (byte)((ci4 >> 4) & 0xf);
            var b4 = (byte)(ci4 & 0xf);
            r4 = (byte)(r4 << 4 | r4);
            g4 = (byte)(g4 << 4 | g4);
            b4 = (byte)(b4 << 4 | b4);
            return new Argb(r4, g4, b4, 255);
          default:
            throw new System.FormatException($"The {colorString} string passed in the colorString argument is not a recognized Color format.");
        }
      }

      if (colorString.Length > 3 && colorString.StartsWith(@"sc#", System.StringComparison.OrdinalIgnoreCase))
      {
        return (colorString[3..].Split(',')) switch
        {
          var s4 when s4.Length == 4 => new Argb((byte)(double.Parse(s4[1], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) * 255), (byte)(double.Parse(s4[2], System.Globalization.CultureInfo.CurrentCulture) * 255), (byte)(double.Parse(s4[3], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) * 255), (byte)(double.Parse(s4[0], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) * 255)),
          var s3 when s3.Length == 3 => new Argb((byte)(double.Parse(s3[0], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) * 255), (byte)(double.Parse(s3[1], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) * 255), (byte)(double.Parse(s3[2], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) * 255), 255),
          _ => throw new System.FormatException($"The {colorString} string passed in the colorString argument is not a recognized Color format (sc#[scA,]scR,scG,scB)."),
        };
      }

      throw new System.FormatException($"The {colorString} string passed in the colorString argument is not a recognized Color.");
    }

    #endregion Static methods

    #region Overloaded operators

    public static explicit operator int(Argb v) => v.ToInt();
    public static explicit operator Argb(int v) => new(v);

    #endregion Overloaded operators

    #region Implemented interfaces

    /// <summary>Converts a Color value to a string representation of the value in hexadecimal.</summary>
    public string ToHtmlHexString() => $"#{m_alpha:X2}{RGB.Red:X2}{RGB.Green:X2}{RGB.Blue:X2}";

    /// <summary>Converts a Color value to a string representation of the value in decimal.</summary>
    public string ToHtmlColorString() => $"rgba({RGB.Red}, {RGB.Green}, {RGB.Blue}, {m_alpha / 255.0})";

    /// <summary>Converts a Color value to a scRGB string representation of the value.</summary>
    /// <see href="https://en.wikipedia.org/wiki/ScRGB"/>
    /// <param name="color">The Color to convert.</param>
    /// <returns>Returns a string representing the hex value.</returns>
    public string ToHtmlScString() => $"sc#{m_alpha / 255F}{RGB.Red / 255F}{RGB.Green / 255F}{RGB.Blue / 255F}";

    #endregion // Implemented interfaces

    public override string ToString() => $"{GetType().Name} {{ Alpha = {m_alpha}, Red = {RGB.Red}, Green = {RGB.Green}, Blue = {RGB.Blue} }}";
  }
}
