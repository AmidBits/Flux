namespace Flux.Colors
{
  /// <summary>Hsva is the same as Hsv with the addition of an alpha channel.</summary>
  public struct Ahsv
    : System.IEquatable<Ahsv>
  {
    public static readonly Ahsv Empty;

    private double m_alpha;
    private Hsv m_hsv;

    public Ahsv(double alpha, Hsv hsv)
    {
      m_alpha = alpha >= 0 && alpha <= 1 ? alpha : throw new System.ArgumentOutOfRangeException(nameof(alpha));
      m_hsv = hsv;
    }
    public Ahsv(double alpha, double hue, double saturation, double value)
      : this(alpha, new Hsv(hue, saturation, value))
    { }

    public double Alpha { get => m_alpha; set => m_alpha = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    public Hsv HSV { get => m_hsv; set => m_hsv = value; }

    /// <summary>Creates an HSL color corresponding to the HSV instance.</summary>
    public Ahsl ToAhsl()
      => new(m_alpha, HSV.ToHsl());
    /// <summary>Creates an RGB color corresponding to the HSV instance.</summary>
    public Argb ToArgb()
      => new(System.Convert.ToByte(m_alpha * 255), HSV.ToRgb());

    #region Static methods
    public static Ahsv FromRandom(System.Random rng)
      => new(rng.NextDouble(0, 360), rng.NextDouble(), rng.NextDouble(), rng.NextDouble());
    public static Ahsv FromRandom()
      => FromRandom(Randomization.NumberGenerator.Crypto);
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(Ahsv a, Ahsv b)
      => a.Equals(b);
    public static bool operator !=(Ahsv a, Ahsv b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Ahsv other)
      => m_alpha == other.m_alpha && HSV.Equals(other.HSV);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Ahsv o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_alpha, HSV);
    public override string ToString()
      => $"{GetType().Name} {{ {(m_alpha * 100):N1}%, {HSV.Hue:N1}\u00B0, {HSV.Saturation * 100:N1}%, {HSV.Value * 100:N1}% }}";
    #endregion Object overrides
  }
}
