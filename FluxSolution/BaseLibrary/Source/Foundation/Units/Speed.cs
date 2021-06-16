namespace Flux.Units
{
  /// <summary>Frequency is a mutable data type to accomodate changes across multiple consumers.</summary>
  public struct Speed
    : System.IComparable<Speed>, System.IEquatable<Speed>, System.IFormattable
  {
    private readonly double m_metersPerSecond;

    public Speed(double metersPerSecond)
      => m_metersPerSecond = metersPerSecond;

    public double MetersPerSecond
      => m_metersPerSecond;

    #region Static methods
    public static Speed Add(Speed left, Speed right)
      => new Speed(left.m_metersPerSecond + right.m_metersPerSecond);
    public static Speed Divide(Speed left, Speed right)
      => new Speed(left.m_metersPerSecond / right.m_metersPerSecond);
    public static Speed Multiply(Speed left, Speed right)
      => new Speed(left.m_metersPerSecond * right.m_metersPerSecond);
    public static Speed Negate(Speed value)
      => new Speed(-value.m_metersPerSecond);
    public static Speed Remainder(Speed dividend, Speed divisor)
      => new Speed(dividend.m_metersPerSecond % divisor.m_metersPerSecond);
    public static Speed Subtract(Speed left, Speed right)
      => new Speed(left.m_metersPerSecond - right.m_metersPerSecond);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Speed v)
      => v.m_metersPerSecond;
    public static implicit operator Speed(double v)
      => new Speed(v);

    public static bool operator <(Speed a, Speed b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Speed a, Speed b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Speed a, Speed b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Speed a, Speed b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Speed a, Speed b)
      => a.Equals(b);
    public static bool operator !=(Speed a, Speed b)
      => !a.Equals(b);

    public static Speed operator +(Speed a, Speed b)
      => Add(a, b);
    public static Speed operator /(Speed a, Speed b)
      => Divide(a, b);
    public static Speed operator *(Speed a, Speed b)
      => Multiply(a, b);
    public static Speed operator -(Speed v)
      => Negate(v);
    public static Speed operator %(Speed a, Speed b)
      => Remainder(a, b);
    public static Speed operator -(Speed a, Speed b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Speed other)
      => m_metersPerSecond.CompareTo(other.m_metersPerSecond);

    // IEquatable
    public bool Equals(Speed other)
      => m_metersPerSecond == other.m_metersPerSecond;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => string.Format(formatProvider, format ?? $"<{nameof(Speed)}: {{0:D3}}>", this);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Speed o && Equals(o);
    public override int GetHashCode()
      => m_metersPerSecond.GetHashCode();
    public override string ToString()
      => ToString(null, null);
    #endregion Object overrides
  }
}
