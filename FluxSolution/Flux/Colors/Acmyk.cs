//namespace Flux.Colors
//{
//  /// <summary>Cmyka is the same as Cmyk with the addition of an alpha channel.</summary>
//  public readonly record struct Acmyk
//  {
//    private readonly double m_alpha;
//    private readonly Cmyk m_cmyk;

//    public Acmyk(double alpha, Cmyk cmyk)
//    {
//      m_alpha = alpha >= 0 && alpha <= 1 ? alpha : throw new System.ArgumentOutOfRangeException(nameof(alpha));
//      m_cmyk = cmyk;
//    }
//    public Acmyk(double alpha, double cyan, double magenta, double yellow, double black)
//      : this(alpha, new Cmyk(cyan, magenta, yellow, black))
//    { }

//    public double Alpha { get => m_alpha; init => m_alpha = value; }
//    public Cmyk CMYK { get => m_cmyk; init => m_cmyk = value; }

//    /// <summary>Creates an RGB color corresponding to the CMYK instance.</summary>
//    public Argb ToArgb() => new(System.Convert.ToByte(m_alpha * 255), CMYK.ToRgb());

//    public static Acmyk FromRandom(System.Random? rng = null) { rng ??= System.Random.Shared; return new(rng.NextDouble(), Cmyk.FromRandom(rng)); }

//    #region Overloaded operators

//    public static explicit operator Acmyk(System.ValueTuple<double, double, double, double, double> v) => new(v.Item1, v.Item2, v.Item3, v.Item4, v.Item5);
//    public static explicit operator Acmyk(double[] v) => new(v[0], v[1], v[2], v[3], v[4]);

//    #endregion Overloaded operators

//    public override string ToString()
//      => $"{GetType().Name} {{ {(m_alpha * 100):N1}%, {CMYK.Cyan * 360:N1}\u00B0, {CMYK.Magenta * 360:N1}\u00B0, {CMYK.Yellow * 360:N1}\u00B0, {CMYK.Key * 360:N1}\u00B0 }}";
//  }
//}
