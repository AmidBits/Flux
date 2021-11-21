namespace Flux.Colors
{
  /// <summary>Cmyka is the same as Cmyk with the addition of an alpha channel.</summary>
  public struct Acmyk
    : System.IEquatable<Acmyk>
  {
    public static readonly Acmyk Empty;

    private double m_alpha;
    private Cmyk m_cmyk;

    public Acmyk(double alpha, Cmyk cmyk)
    {
      m_alpha = alpha >= 0 && alpha <= 1 ? alpha : throw new System.ArgumentOutOfRangeException(nameof(alpha));
      m_cmyk = cmyk;
    }
    public Acmyk(double alpha, double cyan, double magenta, double yellow, double black)
      : this(alpha, new Cmyk(cyan, magenta, yellow, black))
    { }

    public double Alpha { get => m_alpha; set => m_alpha = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    public Cmyk CMYK { get => m_cmyk; set => m_cmyk = value; }

    /// <summary>Creates an RGB color corresponding to the CMYK instance.</summary>
    public Argb ToArgb()
      => new(System.Convert.ToByte(m_alpha * 255), CMYK.ToRgb());

    #region Static members
    public static Acmyk FromRandom(System.Random rng)
    {
      if (rng is null) throw new System.ArgumentNullException(nameof(rng));

      return new Acmyk(rng.NextDouble(), rng.NextDouble(), rng.NextDouble(), rng.NextDouble(), rng.NextDouble());
    }
    public static Acmyk FromRandom()
      => FromRandom(Randomization.NumberGenerator.Crypto);
    #endregion Static members

    #region Overloaded operators
    public static bool operator ==(Acmyk a, Acmyk b)
      => a.Equals(b);
    public static bool operator !=(Acmyk a, Acmyk b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Acmyk other)
      => m_alpha == other.m_alpha && CMYK.Equals(other.CMYK);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Acmyk o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(CMYK, Alpha);
    public override string ToString()
      => $"{GetType().Name} {{ {(m_alpha * 100):N1}%, {CMYK.Cyan * 360:N1}\u00B0, {CMYK.Magenta * 360:N1}\u00B0, {CMYK.Yellow * 360:N1}\u00B0, {CMYK.Key * 360:N1}\u00B0 }}";
    #endregion Object overrides
  }
}
