namespace Flux.Colors
{
  public struct Ahwb
    : System.IEquatable<Ahwb>
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
      => new Ahsv(m_alpha, HWB.ToHsv());
    /// <summary>Converts the Hwb to a corresponding RGB color.</summary>
    public Argb ToArgb()
      => new Argb(System.Convert.ToByte(m_alpha * 255), HWB.ToRgb());

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
    public static bool operator ==(Ahwb a, Ahwb b)
      => a.Equals(b);
    public static bool operator !=(Ahwb a, Ahwb b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Ahwb other)
      => m_alpha == other.m_alpha && HWB.Equals(other.HWB);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Ahwb o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_alpha, HWB);
    public override string ToString()
      => $"<{GetType().Name}: {HWB.Hue}, {HWB.White}, {HWB.Black}, {m_alpha}>";
    #endregion Object overrides
  }
}
