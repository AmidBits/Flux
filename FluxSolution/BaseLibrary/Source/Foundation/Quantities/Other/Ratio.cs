namespace Flux.Quantity
{
  /// <summary>A ratio indicates how many times one number contains another. It is two related quantities measured with the same unit (here System.Double), and is a dimensionless number (value). This struct stores both constituting numbers of the ratio (numerator and denominator) and returns the quotient as a value.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Ratio"/>
#if NET5_0
  public struct Ratio
    : System.IEquatable<Ratio>, IValuedUnit<double>
#elif NET6_0_OR_GREATER
  public struct Ratio
    : IValuedUnit<double>
#endif
  {
    private readonly double m_numerator;
    private readonly double m_denominator;

    public Ratio(double numerator, double denominator)
    {
      m_numerator = numerator;
      m_denominator = denominator;
    }

    public double Numerator
      => m_numerator;
    public double Denominator
      => m_denominator;

    public double Value
      => m_numerator / m_denominator;

    #region Overloaded operators
    public static explicit operator double(Ratio v)
      => v.Value;

#if NET5_0
    public static bool operator ==(Ratio a, Ratio b)
      => a.Equals(b);
    public static bool operator !=(Ratio a, Ratio b)
      => !a.Equals(b);
#endif
    #endregion Overloaded operators

    #region Implemented interfaces
#if NET5_0
    // IEquatable
    public bool Equals(Ratio other)
      => m_numerator == other.m_numerator && m_denominator == other.m_denominator;
#endif
    #endregion Implemented interfaces

    #region Object overrides
#if NET5_0
    public override bool Equals(object? obj)
      => obj is Ratio o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_numerator, m_denominator);
#endif
    public override string ToString()
      => $"{GetType().Name} {{ Numerator = {m_numerator}, Denominator = {m_denominator} ({Value}) }}";
    #endregion Object overrides
  }
}
