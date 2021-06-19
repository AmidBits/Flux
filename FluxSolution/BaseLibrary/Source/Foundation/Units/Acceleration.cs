namespace Flux.Units
{
  /// <summary>Frequency is a mutable data type to accomodate changes across multiple consumers.</summary>
  public struct Acceleration
    : System.IComparable<Acceleration>, System.IEquatable<Acceleration>, System.IFormattable
  {
    private readonly double m_meterPerSecondSquare;

    public Acceleration(double meterPerSecondSquare)
      => m_meterPerSecondSquare = meterPerSecondSquare;

    public double MeterPerSecondSquare
      => m_meterPerSecondSquare;

    #region Static methods
    public static Acceleration Add(Acceleration left, Acceleration right)
      => new Acceleration(left.m_meterPerSecondSquare + right.m_meterPerSecondSquare);
    public static Acceleration Divide(Acceleration left, Acceleration right)
      => new Acceleration(left.m_meterPerSecondSquare / right.m_meterPerSecondSquare);
    public static Acceleration Multiply(Acceleration left, Acceleration right)
      => new Acceleration(left.m_meterPerSecondSquare * right.m_meterPerSecondSquare);
    public static Acceleration Negate(Acceleration value)
      => new Acceleration(-value.m_meterPerSecondSquare);
    public static Acceleration Remainder(Acceleration dividend, Acceleration divisor)
      => new Acceleration(dividend.m_meterPerSecondSquare % divisor.m_meterPerSecondSquare);
    public static Acceleration Subtract(Acceleration left, Acceleration right)
      => new Acceleration(left.m_meterPerSecondSquare - right.m_meterPerSecondSquare);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Acceleration v)
      => v.m_meterPerSecondSquare;
    public static implicit operator Acceleration(double v)
      => new Acceleration(v);

    public static bool operator <(Acceleration a, Acceleration b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Acceleration a, Acceleration b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Acceleration a, Acceleration b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Acceleration a, Acceleration b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Acceleration a, Acceleration b)
      => a.Equals(b);
    public static bool operator !=(Acceleration a, Acceleration b)
      => !a.Equals(b);

    public static Acceleration operator +(Acceleration a, Acceleration b)
      => Add(a, b);
    public static Acceleration operator /(Acceleration a, Acceleration b)
      => Divide(a, b);
    public static Acceleration operator *(Acceleration a, Acceleration b)
      => Multiply(a, b);
    public static Acceleration operator -(Acceleration v)
      => Negate(v);
    public static Acceleration operator %(Acceleration a, Acceleration b)
      => Remainder(a, b);
    public static Acceleration operator -(Acceleration a, Acceleration b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Acceleration other)
      => m_meterPerSecondSquare.CompareTo(other.m_meterPerSecondSquare);

    // IEquatable
    public bool Equals(Acceleration other)
      => m_meterPerSecondSquare == other.m_meterPerSecondSquare;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => string.Format(formatProvider, format ?? $"<{nameof(Acceleration)}: {{0:D3}}>", this);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Acceleration o && Equals(o);
    public override int GetHashCode()
      => m_meterPerSecondSquare.GetHashCode();
    public override string ToString()
      => ToString(null, null);
    #endregion Object overrides
  }
}
