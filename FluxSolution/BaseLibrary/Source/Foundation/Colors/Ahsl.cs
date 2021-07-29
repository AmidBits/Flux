namespace Flux.Colors
{
  /// <summary>Hsla is the same as Hsl with the addition of an alpha channel.</summary>
  public struct Ahsl
    : System.IEquatable<Ahsl>
  {
    public static readonly Ahsl Empty;
    public bool IsEmpty => Equals(Empty);

    private double m_alpha;
    private Hsl m_hsl;

    public double Alpha { get => m_alpha; set => m_alpha = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    public Hsl HSL { get => m_hsl; set => m_hsl = value; }

    public Ahsl(double alpha, Hsl hsl)
    {
      m_alpha = alpha >= 0 && alpha <= 1 ? alpha : throw new System.ArgumentOutOfRangeException(nameof(alpha));
      m_hsl = hsl;
    }
    public Ahsl(double alpha, double hue, double saturation, double lightness)
      : this(alpha, new Hsl(hue, saturation, lightness))
    { }

    /// <summary>Creates an HSVA color corresponding to the HSL instance.</summary>
    public Ahsv ToAhsv()
      => new Ahsv(m_alpha, HSL.ToHsv());
    /// <summary>Creates an RGBA color corresponding to the HSL instance.</summary>
    public Argb ToArgb()
      => new Argb(System.Convert.ToByte(255 * m_alpha), HSL.ToRgb());

    public string ToStringHtmlHsla()
      => $"hsla({HSL.Hue}, {HSL.Saturation}%, {HSL.Lightness}%, {m_alpha})";

    #region Static methods
    public static Ahsl FromRandom(System.Random rng)
    {
      if (rng is null) throw new System.ArgumentNullException(nameof(rng));

      return new Ahsl(rng.NextDouble(), Hsl.FromRandom(rng));
    }
    public static Ahsl FromRandom()
      => FromRandom(Randomization.NumberGenerator.Crypto);
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(Ahsl a, Ahsl b)
      => a.Equals(b);
    public static bool operator !=(Ahsl a, Ahsl b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Ahsl other)
      => m_alpha == other.m_alpha && HSL.Equals(other.HSL);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Ahsl o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_alpha, HSL);
    public override string ToString()
      => $"<{GetType().Name}: {HSL.Hue}, {HSL.Saturation}, {HSL.Lightness}, {m_alpha}>";
    #endregion Object overrides
  }
}
