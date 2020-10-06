namespace Flux.Media.Coloring
{
  public struct Hsv
    : System.IEquatable<Hsv>, System.IFormattable
  {
    #region Properties
    private double? m_alpha;
    public double Alpha { get => m_alpha ?? 1; set => m_alpha = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

    private double m_hue;
    public double Hue { get => m_hue; set => m_hue = value >= 0 && value <= 360 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

    private double m_saturation;
    public double Saturation { get => m_saturation; set => m_saturation = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

    private double m_value;
    public double Value { get => m_value; set => m_value = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    #endregion Properties

    public Hsv(double alpha, double hue, double saturation, double value)
    {
      m_alpha = alpha >= 0 && alpha <= 1 ? alpha : throw new System.ArgumentOutOfRangeException(nameof(saturation));
      m_hue = hue >= 0 && hue <= 360 ? hue : throw new System.ArgumentOutOfRangeException(nameof(hue));
      m_saturation = saturation >= 0 && saturation <= 1 ? saturation : throw new System.ArgumentOutOfRangeException(nameof(saturation));
      m_value = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value));
    }

    public double GetChroma()
      => m_value * m_saturation;

    /// <summary>Returns an Rgb struct based on Hsv model.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/HSL_and_HSV#From_HSV"/>
    /// <returns>An Rgb object</returns>
    public Rgb ToRgb()
    {
      double chroma = Value * Saturation;
      double h1 = Hue / 60;
      double x = chroma * (1 - System.Math.Abs((h1 % 2) - 1));

      double m = Value - chroma;
      double r1 = m, g1 = m, b1 = m;

      switch (h1)
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
    public static bool operator ==(Hsv a, Hsv b)
      => a.Equals(b);
    public static bool operator !=(Hsv a, Hsv b)
      => !a.Equals(b);

    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Hsv other)
      => Alpha == other.Alpha && Hue == other.Hue && Saturation == other.Saturation && Value == other.Value;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => $"<{Alpha}, {Hue}, {Saturation}, {Value}>";

    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is Hsv o && Equals(o);
    public override int GetHashCode()
      => System.Linq.Enumerable.Empty<object>().Append(Alpha, Hue, Saturation, Value).CombineHashDefault();
    public override string ToString()
      => ToString(default, System.Globalization.CultureInfo.CurrentCulture);
  }
}
