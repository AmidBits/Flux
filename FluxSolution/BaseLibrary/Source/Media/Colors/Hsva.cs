namespace Flux.Colors
{
  /// <summary>Hsva is the same as Hsv with the addition of an alpha channel.</summary>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
  public struct Hsva
    : System.IEquatable<Hsva>
  {
    public static readonly Hsva Empty;
    public bool IsEmpty => Equals(Empty);

    [System.Runtime.InteropServices.FieldOffset(0)] private Hsv m_hsv;
    [System.Runtime.InteropServices.FieldOffset(24)] private double m_alpha;

    public Hsv HSV { get => m_hsv; set => m_hsv = value; }
    public double Alpha { get => m_alpha; set => m_alpha = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

    public Hsva(Hsv hsv, double alpha)
    {
      m_hsv = hsv;
      m_alpha = alpha >= 0 && alpha <= 1 ? alpha : throw new System.ArgumentOutOfRangeException(nameof(alpha));
    }
    public Hsva(double hue, double saturation, double value, double alpha)
      : this(new Hsv(hue, saturation, value), alpha)
    {
    }

    public static Hsva Random(System.Random rng)
      => new Hsva(rng.NextDouble(0, 360), rng.NextDouble(), rng.NextDouble(), rng.NextDouble());
    public static Hsva Random()
      => Random(Flux.Random.NumberGenerator.Crypto);

    /// <summary>Creates an HSL color corresponding to the HSV instance.</summary>
    public Hsla ToHsla()
      => new Hsla(HSV.ToHsl(), Alpha);
    /// <summary>Creates an RGB color corresponding to the HSV instance.</summary>
    public Rgba ToRgba()
      => new Rgba(HSV.ToRgb(), System.Convert.ToByte(Alpha * 255));

    // Operators
    public static bool operator ==(Hsva a, Hsva b)
      => a.Equals(b);
    public static bool operator !=(Hsva a, Hsva b)
      => !a.Equals(b);

    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Hsva other)
      => HSV == other.HSV && Alpha == other.Alpha;

    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is Hsva o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(HSV.Hue, HSV.Saturation, HSV.Value, Alpha);
    public override string ToString()
      => $"<{GetType().Name}: {HSV.Hue}, {HSV.Saturation}, {HSV.Value}, {Alpha}>";
  }
}
