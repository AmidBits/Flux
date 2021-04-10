namespace Flux.Colors
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
  public struct Cmyk
    : System.IEquatable<Cmyk>
  {
    public static readonly Cmyk Empty;
    public bool IsEmpty => Equals(Empty);

    [System.Runtime.InteropServices.FieldOffset(0)] private double m_cyan;
    [System.Runtime.InteropServices.FieldOffset(8)] private double m_magenta;
    [System.Runtime.InteropServices.FieldOffset(16)] private double m_yellow;
    [System.Runtime.InteropServices.FieldOffset(24)] private double m_black;
    [System.Runtime.InteropServices.FieldOffset(32)] private double? m_alpha;

    public double Cyan { get => m_cyan; set => m_cyan = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    public double Magenta { get => m_magenta; set => m_magenta = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    public double Yellow { get => m_yellow; set => m_yellow = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    public double Black { get => m_black; set => m_black = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    public double Alpha { get => m_alpha ?? 1; set => m_alpha = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

    public Cmyk(double cyan, double magenta, double yellow, double black, double alpha)
    {
      m_cyan = cyan >= 0 && cyan <= 1 ? cyan : throw new System.ArgumentOutOfRangeException(nameof(cyan));
      m_magenta = magenta >= 0 && magenta <= 1 ? magenta : throw new System.ArgumentOutOfRangeException(nameof(magenta));
      m_yellow = yellow >= 0 && yellow <= 1 ? yellow : throw new System.ArgumentOutOfRangeException(nameof(yellow));
      m_black = black >= 0 && black <= 1 ? black : throw new System.ArgumentOutOfRangeException(nameof(black));
      m_alpha = alpha >= 0 && alpha <= 1 ? alpha : throw new System.ArgumentOutOfRangeException(nameof(alpha));
    }
    public Cmyk(double cyan, double magenta, double yellow, double black)
      : this(cyan, magenta, yellow, black, 1)
    {
    }

    public static Cmyk Random(System.Random rng)
    {
      if (rng is null) throw new System.ArgumentNullException(nameof(rng));

      return new Cmyk(rng.NextDouble(), rng.NextDouble(), rng.NextDouble(), rng.NextDouble(), rng.NextDouble());
    }
    public static Cmyk Random()
      => Random(Flux.Random.NumberGenerator.Crypto);

    /// <summary>Creates an RGB color corresponding to the CMYK instance.</summary>
    public Rgb ToRgb()
    {
      var m1black = 1 - m_black;

      var red = System.Convert.ToByte((1 - System.Math.Min(1, m_cyan * m1black + m_black)) * 255);
      var green = System.Convert.ToByte((1 - System.Math.Min(1, m_magenta * m1black + m_black)) * 255);
      var blue = System.Convert.ToByte((1 - System.Math.Min(1, m_yellow * m1black + m_black)) * 255);
      var alpha = System.Convert.ToByte(Alpha * 255);

      return new Rgb(red, green, blue, alpha);
    }

    // Operators
    public static bool operator ==(Cmyk a, Cmyk b)
      => a.Equals(b);
    public static bool operator !=(Cmyk a, Cmyk b)
      => !a.Equals(b);

    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Cmyk other)
      => Cyan == other.Cyan && Magenta == other.Magenta && Yellow == other.Yellow && Black == other.Black && Alpha == other.Alpha;

    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is Cmyk o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Cyan, Magenta, Yellow, Black, Alpha);
    public override string ToString()
      => $"<{nameof(Cmyk)}: {Cyan}, {Magenta}, {Yellow}, {Black}, {Alpha}>";
  }
}
