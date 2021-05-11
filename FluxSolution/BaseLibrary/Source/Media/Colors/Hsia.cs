namespace Flux.Colors
{
  /// <summary>Hsia is the same as Hsi with the addition of an alpha channel.</summary>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
  public struct Hsia
    : System.IEquatable<Hsia>
  {
    public static readonly Hsia Empty;
    public bool IsEmpty => Equals(Empty);

    [System.Runtime.InteropServices.FieldOffset(0)] private Hsi m_hsi;
    [System.Runtime.InteropServices.FieldOffset(24)] private double m_alpha;

    public Hsi HSI { get => m_hsi; set => m_hsi = value; }
    public double Alpha { get => m_alpha; set => m_alpha = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

    public Hsia(Hsi hsi, double alpha)
    {
      m_hsi = hsi;
      m_alpha = alpha >= 0 && alpha <= 1 ? alpha : throw new System.ArgumentOutOfRangeException(nameof(alpha));
    }
    public Hsia(double hue, double saturation, double intensity, double alpha)
      : this(new Hsi(hue, saturation, intensity), alpha)
    {
    }

    public static Hsia Random(System.Random rng)
    {
      if (rng is null) throw new System.ArgumentNullException(nameof(rng));

      return new Hsia(rng.NextDouble(0, 360), rng.NextDouble(), rng.NextDouble(), rng.NextDouble());
    }
    public static Hsia Random()
      => Random(Flux.Random.NumberGenerator.Crypto);

    /// <summary>Creates an RGB color corresponding to the HSI instance.</summary>
    public Rgba ToRgba()
      => new Rgba(HSI.ToRgb(), System.Convert.ToByte(Alpha * 255));

    // Operators
    public static bool operator ==(Hsia a, Hsia b)
      => a.Equals(b);
    public static bool operator !=(Hsia a, Hsia b)
      => !a.Equals(b);

    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Hsia other)
      => HSI == other.HSI && Alpha == other.Alpha;

    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is Hsia o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(HSI.Hue, HSI.Saturation, HSI.Intensity, Alpha);
    public override string ToString()
      => $"<{GetType().Name}: {HSI.Hue}, {HSI.Saturation}, {HSI.Intensity}, {Alpha}>";
  }
}
