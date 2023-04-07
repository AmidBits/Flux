namespace Flux.Colors
{
  public readonly record struct Ahwb
  {
    private readonly double m_alpha;
    private readonly Hwb m_hwb;

    public Ahwb(double alpha, Hwb hwb)
    {
      m_alpha = alpha >= 0 && alpha <= 1 ? alpha : throw new System.ArgumentOutOfRangeException(nameof(alpha));
      m_hwb = hwb;
    }
    public Ahwb(double alpha, double hue, double white, double black) : this(alpha, new Hwb(hue, white, black)) { }

    public double Alpha { get => m_alpha; init => m_alpha = value; }
    public Hwb HWB { get => m_hwb; init => m_hwb = value; }

    /// <summary>Converts the Hwb to a corresponding HSV color.</summary>
    public Ahsv ToAhsv() => new(m_alpha, HWB.ToHsv());

    /// <summary>Converts the Hwb to a corresponding RGB color.</summary>
    public Argb ToArgb() => new(System.Convert.ToByte(m_alpha * 255), HWB.ToRgb());

    public static Ahwb FromRandom(System.Random? rng = null) { rng ??= new System.Random(); return new(rng.NextDouble(), Hwb.FromRandom(rng)); }

    public override string ToString() => $"{GetType().Name} {{ {(m_alpha * 100):N1}%, {HWB.Hue:N1}\u00B0, {HWB.White * 100:N1}%, {HWB.Black * 100:N1}% }}";
  }
}
