//namespace Flux.Colors
//{
//  public readonly record struct Hsi
//  {
//    private readonly double m_hue;
//    private readonly double m_saturation;
//    private readonly double m_intensity;

//    public Hsi(double hue, double saturation, double intensity)
//    {
//      m_hue = hue >= 0 && hue <= 360 ? hue : throw new System.ArgumentOutOfRangeException(nameof(hue));
//      m_saturation = saturation >= 0 && saturation <= 1 ? saturation : throw new System.ArgumentOutOfRangeException(nameof(saturation));
//      m_intensity = intensity >= 0 && intensity <= 1 ? intensity : throw new System.ArgumentOutOfRangeException(nameof(intensity));
//    }

//    public double Hue { get => m_hue; init => m_hue = value; }
//    public double Saturation { get => m_saturation; init => m_saturation = value; }
//    public double Intensity { get => m_intensity; init => m_intensity = value; }

//    public double GetChroma() => 3 * m_intensity * m_saturation / (1 + (1 - double.Abs((m_hue / 60 % 2) - 1)));

//    /// <summary>Creates an RGB color corresponding to the HSI instance.</summary>
//    public Rgb ToRgb()
//    {
//      var c = GetChroma();
//      var h = m_hue / 60;
//      var x = c * (1 - double.Abs((h % 2) - 1));

//      var m = m_intensity * (1 - m_saturation);

//      var r = m;
//      var g = m;
//      var b = m;

//      switch (h)
//      {
//        case var v1 when v1 < 1:
//          r += c;
//          g += x;
//          break;
//        case var v2 when v2 < 2:
//          r += x;
//          g += c;
//          break;
//        case var v3 when v3 < 3:
//          g += c;
//          b += x;
//          break;
//        case var v4 when v4 < 4:
//          g += x;
//          b += c;
//          break;
//        case var v5 when v5 < 5:
//          r += x;
//          b += c;
//          break;
//        default: // h1 <= 6 //
//          r += c;
//          b += x;
//          break;
//      }

//      return new Rgb(
//        System.Convert.ToByte(255 * r),
//        System.Convert.ToByte(255 * g),
//        System.Convert.ToByte(255 * b)
//      );
//    }

//    public static Hsi FromRandom(System.Random? rng = null) { rng ??= System.Random.Shared; return new(rng.NextDouble() * 360, rng.NextDouble(), rng.NextDouble()); }

//    public override string ToString() => $"{GetType().Name} {{ {m_hue:N1}\u00B0, {m_saturation * 100:N1}%, {m_intensity * 100:N1}% }}";
//  }
//}
