namespace Flux.Colors
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
  public struct Hsl
    : System.IEquatable<Hsl>
  {
    public static readonly Hsl Empty;
    public bool IsEmpty => Equals(Empty);

    [System.Runtime.InteropServices.FieldOffset(0)] private double m_hue;
    [System.Runtime.InteropServices.FieldOffset(8)] private double m_saturation;
    [System.Runtime.InteropServices.FieldOffset(16)] private double m_lightness;
    [System.Runtime.InteropServices.FieldOffset(24)] private double? m_alpha;

    public double Hue { get => m_hue; set => m_hue = value >= 0 && value <= 360 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    public double Saturation { get => m_saturation; set => m_saturation = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    public double Lightness { get => m_lightness; set => m_lightness = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    public double Alpha { get => m_alpha ?? 1; set => m_alpha = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

    public Hsl(double hue, double saturation, double lightness, double alpha)
    {
      m_hue = hue >= 0 && hue <= 360 ? hue : throw new System.ArgumentOutOfRangeException(nameof(hue));
      m_saturation = saturation >= 0 && saturation <= 1 ? saturation : throw new System.ArgumentOutOfRangeException(nameof(saturation));
      m_lightness = lightness >= 0 && lightness <= 1 ? lightness : throw new System.ArgumentOutOfRangeException(nameof(lightness));
      m_alpha = alpha >= 0 && alpha <= 1 ? alpha : throw new System.ArgumentOutOfRangeException(nameof(saturation));
    }
    public Hsl(double hue, double saturation, double lightness)
      : this(hue, saturation, lightness, 1)
    {
    }

    public static Hsl Random(System.Random rng)
    {
      if (rng is null) throw new System.ArgumentNullException(nameof(rng));

      return new Hsl(rng.NextDouble(0, 360), rng.NextDouble(), rng.NextDouble(), rng.NextDouble());
    }
    public static Hsl Random()
      => Random(Flux.Random.NumberGenerator.Crypto);

    public double GetChroma()
      => (1 - System.Math.Abs(2 * m_lightness - 1)) * m_saturation;

    /// <summary>Creates an HSV color corresponding to the HSL instance.</summary>
    public Hsv ToHsv()
    {
      var h = Hue;
      var v = Lightness + Saturation * System.Math.Min(Lightness, 1 - Lightness);
      var s = v == 0 ? 0 : 2 * (1 - Lightness / v);

      return new Hsv(h, s, v, Alpha);
    }
    /// <summary>Creates an RGB color corresponding to the HSL instance.</summary>
    public Rgb ToRgb()
    {
      double c = GetChroma();
      double h1 = Hue / 60;
      double x = c * (1 - System.Math.Abs((h1 % 2) - 1));

      double m = Lightness - (0.5 * c);
      double r1 = m, g1 = m, b1 = m;

      switch (h1)
      {
        case >= 0 and <= 1:
          r1 += c;
          g1 += x;
          break;
        case > 1 and <= 2:
          r1 += x;
          g1 += c;
          break;
        case > 2 and <= 3:
          g1 += c;
          b1 += x;
          break;
        case > 3 and <= 4:
          g1 += x;
          b1 += c;
          break;
        case > 4 and <= 5:
          r1 += x;
          b1 += c;
          break;
        case > 5 and <= 6:
          r1 += c;
          b1 += x;
          break;
        default:
          break;
      }

      return new Rgb(
        System.Convert.ToByte(255 * r1),
        System.Convert.ToByte(255 * g1),
        System.Convert.ToByte(255 * b1),
        System.Convert.ToByte(255 * Alpha)
       );
    }

    // Operators
    public static bool operator ==(Hsl a, Hsl b)
      => a.Equals(b);
    public static bool operator !=(Hsl a, Hsl b)
      => !a.Equals(b);

    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Hsl other)
      => Hue == other.Hue && Saturation == other.Saturation && Lightness == other.Lightness && Alpha == other.Alpha;

    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is Hsl o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Hue, Saturation, Lightness, Alpha);
    public override string ToString()
      => $"<{GetType().Name}: {Hue}, {Saturation}, {Lightness}, {Alpha}>";
  }
}
