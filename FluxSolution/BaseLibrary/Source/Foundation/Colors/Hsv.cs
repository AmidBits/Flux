namespace Flux.Colors
{
  public struct Hsv
    : System.IEquatable<Hsv>
  {
    public static readonly Hsv Empty;
    public bool IsEmpty => Equals(Empty);

    private double m_hue;
    private double m_saturation;
    private double m_value;

    public double Hue { get => m_hue; set => m_hue = value >= 0 && value <= 360 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    public double Saturation { get => m_saturation; set => m_saturation = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    public double Value { get => m_value; set => m_value = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

    public Hsv(double hue, double saturation, double value)
    {
      m_hue = hue >= 0 && hue <= 360 ? hue : throw new System.ArgumentOutOfRangeException(nameof(hue));
      m_saturation = saturation >= 0 && saturation <= 1 ? saturation : throw new System.ArgumentOutOfRangeException(nameof(saturation));
      m_value = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value));
    }

    public double GetChroma()
      => m_value * m_saturation;

    /// <summary>Creates an HSL color corresponding to the HSV instance.</summary>
    public Hsl ToHsl()
    {
      var lightness = m_value * (1 - m_saturation / 2);

      return new Hsl(m_hue, lightness == 0 || lightness == 1 ? 0 : (m_value - lightness) / System.Math.Min(lightness, 1 - lightness), lightness);
    }
    /// <summary>Converts to a Hwb color.</summary>
    public Hwb ToHwb()
      => new Hwb(m_hue, (1 - m_saturation) * m_value, 1 - m_value);
    /// <summary>Creates an RGB color corresponding to the HSV instance.</summary>
    public Rgb ToRgb()
    {
      double c = GetChroma();
      double h1 = m_hue / 60;
      double x = c * (1 - System.Math.Abs((h1 % 2) - 1));

      double m = m_value - c;
      double r1 = m, g1 = m, b1 = m;

      switch (h1)
      {
        case var h01 when h01 >= 0 && h01 <= 1:
          r1 += c;
          g1 += x;
          break;
        case var h11 when h11 > 1 && h11 <= 2:
          r1 += x;
          g1 += c;
          break;
        case var h23 when h23 > 2 && h23 <= 3:
          g1 += c;
          b1 += x;
          break;
        case var h34 when h34 > 3 && h34 <= 4:
          g1 += x;
          b1 += c;
          break;
        case var h45 when h45 > 4 && h45 <= 5:
          r1 += x;
          b1 += c;
          break;
        case var h56 when h56 > 5 && h56 <= 6:
          r1 += c;
          b1 += x;
          break;
        default:
          break;
      }

      return new Rgb(
        System.Convert.ToByte(255 * r1),
        System.Convert.ToByte(255 * g1),
        System.Convert.ToByte(255 * b1)
       );
    }

    #region Static methods
    public static Hsv Random(System.Random rng)
    {
      if (rng is null) throw new System.ArgumentNullException(nameof(rng));

      return new Hsv(rng.NextDouble(0, 360), rng.NextDouble(), rng.NextDouble());
    }
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(Hsv a, Hsv b)
      => a.Equals(b);
    public static bool operator !=(Hsv a, Hsv b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Hsv other)
      => Hue == other.Hue && Saturation == other.Saturation && Value == other.Value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Hsv o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_hue, m_saturation, m_value);
    public override string ToString()
      => $"<{GetType().Name}: {m_hue:N1}\u00B0, {(m_saturation * 100):N1}%, {(m_value * 100):N1}%>";
    #endregion Object overrides
  }
}
