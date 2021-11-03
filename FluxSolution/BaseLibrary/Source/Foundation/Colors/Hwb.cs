namespace Flux.Colors
{
  public struct Hwb
    : System.IEquatable<Hwb>
  {
    public static readonly Hwb Empty;

    private double m_hue;
    private double m_white;
    private double m_black;

    public Hwb(double hue, double white, double black)
    {
      m_hue = hue >= 0 && hue <= 360 ? hue : throw new System.ArgumentOutOfRangeException(nameof(hue));
      m_white = white >= 0 && white <= 1 ? white : throw new System.ArgumentOutOfRangeException(nameof(white));
      m_black = black >= 0 && black <= 1 ? black : throw new System.ArgumentOutOfRangeException(nameof(black));
    }

    public double Hue { get => m_hue; set => m_hue = value >= 0 && value <= 360 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    public double White { get => m_white; set => m_white = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    public double Black { get => m_black; set => m_black = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

    public double GetChroma()
      => 3 * m_black * m_white / (1 + (1 - System.Math.Abs((m_hue / 60 % 2) - 1)));

    /// <summary>Converts the Hwb to a corresponding HSV color.</summary>
    public Hsv ToHsv()
    {
      var v = 1 - m_black;

      return new Hsv(m_hue, v > 0 ? System.Math.Max(1 - (m_white / v), 0) : 0, v);
    }
    /// <summary>Converts the Hwb to a corresponding RGB color.</summary>
    public Rgb ToRgb()
    {
      var h1 = m_hue / 360;
      var wh = m_white;
      var bl = m_black;

      if (wh + bl is var ratio && ratio > 1)
      {
        wh /= ratio;
        bl /= ratio;
      }

      var whole = (int)(h1 * 6);
      var fraction = (h1 * 6) - whole;

      if ((whole & 1) == 1)
        fraction = 1 - fraction;

      var v = 1 - bl; // Unit inverse.

      var n = wh + fraction * (v - wh);

      double r, g, b;

      switch (whole)
      {
        default:
        case 6:
        case 0: r = v; g = n; b = wh; break;
        case 1: r = n; g = v; b = wh; break;
        case 2: r = wh; g = v; b = n; break;
        case 3: r = wh; g = n; b = v; break;
        case 4: r = n; g = wh; b = v; break;
        case 5: r = v; g = wh; b = n; break;
      }

      return new Rgb(
        System.Convert.ToByte(r * 255),
        System.Convert.ToByte(g * 255),
        System.Convert.ToByte(b * 255)
      );
    }

    #region Static member
    public static Hwb FromRandom(System.Random rng)
    {
      if (rng is null) throw new System.ArgumentNullException(nameof(rng));

      return new Hwb(rng.NextDouble(0, 360), rng.NextDouble(), rng.NextDouble());
    }
    public static Hwb FromRandom()
      => FromRandom(Randomization.NumberGenerator.Crypto);
    #endregion Static member

    #region Overloaded operators
    public static bool operator ==(Hwb a, Hwb b)
      => a.Equals(b);
    public static bool operator !=(Hwb a, Hwb b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Hwb other)
      => Hue == other.Hue && White == other.White && Black == other.Black;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Hwb o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Hue, White, Black);
    public override string ToString()
      => $"<{GetType().Name}: {m_hue:N1}\u00B0, {(m_white * 100):N1}%, {(m_black * 100):N1}%>";
    #endregion Object overrides
  }
}
