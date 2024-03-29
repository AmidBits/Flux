namespace Flux.Colors
{
  public readonly record struct Hsl
    : IHtmlColorFormattable
  {
    private readonly double m_hue;
    private readonly double m_saturation;
    private readonly double m_lightness;

    public Hsl(double hue, double saturation, double lightness)
    {
      m_hue = hue >= 0 && hue <= 360 ? hue : throw new System.ArgumentOutOfRangeException(nameof(hue));
      m_saturation = saturation >= 0 && saturation <= 1 ? saturation : throw new System.ArgumentOutOfRangeException(nameof(saturation));
      m_lightness = lightness >= 0 && lightness <= 1 ? lightness : throw new System.ArgumentOutOfRangeException(nameof(lightness));
    }

    public double Hue { get => m_hue; init => m_hue = value; }
    public double Saturation { get => m_saturation; init => m_saturation = value; }
    public double Lightness { get => m_lightness; init => m_lightness = value; }

    public double GetChroma() => (1 - System.Math.Abs(2 * m_lightness - 1)) * m_saturation;

    /// <summary>Creates an HSV color corresponding to the HSL instance.</summary>
    public Hsv ToHsv()
    {
      var value = m_lightness + m_saturation * System.Math.Min(m_lightness, 1 - m_lightness);

      return new Hsv(m_hue, value == 0 ? 0 : 2 * (1 - m_lightness / value), value);
    }

    /// <summary>Creates an RGB color corresponding to the HSL instance.</summary>
    public Rgb ToRgb()
    {
      var c = GetChroma();
      var h = m_hue / 60;
      var x = c * (1 - System.Math.Abs((h % 2) - 1));

      var m = m_lightness - (0.5 * c);

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

    public string ToHtmlColorString() => $"hsl({Hue}, {Saturation}%, {Lightness}%)";

    public static Hsl FromRandom(System.Random? rng = null) { rng ??= System.Random.Shared; return new(rng.NextDouble() * 360, rng.NextDouble(), rng.NextDouble()); }

    public override string ToString() => $"{GetType().Name} {{ {m_hue:N1}\u00B0, {(m_saturation * 100):N1}%, {(m_lightness * 100):N1}% }}";
  }
}
