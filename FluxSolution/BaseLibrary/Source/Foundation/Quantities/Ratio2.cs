namespace Flux.Quantity
{
  /// <summary>A ratio indicates how many times one number contains another. This struct stores both constituting numbers of the ratio (numerator and denominator).</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Ratio"/>
  public struct Ratio2
    : System.IEquatable<Ratio2>, IValuedUnit
  {
    private readonly double m_numerator;
    private readonly double m_denominator;

    public Ratio2(double numerator, double denominator)
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
    public static explicit operator double(Ratio2 v)
      => v.Value;

    public static bool operator ==(Ratio2 a, Ratio2 b)
      => a.Equals(b);
    public static bool operator !=(Ratio2 a, Ratio2 b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(Ratio2 other)
      => m_numerator == other.m_numerator && m_denominator == other.m_denominator;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Ratio2 o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_numerator, m_denominator);
    public override string ToString()
      => $"<{GetType().Name}: {m_numerator}:{m_denominator} ({Value})>";
    #endregion Object overrides
  }
}
