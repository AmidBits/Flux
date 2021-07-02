namespace Flux.Colors
{
  /// <summary>Cmyka is the same as Cmyk with the addition of an alpha channel.</summary>
  public struct Cmyka
    : System.IEquatable<Cmyka>
  {
    public static readonly Cmyka Empty;
    public bool IsEmpty => Equals(Empty);

    private Cmyk m_cmyk;
    private double m_alpha;

    public Cmyk CMYK { get => m_cmyk; set => m_cmyk = value; }
    public double Alpha { get => m_alpha; set => m_alpha = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

    public Cmyka(Cmyk cmyk, double alpha)
    {
      m_cmyk = cmyk;
      m_alpha = alpha >= 0 && alpha <= 1 ? alpha : throw new System.ArgumentOutOfRangeException(nameof(alpha));
    }
    public Cmyka(double cyan, double magenta, double yellow, double black, double alpha)
      : this(new Cmyk(cyan, magenta, yellow, black), alpha)
    { }

    /// <summary>Creates an RGB color corresponding to the CMYK instance.</summary>
    public Rgba ToRgba()
      => new Rgba(CMYK.ToRgb(), System.Convert.ToByte(Alpha * 255));

    #region Static members
    public static Cmyka FromRandom(System.Random rng)
    {
      if (rng is null) throw new System.ArgumentNullException(nameof(rng));

      return new Cmyka(rng.NextDouble(), rng.NextDouble(), rng.NextDouble(), rng.NextDouble(), rng.NextDouble());
    }
    public static Cmyka FromRandom()
      => FromRandom(Randomization.NumberGenerator.Crypto);
    #endregion Static members

    #region Overloaded operators
    public static bool operator ==(Cmyka a, Cmyka b)
      => a.Equals(b);
    public static bool operator !=(Cmyka a, Cmyka b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Cmyka other)
      => CMYK == other.CMYK && Alpha == other.Alpha;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Cmyka o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(CMYK, Alpha);
    public override string ToString()
      => $"<{GetType().Name}: {(CMYK.Cyan * 360):N1}\u00B0, {(CMYK.Magenta * 360):N1}\u00B0, {(CMYK.Yellow * 360):N1}\u00B0, {(CMYK.Key * 360):N1}\u00B0, {(m_alpha * 100):N1}%>";
    #endregion Object overrides
  }
}
