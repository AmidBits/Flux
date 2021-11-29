namespace Flux.Colors
{
#if NET5_0
  public struct Ahwb
    : System.IEquatable<Ahwb>
#else
  public record struct Ahwb
#endif
  {
    public static readonly Ahwb Empty;

    private double m_alpha;
    private Hwb m_hwb;

    public Ahwb(double alpha, Hwb hwb)
    {
      m_alpha = alpha >= 0 && alpha <= 1 ? alpha : throw new System.ArgumentOutOfRangeException(nameof(alpha));
      m_hwb = hwb;
    }
    public Ahwb(double alpha, double hue, double white, double black)
      : this(alpha, new Hwb(hue, white, black))
    { }

    public double Alpha { get => m_alpha; set => m_alpha = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    public Hwb HWB { get => m_hwb; set => m_hwb = value; }

    /// <summary>Converts the Hwb to a corresponding HSV color.</summary>
    public Ahsv ToAhsv()
      => new(m_alpha, HWB.ToHsv());
    /// <summary>Converts the Hwb to a corresponding RGB color.</summary>
    public Argb ToArgb()
      => new(System.Convert.ToByte(m_alpha * 255), HWB.ToRgb());

    #region Static methods
    public static Ahwb FromRandom(System.Random rng)
    {
      if (rng is null) throw new System.ArgumentNullException(nameof(rng));

      return new Ahwb(rng.NextDouble(0, 360), rng.NextDouble(), rng.NextDouble(), rng.NextDouble());
    }
    public static Ahwb FromRandom()
      => FromRandom(Randomization.NumberGenerator.Crypto);
    #endregion Static methods

    #region Overloaded operators
#if NET5_0
    public static bool operator ==(Ahwb a, Ahwb b)
      => a.Equals(b);
    public static bool operator !=(Ahwb a, Ahwb b)
      => !a.Equals(b);
#endif
    #endregion Overloaded operators

    #region Implemented interfaces
#if NET5_0
    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Ahwb other)
      => m_alpha == other.m_alpha && HWB.Equals(other.HWB);
#endif
    #endregion Implemented interfaces

    #region Object overrides
#if NET5_0
    public override bool Equals(object? obj)
      => obj is Ahwb o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_alpha, HWB);
#endif
    public override string ToString()
      => $"{GetType().Name} {{ {(m_alpha * 100):N1}%, {HWB.Hue:N1}\u00B0, {HWB.White * 100:N1}%, {HWB.Black * 100:N1}% }}";
    #endregion Object overrides
  }
}
