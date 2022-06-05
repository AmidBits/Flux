namespace Flux.Colors
{
  /// <summary>Hsia is the same as Hsi with the addition of an alpha channel.</summary>
  public readonly struct Ahsi
    : System.IEquatable<Ahsi>
  {
    public static readonly Ahsi Empty;

    private readonly double m_alpha;
    private readonly Hsi m_hsi;

    public Ahsi(double alpha, Hsi hsi)
    {
      m_alpha = alpha >= 0 && alpha <= 1 ? alpha : throw new System.ArgumentOutOfRangeException(nameof(alpha));
      m_hsi = hsi;
    }
    public Ahsi(double alpha, double hue, double saturation, double intensity)
      : this(alpha, new Hsi(hue, saturation, intensity))
    { }

    public double Alpha { get => m_alpha; init => m_alpha = value; }
    public Hsi HSI { get => m_hsi; init => m_hsi = value; }

    /// <summary>Creates an RGB color corresponding to the HSI instance.</summary>
    public Argb ToArgb()
      => new(System.Convert.ToByte(m_alpha * 255), HSI.ToRgb());

    #region Static members
    public static Ahsi FromRandom(System.Random rng)
    {
      if (rng is null) throw new System.ArgumentNullException(nameof(rng));

      return new Ahsi(rng.NextDouble(0, 360), rng.NextDouble(), rng.NextDouble(), rng.NextDouble());
    }
    public static Ahsi FromRandom()
      => FromRandom(Randomization.NumberGenerator.Crypto);
    #endregion Static members

    #region Overloaded operators
    public static bool operator ==(Ahsi a, Ahsi b)
      => a.Equals(b);
    public static bool operator !=(Ahsi a, Ahsi b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interface
    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Ahsi other)
      => m_alpha == other.m_alpha && HSI.Equals(other.HSI);
    #endregion Implemented interface

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Ahsi o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_alpha, HSI);
    public override string ToString()
      => $"{GetType().Name} {{ {(m_alpha * 100):N1}%, {HSI.Hue:N1}\u00B0, {HSI.Saturation * 100:N1}%, {HSI.Intensity * 100:N1}% }}";
    #endregion Object overrides
  }
}
