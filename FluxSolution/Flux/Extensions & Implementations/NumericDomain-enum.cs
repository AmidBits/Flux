namespace Flux
{
  /// <summary>
  /// <para>Represents the domain of a numeric type.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Number#Classification"/></para>
  /// </summary>
  public enum NumericDomain
  {
    Unknown = '?',
    /// <summary>
    /// <para>Represents the domain of natural numbers (non-negative or positive whole numbers, depending on convention).</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Natural_number"/></para>
    /// </summary>
    /// <remarks>"DOUBLE-STRUCK CAPITAL N"</remarks>
    NaturalNumbers = '\u2115',
    /// <summary>
    /// <para>Represents the domain of integers (includes negative whole numbers).</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Integer"/></para>
    /// </summary>
    /// <remarks>"DOUBLE-STRUCK CAPITAL Z"</remarks>
    Integers = '\u2124',
    /// <summary>
    /// <para>Represents the domain of rational numbers (fractions).</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Rational_number"/></para>
    /// </summary>
    /// <remarks>"DOUBLE-STRUCK CAPITAL Q"</remarks>
    RationalNumbers = '\u211A',
    /// <summary>
    /// <para>Represents the domain of real numbers (floating point numbers).</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Real_number"/></para>
    /// </summary>
    /// <remarks>"DOUBLE-STRUCK CAPITAL R"</remarks>
    RealNumbers = '\u211D',
    /// <summary>
    /// <para>Represents the domain of complex numbers.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Complex_number"/></para>
    /// </summary>
    /// <remarks>"DOUBLE-STRUCK CAPITAL C"</remarks>
    ComplexNumbers = '\u210C',
  }

  public static partial class NumberBase
  {
    extension<TNumberBase>(TNumberBase)
      where TNumberBase : System.Numerics.INumberBase<TNumberBase>
    {
      public static NumericDomain GetNumericDomain()
        => NumericTypeInfo<TNumberBase>.IsUnsignedInteger ? NumericDomain.NaturalNumbers
        : NumericTypeInfo<TNumberBase>.IsSignedInteger ? NumericDomain.Integers
        : NumericTypeInfo<TNumberBase>.IsBigRational ? NumericDomain.RationalNumbers
        : NumericTypeInfo<TNumberBase>.ImplementsIFloatingPoint ? NumericDomain.RealNumbers
        : NumericTypeInfo<TNumberBase>.IsComplex ? NumericDomain.ComplexNumbers
        : NumericDomain.Unknown;

      public static System.Text.Rune GetNumericDomainSymbol()
        => (System.Text.Rune)(char)GetNumericDomain<TNumberBase>();
    }
  }
}
