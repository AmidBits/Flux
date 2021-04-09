namespace Flux.Colors
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
  public struct Hsi
    : System.IEquatable<Hsi>
  {
    public static readonly Hsi Empty;
    public bool IsEmpty => Equals(Empty);

    [System.Runtime.InteropServices.FieldOffset(0)] private double m_hue;
    [System.Runtime.InteropServices.FieldOffset(8)] private double m_saturation;
    [System.Runtime.InteropServices.FieldOffset(16)] private double m_intensity;
    [System.Runtime.InteropServices.FieldOffset(24)] private double? m_alpha;

    public double Hue { get => m_hue; set => m_hue = value >= 0 && value <= 360 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    public double Saturation { get => m_saturation; set => m_saturation = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    public double Intensity { get => m_intensity; set => m_intensity = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    public double Alpha { get => m_alpha ?? 1; set => m_alpha = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

    public Hsi(double hue, double saturation, double intensity, double alpha)
    {
      m_hue = hue >= 0 && hue <= 360 ? hue : throw new System.ArgumentOutOfRangeException(nameof(hue));
      m_saturation = saturation >= 0 && saturation <= 1 ? saturation : throw new System.ArgumentOutOfRangeException(nameof(saturation));
      m_intensity = intensity >= 0 && intensity <= 1 ? intensity : throw new System.ArgumentOutOfRangeException(nameof(intensity));
      m_alpha = alpha >= 0 && alpha <= 1 ? alpha : throw new System.ArgumentOutOfRangeException(nameof(saturation));
    }
    public Hsi(double hue, double saturation, double intensity)
      : this(hue, saturation, intensity, 1)
    { }

    public static Hsi Random(System.Random rng)
    {
      if (rng is null) throw new System.ArgumentNullException(nameof(rng));

      return new Hsi(rng.NextDouble(0, 360), rng.NextDouble(), rng.NextDouble(), rng.NextDouble());
    }
    public static Hsi Random()
      => Random(Flux.Random.NumberGenerator.Crypto);

    public double GetChroma()
      => 3 * m_intensity * m_saturation / (1 + (1 - System.Math.Abs((m_hue / 60 % 2) - 1)));

    /// <summary>Creates an RGB color corresponding to the HSI instance.</summary>
    public Rgb ToRgb()
    {
      double hue1 = Hue / 60;
      double z = 1 - System.Math.Abs((hue1 % 2) - 1);
      double chroma = (3 * Intensity * Saturation) / (1 + z);
      double x = chroma * z;

      double m = Intensity * (1 - Saturation);
      double r1 = m, g1 = m, b1 = m;

      switch (hue1)
      {
        case var v1 when v1 < 1:
          r1 += chroma;
          g1 += x;
          break;
        case var v2 when v2 < 2:
          r1 += x;
          g1 += chroma;
          break;
        case var v3 when v3 < 3:
          g1 += chroma;
          b1 += x;
          break;
        case var v4 when v4 < 4:
          g1 += x;
          b1 += chroma;
          break;
        case var v5 when v5 < 5:
          r1 += x;
          b1 += chroma;
          break;
        default: // h1 <= 6 //
          r1 += chroma;
          b1 += x;
          break;
      }

      return new Rgb(
        System.Convert.ToByte(255 * Alpha),
        System.Convert.ToByte(255 * r1),
        System.Convert.ToByte(255 * g1),
        System.Convert.ToByte(255 * b1)
      );
    }

    // Operators
    public static bool operator ==(Hsi a, Hsi b)
      => a.Equals(b);
    public static bool operator !=(Hsi a, Hsi b)
      => !a.Equals(b);

    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Hsi other)
      => Alpha == other.Alpha && Hue == other.Hue && Saturation == other.Saturation && Intensity == other.Intensity;

    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is Hsi o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Hue, Saturation, Intensity, Alpha);
    public override string ToString()
      => $"<{GetType().Name}: {Hue}, {Saturation}, {Intensity}, {Alpha}>";
  }
}
