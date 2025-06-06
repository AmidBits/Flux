namespace Flux.Units
{
  /// <summary>A ratio, using real numbers is an irrational number, indicates how many times one number contains another. It is two related quantities measured with the same unit, it is a dimensionless number (value). This struct stores both constituting numbers of the ratio (numerator and denominator) and returns the quotient as a value.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Ratio"/>
  public readonly record struct Ratio
    : IValueQuantifiable<double>
  {
    private readonly double m_numerator;
    private readonly double m_denominator;

    public Ratio(double numerator, double denominator)
    {
      m_numerator = numerator;
      m_denominator = denominator;
    }
    /// <summary>Use a pre-computed ratio. This is less accurate because losses may already have been accumulated, and the ratio is no longer the original numerator and denominator.</summary>
    public Ratio(double ratio) : this(ratio, 1) { }

    public void Deconstruct(out double numerator, out double denominator) { numerator = m_numerator; denominator = m_denominator; }

    public double Numerator => m_numerator;
    public double Denominator => m_denominator;

    ///// <summary>If a diagonal length is known, the proportional width and height can be computed using the Pythagorean theorem.</summary>
    ///// <param name="diagonalLength">The length of the known diagonal.</param>
    ///// <returns>The proportional (to the arguments passed) lengths of width and height.</returns>
    //public Flux.Numerics.CartesianCoordinate2<double> ToSize(double diagonalLength) => (Numerics.CartesianCoordinate2<double>)ToSize(diagonalLength, m_numerator, m_denominator);

    /// <summary>
    /// <para>Convert the ratio to a fraction. If numerator and/or denominator are not integers, the fraction is approximated.</para>
    /// </summary>
    public Numerics.BigRational ToFraction()
      => double.IsInteger(m_numerator) && double.IsInteger(m_denominator)
      ? new(System.Numerics.BigInteger.CreateChecked(m_numerator), System.Numerics.BigInteger.CreateChecked(m_denominator))
      : Numerics.BigRational.ApproximateRational(Value);

    public string ToRatioNotationString(string? format, System.IFormatProvider? formatProvider = null, RatioNotation ratioNotation = RatioNotation.AcolonB)
      => ToRatioNotationString(ratioNotation, m_numerator.ToString(format, formatProvider), m_denominator.ToString(format, formatProvider));

    #region Static methods

    public static string ToRatioNotationString(RatioNotation ratioNotation, string left, string right)
      => ratioNotation switch
      {
        RatioNotation.AcolonB => $"{left}\u2236{right}", // As a ratio (colon).
        RatioNotation.AperB => $"{left} per {right}", // As a rate ("A per B")
        RatioNotation.AslashB => $"{left}\u2044{right}", // With a ratio slash.
        RatioNotation.AtoB => $"{left} to {right}", // As textual "A to B".
        _ => throw new System.ArgumentOutOfRangeException(nameof(ratioNotation))
      };

    /// <summary>
    /// <para>When the diagonal length and side-to-side ratio is known, the proportional width and height can be computed using the Pythagorean theorem. E.g. A diagonal of 65" and a ratio of 16:9.</para>
    /// </summary>
    /// <param name="diagonalLength">The length of the known diagonal. E.g. 65.</param>
    /// <param name="ratioX">The x-axis portion of a ratio, which corresponds to width in the result. E.g. 16.</param>
    /// <param name="ratioY">The y-axis portion of a ratio, which corresponds to height in the result. E.g. 9.</param>
    /// <returns>The proportional (to the arguments passed) lengths of width (e.g. 56.6524099130957) and height (e.g. 31.866980576116333).</returns>
    public static (double width, double height) ToSize(double diagonalLength, double ratioX, double ratioY)
    {
      var m = double.Sqrt(ratioX * ratioX + ratioY * ratioY);

      return new(diagonalLength * ratioX / m, diagonalLength * ratioY / m);
    }

    /// <summary>
    /// <para>When the diagonal length and side-to-side ratio is known, the proportional width and height can be computed using the Pythagorean theorem. E.g. A diagonal of 65" and a ratio of 16:9.</para>
    /// </summary>
    /// <param name="diagonalLength">The length of the known diagonal. E.g. 65.</param>
    /// <param name="ratio">The pre-computed ratio, e.g. if the two parts of a ratio is unknown, which then corresponds to width AND height in the result. E.g. 1.777777777777778 (16/9).</param>
    /// <returns>The proportional (to the arguments passed) lengths of width and height.</returns>
    public static (double width, double height) ToSize(double diagonalLength, double ratio) => ToSize(diagonalLength, ratio, 1);

    #endregion // Static methods

    #region Overloaded operators

    #endregion Overloaded operators

    #region Implemented interfaces

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToRatioNotationString(format, formatProvider);

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The <see cref="Radio.Value"/> property is the ratio between <see cref="Numerator"/> and <see cref="Denominator"/>, i.e. <see cref="Numerator"/>/<see cref="Denominator"/>.</para>
    /// </summary>
    public double Value => m_numerator / m_denominator;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
