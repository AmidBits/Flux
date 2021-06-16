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

    public double Hue { get => m_hue; set => m_hue = value >= 0 && value <= 360 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    public double Saturation { get => m_saturation; set => m_saturation = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    public double Lightness { get => m_lightness; set => m_lightness = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

    public Hsl(double hue, double saturation, double lightness)
    {
      m_hue = hue >= 0 && hue <= 360 ? hue : throw new System.ArgumentOutOfRangeException(nameof(hue));
      m_saturation = saturation >= 0 && saturation <= 1 ? saturation : throw new System.ArgumentOutOfRangeException(nameof(saturation));
      m_lightness = lightness >= 0 && lightness <= 1 ? lightness : throw new System.ArgumentOutOfRangeException(nameof(lightness));
    }

    public double GetChroma()
      => (1 - System.Math.Abs(2 * m_lightness - 1)) * m_saturation;

    /// <summary>Creates an HSV color corresponding to the HSL instance.</summary>
    public Hsv ToHsv()
    {
      var v = m_lightness + m_saturation * System.Math.Min(m_lightness, 1 - m_lightness);

      return new Hsv(m_hue, v == 0 ? 0 : 2 * (1 - m_lightness / v), v);
    }
    /// <summary>Creates an RGB color corresponding to the HSL instance.</summary>
    public Rgb ToRgb()
    {
      double c = GetChroma();
      double h1 = m_hue / 60;
      double x = c * (1 - System.Math.Abs((h1 % 2) - 1));

      double m = m_lightness - (0.5 * c);
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
        System.Convert.ToByte(255 * b1)
       );
    }

    public string ToStringHtmlHsl()
      => $"hsl({Hue}, {Saturation}%, {Lightness}%)";

    #region Static methods
    public static Hsl Random(System.Random rng)
    {
      if (rng is null) throw new System.ArgumentNullException(nameof(rng));

      return new Hsl(rng.NextDouble(0, 360), rng.NextDouble(), rng.NextDouble());
    }
    public static Hsl Random()
      => Random(Flux.Random.NumberGenerator.Crypto);
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(Hsl a, Hsl b)
      => a.Equals(b);
    public static bool operator !=(Hsl a, Hsl b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interface
    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Hsl other)
      => Hue == other.Hue && Saturation == other.Saturation && Lightness == other.Lightness;
    #endregion Implemented interface

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Hsl o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Hue, Saturation, Lightness);
    public override string ToString()
      => $"<{GetType().Name}: {m_hue:N1}\u00B0, {(m_saturation * 100):N1}%, {(m_lightness * 100):N1}%>";
    #endregion Object overrides
  }
}
