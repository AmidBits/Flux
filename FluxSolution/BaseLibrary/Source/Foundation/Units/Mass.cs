namespace Flux.Units
{
  /// <summary>Mass.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Mass"/>
  public struct Mass
    : System.IComparable<Mass>, System.IEquatable<Mass>, IStandardizedScalar
  {
    private readonly double m_kilogram;

    public Mass(double kilograms)
      => m_kilogram = kilograms;

    public double Kilograms
      => m_kilogram;
    public double Pounds
      => ConvertKilogramsToPounds(m_kilogram);

    #region Static methods
    public static Mass Add(Mass left, Mass right)
      => new Mass(left.m_kilogram + right.m_kilogram);
    public static double ConvertKilogramsToPounds(double pounds)
      => pounds / 0.45359237;
    public static double ConvertPoundsToKilograms(double pounds)
      => pounds * 0.45359237;
    public static Mass Divide(Mass left, Mass right)
      => new Mass(left.m_kilogram / right.m_kilogram);
    public static Mass FromPounds(double pounds)
      => new Mass(ConvertPoundsToKilograms(pounds));
    public static Mass Multiply(Mass left, Mass right)
      => new Mass(left.m_kilogram * right.m_kilogram);
    public static Mass Negate(Mass value)
      => new Mass(-value.m_kilogram);
    public static Mass Remainder(Mass dividend, Mass divisor)
      => new Mass(dividend.m_kilogram % divisor.m_kilogram);
    public static Mass Subtract(Mass left, Mass right)
      => new Mass(left.m_kilogram - right.m_kilogram);
    #endregion Static methods

    #region Overloaded operators
    public static implicit operator double(Mass v)
      => v.m_kilogram;
    public static implicit operator Mass(double v)
      => new Mass(v);

    public static bool operator <(Mass a, Mass b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Mass a, Mass b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Mass a, Mass b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Mass a, Mass b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Mass a, Mass b)
      => a.Equals(b);
    public static bool operator !=(Mass a, Mass b)
      => !a.Equals(b);

    public static Mass operator +(Mass a, Mass b)
      => Add(a, b);
    public static Mass operator /(Mass a, Mass b)
      => Divide(a, b);
    public static Mass operator *(Mass a, Mass b)
      => Multiply(a, b);
    public static Mass operator -(Mass v)
      => Negate(v);
    public static Mass operator %(Mass a, Mass b)
      => Remainder(a, b);
    public static Mass operator -(Mass a, Mass b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Mass other)
      => m_kilogram.CompareTo(other.m_kilogram);

    // IEquatable
    public bool Equals(Mass other)
      => m_kilogram == other.m_kilogram;

    // IUnitStandardized
    public double GetScalar()
      => m_kilogram;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Mass o && Equals(o);
    public override int GetHashCode()
      => m_kilogram.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_kilogram} kg>";
    #endregion Object overrides
  }
}
