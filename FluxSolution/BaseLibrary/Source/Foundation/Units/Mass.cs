namespace Flux.Units
{
  /// <summary>Mass.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Mass"/>
  public struct Mass
    : System.IComparable<Mass>, System.IEquatable<Mass>, System.IFormattable
  {
    private readonly double m_kilograms;

    public Mass(double kilograms)
      => m_kilograms = kilograms;

    public double Kilograms
      => m_kilograms;
    public double Pounds
      => ConvertKilogramsToPounds(m_kilograms);

    #region Static methods
    public static Mass Add(Mass left, Mass right)
      => new Mass(left.m_kilograms + right.m_kilograms);
    public static double ConvertKilogramsToPounds(double pounds)
      => pounds / 0.45359237;
    public static double ConvertPoundsToKilograms(double pounds)
      => pounds * 0.45359237;
    public static Mass Divide(Mass left, Mass right)
      => new Mass(left.m_kilograms / right.m_kilograms);
    public static Mass FromPounds(double pounds)
      => new Mass(ConvertPoundsToKilograms(pounds));
    public static Mass Multiply(Mass left, Mass right)
      => new Mass(left.m_kilograms * right.m_kilograms);
    public static Mass Negate(Mass value)
      => new Mass(-value.m_kilograms);
    public static Mass Remainder(Mass dividend, Mass divisor)
      => new Mass(dividend.m_kilograms % divisor.m_kilograms);
    public static Mass Subtract(Mass left, Mass right)
      => new Mass(left.m_kilograms - right.m_kilograms);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Mass v)
      => v.m_kilograms;
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
      => m_kilograms.CompareTo(other.m_kilograms);

    // IEquatable
    public bool Equals(Mass other)
      => m_kilograms == other.m_kilograms;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => string.Format(formatProvider, format ?? $"<{nameof(Mass)}: {{0:D3}}>", this);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Mass o && Equals(o);
    public override int GetHashCode()
      => m_kilograms.GetHashCode();
    public override string ToString()
      => ToString(null, null);
    #endregion Object overrides
  }
}
