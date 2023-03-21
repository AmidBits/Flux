namespace Flux.Colors
{
  /// <summary>Hsva is the same as Hsv with the addition of an alpha channel.</summary>
  public readonly record struct Ahsv
  {
    private readonly double m_alpha;
    private readonly Hsv m_hsv;

    public Ahsv(double alpha, Hsv hsv)
    {
      m_alpha = alpha >= 0 && alpha <= 1 ? alpha : throw new System.ArgumentOutOfRangeException(nameof(alpha));
      m_hsv = hsv;
    }
    public Ahsv(double alpha, double hue, double saturation, double value)
      : this(alpha, new Hsv(hue, saturation, value))
    { }

    public double Alpha { get => m_alpha; init => m_alpha = value; }
    public Hsv HSV { get => m_hsv; init => m_hsv = value; }

    /// <summary>Creates an HSL color corresponding to the HSV instance.</summary>
    public Ahsl ToAhsl()
      => new(m_alpha, HSV.ToHsl());
    /// <summary>Creates an RGB color corresponding to the HSV instance.</summary>
    public Argb ToArgb()
      => new(System.Convert.ToByte(m_alpha * 255), HSV.ToRgb());

    #region Static methods
    public static Ahsv FromRandom(System.Random rng)
      => rng is null
      ? throw new System.ArgumentNullException(nameof(rng))
      : new(rng.NextDouble(), Hsv.FromRandom(rng));
    #endregion Static methods

    public override string ToString()
      => $"{GetType().Name} {{ {(m_alpha * 100):N1}%, {HSV.Hue:N1}\u00B0, {HSV.Saturation * 100:N1}%, {HSV.Value * 100:N1}% }}";
  }
}
