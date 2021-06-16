namespace Flux.Colors
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
  public struct Hwba
    : System.IEquatable<Hwba>
  {
    public static readonly Hwba Empty;
    public bool IsEmpty => Equals(Empty);

    [System.Runtime.InteropServices.FieldOffset(0)] private Hwb m_hwb;
    [System.Runtime.InteropServices.FieldOffset(8)] private double m_alpha;

    public Hwb HWB { get => m_hwb; set => m_hwb = value; }
    public double Alpha { get => m_alpha; set => m_alpha = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

    public Hwba(Hwb hwb, double alpha)
    {
      m_hwb = hwb;
      m_alpha = alpha >= 0 && alpha <= 1 ? alpha : throw new System.ArgumentOutOfRangeException(nameof(alpha));
    }
    public Hwba(double hue, double white, double black, double alpha)
      : this(new Hwb(hue, white, black), alpha)
    { }

    /// <summary>Converts the Hwb to a corresponding HSV color.</summary>
    public Hsva ToHsva()
      => new Hsva(HWB.ToHsv(), Alpha);
    /// <summary>Converts the Hwb to a corresponding RGB color.</summary>
    public Rgba ToRgba()
      => new Rgba(HWB.ToRgb(), System.Convert.ToByte(Alpha * 255));

    #region Static methods
    public static Hwba Random(System.Random rng)
    {
      if (rng is null) throw new System.ArgumentNullException(nameof(rng));

      return new Hwba(rng.NextDouble(0, 360), rng.NextDouble(), rng.NextDouble(), rng.NextDouble());
    }
    public static Hwba Random()
      => Random(Flux.Random.NumberGenerator.Crypto);
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(Hwba a, Hwba b)
      => a.Equals(b);
    public static bool operator !=(Hwba a, Hwba b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Hwba other)
      => HWB == other.HWB && Alpha == other.Alpha;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Hwba o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(HWB, Alpha);
    public override string ToString()
      => $"<{GetType().Name}: {HWB.Hue}, {HWB.White}, {HWB.Black}, {Alpha}>";
    #endregion Object overrides
  }
}
