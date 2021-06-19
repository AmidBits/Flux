namespace Flux.Units
{
  /// <summary>Frequency is a mutable data type to accomodate changes across multiple consumers.</summary>
  public struct Power
    : System.IComparable<Power>, System.IEquatable<Power>, System.IFormattable
  {
    private readonly double m_watt;

    public Power(double watt)
      => m_watt = watt;

    public double Watt
      => m_watt;

    #region Static methods
    public static Power Add(Power left, Power right)
      => new Power(left.m_watt + right.m_watt);
    public static Power Divide(Power left, Power right)
      => new Power(left.m_watt / right.m_watt);
    public static Power Multiply(Power left, Power right)
      => new Power(left.m_watt * right.m_watt);
    public static Power Negate(Power value)
      => new Power(-value.m_watt);
    public static Power Remainder(Power dividend, Power divisor)
      => new Power(dividend.m_watt % divisor.m_watt);
    public static Power Subtract(Power left, Power right)
      => new Power(left.m_watt - right.m_watt);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Power v)
      => v.m_watt;
    public static implicit operator Power(double v)
      => new Power(v);

    public static bool operator <(Power a, Power b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Power a, Power b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Power a, Power b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Power a, Power b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Power a, Power b)
      => a.Equals(b);
    public static bool operator !=(Power a, Power b)
      => !a.Equals(b);

    public static Power operator +(Power a, Power b)
      => Add(a, b);
    public static Power operator /(Power a, Power b)
      => Divide(a, b);
    public static Power operator *(Power a, Power b)
      => Multiply(a, b);
    public static Power operator -(Power v)
      => Negate(v);
    public static Power operator %(Power a, Power b)
      => Remainder(a, b);
    public static Power operator -(Power a, Power b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Power other)
      => m_watt.CompareTo(other.m_watt);

    // IEquatable
    public bool Equals(Power other)
      => m_watt == other.m_watt;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => string.Format(formatProvider, format ?? $"<{nameof(Power)}: {{0:D3}}>", this);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Power o && Equals(o);
    public override int GetHashCode()
      => m_watt.GetHashCode();
    public override string ToString()
      => ToString(null, null);
    #endregion Object overrides
  }
}
