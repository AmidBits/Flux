namespace Flux.Colors
{
  /// <summary>Hsla is the same as Hsl with the addition of an alpha channel.</summary>
  public readonly record struct Ahsl
    : IHtmlColorFormattable
  {
    private readonly double m_alpha;
    private readonly Hsl m_hsl;

    public Ahsl(double alpha, Hsl hsl)
    {
      m_alpha = alpha >= 0 && alpha <= 1 ? alpha : throw new System.ArgumentOutOfRangeException(nameof(alpha));
      m_hsl = hsl;
    }
    public Ahsl(double alpha, double hue, double saturation, double lightness) : this(alpha, new Hsl(hue, saturation, lightness)) { }

    public double Alpha { get => m_alpha; init => m_alpha = value; }
    public Hsl HSL { get => m_hsl; init => m_hsl = value; }

    /// <summary>Creates an HSVA color corresponding to the HSL instance.</summary>
    public Ahsv ToAhsv() => new(m_alpha, HSL.ToHsv());

    /// <summary>Creates an RGBA color corresponding to the HSL instance.</summary>
    public Argb ToArgb() => new(System.Convert.ToByte(255 * m_alpha), HSL.ToRgb());

    public string ToHtmlColorString() => $"hsla({HSL.Hue}, {HSL.Saturation}%, {HSL.Lightness}%, {m_alpha})";

    public static Ahsl FromRandom(System.Random? rng = null) { rng ??= new System.Random(); return new(rng.NextDouble(), Hsl.FromRandom(rng)); }

    public override string ToString() => $"{GetType().Name} {{ {(m_alpha * 100):N1}%, {HSL.Hue:N1}\u00B0, {HSL.Saturation * 100:N1}%, {HSL.Lightness * 100:N1}% }}";
  }
}
