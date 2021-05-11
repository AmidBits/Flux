namespace Flux.Colors
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
  public struct Hwb
    : System.IEquatable<Hwb>
  {
    public static readonly Hwb Empty;
    public bool IsEmpty => Equals(Empty);

    [System.Runtime.InteropServices.FieldOffset(0)] private double m_hue;
    [System.Runtime.InteropServices.FieldOffset(8)] private double m_white;
    [System.Runtime.InteropServices.FieldOffset(16)] private double m_black;

    public double Hue { get => m_hue; set => m_hue = value >= 0 && value <= 360 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    public double White { get => m_white; set => m_white = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    public double Black { get => m_black; set => m_black = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

    public Hwb(double hue, double white, double black)
    {
      m_hue = hue >= 0 && hue <= 6 ? hue : throw new System.ArgumentOutOfRangeException(nameof(hue));
      m_white = white >= 0 && white <= 1 ? white : throw new System.ArgumentOutOfRangeException(nameof(white));
      m_black = black >= 0 && black <= 1 ? black : throw new System.ArgumentOutOfRangeException(nameof(black));
    }

    public static Hwb Random(System.Random rng)
    {
      if (rng is null) throw new System.ArgumentNullException(nameof(rng));

      return new Hwb(rng.NextDouble(0, 360), rng.NextDouble(), rng.NextDouble());
    }
    public static Hwb Random()
      => Random(Flux.Random.NumberGenerator.Crypto);

    public double GetChroma()
      => 3 * m_black * m_white / (1 + (1 - System.Math.Abs((m_hue / 60 % 2) - 1)));

    /// <summary>Converts to a corresponding HSV color.</summary>
    public Hsv ToHsv()
    {
      var v = 1 - m_black;

      return new Hsv(m_hue, 1 - (m_white / v), v);
    }
    /// <summary>Creates an RGB color corresponding to the HWB instance.</summary>
    public Rgb ToRgb()
    {
      //double hue = m_hue;
      //double white = m_white;
      //double black = m_black;

      //var v = 1 - black;

      //if (hue == -1 && System.Convert.ToByte(v * 255) is var vb)
      //  return new Rgb(vb, vb, vb);

      //var i = (int)System.Math.Floor(hue);
      //var f = hue - i;

      //if ((i & 1) == 1)
      //  f = 1 - f;

      //var n = white + f * (v - white);

      //double r, g, b;

      //switch (i)
      //{
      //  default:
      //  case 6:
      //  case 0: r = v; g = n; b = white; break;
      //  case 1: r = n; g = v; b = white; break;
      //  case 2: r = white; g = v; b = n; break;
      //  case 3: r = white; g = n; b = v; break;
      //  case 4: r = n; g = white; b = v; break;
      //  case 5: r = v; g = white; b = n; break;
      //}

      //return new Rgb(
      //  System.Convert.ToByte(255 * r),
      //  System.Convert.ToByte(255 * g),
      //  System.Convert.ToByte(255 * b),
      //  System.Convert.ToByte(255 * Alpha)
      //);

      var h = m_hue;// / 360;
      var wh = m_white;// / 100;
      var bl = m_black;// / 100;

      var ratio = wh + bl;

      double v, f, n;
      if (ratio > 1)
      {
        wh /= ratio;
        bl /= ratio;
      }
      var i = (int)System.Math.Floor(6 * h);
      v = 1 - bl;
      f = 6 * h - i;

      if ((i & 1) == 1)
        f = 1 - f;

      n = wh + f * (v - wh);

      double r, g, b;

      switch (i)
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
        System.Convert.ToByte(255 * r),
        System.Convert.ToByte(255 * g),
        System.Convert.ToByte(255 * b)
      );
    }

    // Operators
    public static bool operator ==(Hwb a, Hwb b)
      => a.Equals(b);
    public static bool operator !=(Hwb a, Hwb b)
      => !a.Equals(b);

    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Hwb other)
      => Hue == other.Hue && White == other.White && Black == other.Black;

    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is Hwb o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Hue, White, Black);
    public override string ToString()
      => $"<{GetType().Name}: {Hue}, {White}, {Black}>";
  }
}
