namespace Flux.Colors
{
  /// <summary>CMYK refers to the four ink plates used in some color printing: Cyan, Magenta, Yellow, and Key (black).</summary>
  /// <see cref="https://en.wikipedia.org/wiki/CMYK_color_model"/>
  public readonly record struct Cmyk
  {
    private readonly double m_cyan;
    private readonly double m_magenta;
    private readonly double m_yellow;
    private readonly double m_key;

    public Cmyk(double cyan, double magenta, double yellow, double key)
    {
      m_cyan = cyan >= 0 && cyan <= 1 ? cyan : throw new System.ArgumentOutOfRangeException(nameof(cyan));
      m_magenta = magenta >= 0 && magenta <= 1 ? magenta : throw new System.ArgumentOutOfRangeException(nameof(magenta));
      m_yellow = yellow >= 0 && yellow <= 1 ? yellow : throw new System.ArgumentOutOfRangeException(nameof(yellow));
      m_key = key >= 0 && key <= 1 ? key : throw new System.ArgumentOutOfRangeException(nameof(key));
    }

    public double Cyan { get => m_cyan; init => m_cyan = value; }
    public double Magenta { get => m_magenta; init => m_magenta = value; }
    public double Yellow { get => m_yellow; init => m_yellow = value; }
    public double Key { get => m_key; init => m_key = value; }

    /// <summary>Creates an RGB color corresponding to the CMYK instance.</summary>
    public Rgb ToRgb()
    {
      var m1key = 1 - m_key;

      var red = System.Convert.ToByte(255 * (1 - m_cyan) * m1key);
      var green = System.Convert.ToByte(255 * (1 - m_magenta) * m1key);
      var blue = System.Convert.ToByte(255 * (1 - m_yellow) * m1key);

      return new Rgb(red, green, blue);
    }

    public static Cmyk FromRandom(System.Random? rng = null) { rng ??= new System.Random(); return new(rng.NextDouble(), rng.NextDouble(), rng.NextDouble(), rng.NextDouble()); }

    public override string ToString() => $"{GetType().Name} {{ {m_cyan * 360:N1}\u00B0, {m_magenta * 360:N1}\u00B0, {m_yellow * 360:N1}\u00B0, {m_key * 360:N1}\u00B0 }}";
  }
}
