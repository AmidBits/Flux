namespace Flux.Colors
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
  public struct Hsv
    : System.IEquatable<Hsv>
  {
    public static readonly Hsv Empty;
    public bool IsEmpty => Equals(Empty);

    [System.Runtime.InteropServices.FieldOffset(0)] private double m_hue;
    [System.Runtime.InteropServices.FieldOffset(8)] private double m_saturation;
    [System.Runtime.InteropServices.FieldOffset(16)] private double m_value;
    [System.Runtime.InteropServices.FieldOffset(24)] private double? m_alpha;

    public double Hue { get => m_hue; set => m_hue = value >= 0 && value <= 360 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    public double Saturation { get => m_saturation; set => m_saturation = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    public double Value { get => m_value; set => m_value = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    public double Alpha { get => m_alpha ?? 1; set => m_alpha = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

    public Hsv(double hue, double saturation, double value, double alpha)
    {
      m_hue = hue >= 0 && hue <= 360 ? hue : throw new System.ArgumentOutOfRangeException(nameof(hue));
      m_saturation = saturation >= 0 && saturation <= 1 ? saturation : throw new System.ArgumentOutOfRangeException(nameof(saturation));
      m_value = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value));
      m_alpha = alpha >= 0 && alpha <= 1 ? alpha : throw new System.ArgumentOutOfRangeException(nameof(saturation));
    }
    public Hsv(double hue, double saturation, double value)
      : this(hue, saturation, value, 1)
    { }

    public static Hsv FromRng(System.Random? rng)
    {
      rng ??= new System.Random();

      return new Hsv(rng.NextDouble(0, 360), rng.NextDouble(), rng.NextDouble(), rng.NextDouble());
    }

    public double GetChroma()
      => m_value * m_saturation;

    /// <summary>Creates an HSL color corresponding to the HSV instance.</summary>
    public Hsl ToHsl()
    {
      var h = Hue;
      var l = Value * (1 - Saturation / 2);
      var s = l == 0 || l == 1 ? 0 : (Value - l) / System.Math.Min(l, 1 - l);

      return new Hsl(h, s, l, Alpha);
    }
    /// <summary>Creates an RGB color corresponding to the HSV instance.</summary>
    public Rgb ToRgb()
    {
      double c = GetChroma();
      double h1 = Hue / 60;
      double x = c * (1 - System.Math.Abs((h1 % 2) - 1));

      double m = Value - c;
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
    public static bool operator ==(Hsv a, Hsv b)
      => a.Equals(b);
    public static bool operator !=(Hsv a, Hsv b)
      => !a.Equals(b);

    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Hsv other)
      => Hue == other.Hue && Saturation == other.Saturation && Value == other.Value && Alpha == other.Alpha;

    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is Hsv o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Hue, Saturation, Value, Alpha);
    public override string ToString()
      => $"<{GetType().Name}: {Hue}, {Saturation}, {Value}, {Alpha}>";
  }
}