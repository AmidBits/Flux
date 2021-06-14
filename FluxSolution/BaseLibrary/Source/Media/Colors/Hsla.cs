namespace Flux.Media.Colors
{
  /// <summary>Hsla is the same as Hsl with the addition of an alpha channel.</summary>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
  public struct Hsla
    : System.IEquatable<Hsla>
  {
    public static readonly Hsla Empty;
    public bool IsEmpty => Equals(Empty);

    [System.Runtime.InteropServices.FieldOffset(0)] private Hsl m_hsl;
    [System.Runtime.InteropServices.FieldOffset(24)] private double m_alpha;

    public Hsl HSL { get => m_hsl; set => m_hsl = value; }
    public double Alpha { get => m_alpha; set => m_alpha = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

    public Hsla(Hsl hsl, double alpha)
    {
      m_hsl = hsl;
      m_alpha = alpha >= 0 && alpha <= 1 ? alpha : throw new System.ArgumentOutOfRangeException(nameof(alpha));
    }
    public Hsla(double hue, double saturation, double lightness, double alpha)
      : this(new Hsl(hue, saturation, lightness), alpha)
    {
    }

    /// <summary>Creates an HSVA color corresponding to the HSL instance.</summary>
    public Hsva ToHsva()
      => new Hsva(HSL.ToHsv(), Alpha);
    /// <summary>Creates an RGBA color corresponding to the HSL instance.</summary>
    public Rgba ToRgba()
      => new Rgba(HSL.ToRgb(), System.Convert.ToByte(255 * Alpha));

    public string ToStringHtmlHsla()
      => $"hsla({HSL.Hue}, {HSL.Saturation}%, {HSL.Lightness}%, {Alpha})";

    #region Static methods
    public static Hsla Random(System.Random rng)
    {
      if (rng is null) throw new System.ArgumentNullException(nameof(rng));

      return new Hsla(Hsl.Random(rng), rng.NextDouble());
    }
    public static Hsla Random()
      => Random(Flux.Random.NumberGenerator.Crypto);
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(Hsla a, Hsla b)
      => a.Equals(b);
    public static bool operator !=(Hsla a, Hsla b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Hsla other)
      => HSL == other.HSL && Alpha == other.Alpha;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Hsla o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(HSL, Alpha);
    public override string ToString()
      => $"<{GetType().Name}: {HSL.Hue}, {HSL.Saturation}, {HSL.Lightness}, {Alpha}>";
    #endregion Object overrides
  }
}
