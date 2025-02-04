namespace Flux.Colors
{
  /// <summary>Hsia is the same as Hsi with the addition of an alpha channel.</summary>
  public readonly record struct Ahsi
  {
    private readonly double m_alpha;
    private readonly Hsi m_hsi;

    public Ahsi(double alpha, Hsi hsi)
    {
      m_alpha = alpha >= 0 && alpha <= 1 ? alpha : throw new System.ArgumentOutOfRangeException(nameof(alpha));
      m_hsi = hsi;
    }
    public Ahsi(double alpha, double hue, double saturation, double intensity) : this(alpha, new Hsi(hue, saturation, intensity)) { }

    public double Alpha { get => m_alpha; init => m_alpha = value; }
    public Hsi HSI { get => m_hsi; init => m_hsi = value; }

    /// <summary>Creates an RGB color corresponding to the HSI instance.</summary>
    public Argb ToArgb() => new(System.Convert.ToByte(m_alpha * 255), HSI.ToRgb());

    public static Ahsi FromRandom(System.Random? rng = null) { rng ??= System.Random.Shared; return new Ahsi(rng.NextDouble(), Hsi.FromRandom(rng)); }

    public override string ToString() => $"{GetType().Name} {{ {(m_alpha * 100):N1}%, {HSI.Hue:N1}\u00B0, {HSI.Saturation * 100:N1}%, {HSI.Intensity * 100:N1}% }}";
  }
}
