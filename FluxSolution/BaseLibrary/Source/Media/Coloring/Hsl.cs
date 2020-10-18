namespace Flux.Coloring
{
  public struct Hsl
    : System.IEquatable<Hsl>, System.IFormattable
  {
    public static readonly Hsl Empty;
    public bool IsEmpty => Equals(Empty);

    #region Properties
    private double? m_alpha;
    public double Alpha { get => m_alpha ?? 1; set => m_alpha = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

    private double m_hue;
    public double Hue { get => m_hue; set => m_hue = value >= 0 && value <= 360 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

    private double m_saturation;
    public double Saturation { get => m_saturation; set => m_saturation = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

    private double m_lightness;
    public double Lightness { get => m_lightness; set => m_lightness = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    #endregion Properties

    public Hsl(double alpha, double hue, double saturation, double lightness)
    {
      m_alpha = alpha >= 0 && alpha <= 1 ? alpha : throw new System.ArgumentOutOfRangeException(nameof(saturation));
      m_hue = hue >= 0 && hue <= 360 ? hue : throw new System.ArgumentOutOfRangeException(nameof(hue));
      m_saturation = saturation >= 0 && saturation <= 1 ? saturation : throw new System.ArgumentOutOfRangeException(nameof(saturation));
      m_lightness = lightness >= 0 && lightness <= 1 ? lightness : throw new System.ArgumentOutOfRangeException(nameof(lightness));
    }

    public double GetChroma()
      => (1 - System.Math.Abs((2 * m_lightness) - 1)) * m_saturation;

    /// <summary>Returns an Rgb struct based on Hsl model.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/HSL_and_HSV#From_HSL"/>
    /// <returns>An RGB object</returns>
    public Rgb ToRgb()
    {
      double chroma = (1 - System.Math.Abs((2 * Lightness) - 1)) * Saturation;
      double hue1 = Hue / 60;
      double x = chroma * (1 - System.Math.Abs((hue1 % 2) - 1));

      double m = Lightness - (0.5 * chroma);
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

      return new Rgb((byte)(255 * Alpha), (byte)(255 * r1), (byte)(255 * g1), (byte)(255 * b1));
    }

    // Operators
    public static bool operator ==(Hsl a, Hsl b)
      => a.Equals(b);
    public static bool operator !=(Hsl a, Hsl b)
      => !a.Equals(b);

    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Hsl other)
      => Alpha == other.Alpha && Hue == other.Hue && Saturation == other.Saturation && Lightness == other.Lightness;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => $"<{Alpha}, {Hue}, {Saturation}, {Lightness}>";

    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is Hsl o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Alpha, Hue, Saturation, Lightness);
    public override string ToString()
      => ToString(default, System.Globalization.CultureInfo.CurrentCulture);
  }
}
