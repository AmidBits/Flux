namespace Flux.Units
{
  /// <summary>Frequency is a mutable data type to accomodate changes across multiple consumers.</summary>
  public struct Volume
    : System.IComparable<Volume>, System.IEquatable<Volume>, System.IFormattable
  {
    private readonly double m_cubicMeter;

    public Volume(double cubicMeter)
      => m_cubicMeter = cubicMeter;

    public double CubicMeter
      => m_cubicMeter;

    #region Static methods
    public static Volume Add(Volume left, Volume right)
      => new Volume(left.m_cubicMeter + right.m_cubicMeter);
    public static Volume Divide(Volume left, Volume right)
      => new Volume(left.m_cubicMeter / right.m_cubicMeter);
    public static Volume Multiply(Volume left, Volume right)
      => new Volume(left.m_cubicMeter * right.m_cubicMeter);
    public static Volume Negate(Volume value)
      => new Volume(-value.m_cubicMeter);
    public static Volume Remainder(Volume dividend, Volume divisor)
      => new Volume(dividend.m_cubicMeter % divisor.m_cubicMeter);
    public static Volume Subtract(Volume left, Volume right)
      => new Volume(left.m_cubicMeter - right.m_cubicMeter);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Volume v)
      => v.m_cubicMeter;
    public static implicit operator Volume(double v)
      => new Volume(v);

    public static bool operator <(Volume a, Volume b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Volume a, Volume b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Volume a, Volume b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Volume a, Volume b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Volume a, Volume b)
      => a.Equals(b);
    public static bool operator !=(Volume a, Volume b)
      => !a.Equals(b);

    public static Volume operator +(Volume a, Volume b)
      => Add(a, b);
    public static Volume operator /(Volume a, Volume b)
      => Divide(a, b);
    public static Volume operator *(Volume a, Volume b)
      => Multiply(a, b);
    public static Volume operator -(Volume v)
      => Negate(v);
    public static Volume operator %(Volume a, Volume b)
      => Remainder(a, b);
    public static Volume operator -(Volume a, Volume b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Volume other)
      => m_cubicMeter.CompareTo(other.m_cubicMeter);

    // IEquatable
    public bool Equals(Volume other)
      => m_cubicMeter == other.m_cubicMeter;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => string.Format(formatProvider, format ?? $"<{nameof(Volume)}: {{0:D3}}>", this);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Volume o && Equals(o);
    public override int GetHashCode()
      => m_cubicMeter.GetHashCode();
    public override string ToString()
      => ToString(null, null);
    #endregion Object overrides
  }
}
