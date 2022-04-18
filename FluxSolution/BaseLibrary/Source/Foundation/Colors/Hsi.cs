namespace Flux.Colors
{
  public struct Hsi
    : System.IEquatable<Hsi>
  {
    public static readonly Hsi Empty;

    private double m_hue;
    private double m_saturation;
    private double m_intensity;

    public Hsi(double hue, double saturation, double intensity)
    {
      m_hue = hue >= 0 && hue <= 360 ? hue : throw new System.ArgumentOutOfRangeException(nameof(hue));
      m_saturation = saturation >= 0 && saturation <= 1 ? saturation : throw new System.ArgumentOutOfRangeException(nameof(saturation));
      m_intensity = intensity >= 0 && intensity <= 1 ? intensity : throw new System.ArgumentOutOfRangeException(nameof(intensity));
    }

    public double Hue
      => m_hue;
    public double Saturation
      => m_saturation;
    public double Intensity
      => m_intensity;

    public double GetChroma()
      => 3 * m_intensity * m_saturation / (1 + (1 - System.Math.Abs((m_hue / 60 % 2) - 1)));

    /// <summary>Creates an RGB color corresponding to the HSI instance.</summary>
    public Rgb ToRgb()
    {
      var c = GetChroma();
      var h = m_hue / 60;
      var x = c * (1 - System.Math.Abs((h % 2) - 1));

      var m = m_intensity * (1 - m_saturation);

      var r = m;
      var g = m;
      var b = m;

      switch (h)
      {
        case var v1 when v1 < 1:
          r += c;
          g += x;
          break;
        case var v2 when v2 < 2:
          r += x;
          g += c;
          break;
        case var v3 when v3 < 3:
          g += c;
          b += x;
          break;
        case var v4 when v4 < 4:
          g += x;
          b += c;
          break;
        case var v5 when v5 < 5:
          r += x;
          b += c;
          break;
        default: // h1 <= 6 //
          r += c;
          b += x;
          break;
      }

      return new Rgb(
        System.Convert.ToByte(255 * r),
        System.Convert.ToByte(255 * g),
        System.Convert.ToByte(255 * b)
      );
    }

    #region Static methods
    public static Hsi FromRandom(System.Random rng)
    {
      if (rng is null) throw new System.ArgumentNullException(nameof(rng));

      return new Hsi(rng.NextDouble(0, 360), rng.NextDouble(), rng.NextDouble());
    }
    public static Hsi FromRandom()
      => FromRandom(Randomization.NumberGenerator.Crypto);
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(Hsi a, Hsi b)
      => a.Equals(b);
    public static bool operator !=(Hsi a, Hsi b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Hsi other)
      => m_hue == other.m_hue && m_saturation == other.m_saturation && m_intensity == other.m_intensity;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Hsi o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_hue, m_saturation, m_intensity);
    public override string ToString()
      => $"{GetType().Name} {{ {m_hue:N1}\u00B0, {m_saturation * 100:N1}%, {m_intensity * 100:N1}% }}";
    #endregion Object overrides
  }
}
