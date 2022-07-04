namespace Flux.Colors
{
  /// <summary>Hsla is the same as Hsl with the addition of an alpha channel.</summary>
  public readonly struct Ahsl
    : System.IEquatable<Ahsl>, IHtmlColorFormattable
  {
    public static readonly Ahsl Empty;

    private readonly double m_alpha;
    private readonly Hsl m_hsl;

    public Ahsl(double alpha, Hsl hsl)
    {
      m_alpha = alpha >= 0 && alpha <= 1 ? alpha : throw new System.ArgumentOutOfRangeException(nameof(alpha));
      m_hsl = hsl;
    }
    public Ahsl(double alpha, double hue, double saturation, double lightness)
      : this(alpha, new Hsl(hue, saturation, lightness))
    { }

    public double Alpha { get => m_alpha; init => m_alpha = value; }
    public Hsl HSL { get => m_hsl; init => m_hsl = value; }

    /// <summary>Creates an HSVA color corresponding to the HSL instance.</summary>
    public Ahsv ToAhsv()
      => new(m_alpha, HSL.ToHsv());
    /// <summary>Creates an RGBA color corresponding to the HSL instance.</summary>
    public Argb ToArgb()
      => new(System.Convert.ToByte(255 * m_alpha), HSL.ToRgb());

    public string ToHtmlColorString()
      => $"hsla({HSL.Hue}, {HSL.Saturation}%, {HSL.Lightness}%, {m_alpha})";

    #region Static methods
    public static Ahsl FromRandom(System.Random rng)
      => rng is null
      ? throw new System.ArgumentNullException(nameof(rng))
      : new(rng.NextDouble(), Hsl.FromRandom(rng));
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static bool operator ==(Ahsl a, Ahsl b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(Ahsl a, Ahsl b) => !a.Equals(b);
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
      => $"{GetType().Name} {{ {(m_alpha * 100):N1}%, {HSL.Hue:N1}\u00B0, {HSL.Saturation * 100:N1}%, {HSL.Lightness * 100:N1}% }}";
    #endregion Object overrides
  }
}
