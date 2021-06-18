namespace Flux.Units
{
  /// <summary>Frequency is a mutable data type to accomodate changes across multiple consumers.</summary>
  public struct Length
    : System.IComparable<Length>, System.IEquatable<Length>, System.IFormattable
  {
    private readonly double m_meters;

    public Length(double meters)
      => m_meters = meters;

    public double Feet
      => ConvertMetersToFeet(m_meters);
    public double Kilometers
      => ConvertMetersToKilometers(m_meters);
    public double Meters
      => m_meters;
    public double Miles
      => ConvertMetersToMiles(m_meters);
    public double NauticalMiles
      => ConvertMetersToNauticalMiles(m_meters);

    #region Static methods
    public static Length Add(Length left, Length right)
      => new Length(left.m_meters + right.m_meters);
    public static double ConvertFeetToMeters(double feet)
      => feet * 0.3048;
    public static double ConvertKilometersToMeters(double kilometers)
      => kilometers * 1000;
    public static double ConvertMetersToFeet(double meters)
      => meters / 0.3048;
    public static double ConvertMetersToNauticalMiles(double meters)
      => meters / 1852;
    public static double ConvertMetersToKilometers(double meters)
      => meters / 1000;
    public static double ConvertMetersToMiles(double meters)
      => meters / 1609.344;
    public static double ConvertMilesToMeters(double miles)
      => miles * 1609.344;
    public static double ConvertNauticalMilesToMeters(double nauticalMiles)
      => nauticalMiles * 1852;
    public static Length Divide(Length left, Length right)
      => new Length(left.m_meters / right.m_meters);
    public static Length FromFeet(double feet)
      => new Length(ConvertFeetToMeters(feet));
    public static Length FromKilometers(double kilometer)
      => new Length(ConvertKilometersToMeters(kilometer));
    public static Length FromMiles(double miles)
      => new Length(ConvertMilesToMeters(miles));
    public static Length FromNauticalMiles(double nauticalMiles)
      => new Length(ConvertNauticalMilesToMeters(nauticalMiles));
    public static Length Multiply(Length left, Length right)
      => new Length(left.m_meters * right.m_meters);
    public static Length Negate(Length value)
      => new Length(-value.m_meters);
    public static Length Remainder(Length dividend, Length divisor)
      => new Length(dividend.m_meters % divisor.m_meters);
    public static Length Subtract(Length left, Length right)
      => new Length(left.m_meters - right.m_meters);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Length v)
      => v.m_meters;
    public static implicit operator Length(double v)
      => new Length(v);

    public static bool operator <(Length a, Length b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Length a, Length b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Length a, Length b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Length a, Length b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Length a, Length b)
      => a.Equals(b);
    public static bool operator !=(Length a, Length b)
      => !a.Equals(b);

    public static Length operator +(Length a, Length b)
      => Add(a, b);
    public static Length operator /(Length a, Length b)
      => Divide(a, b);
    public static Length operator *(Length a, Length b)
      => Multiply(a, b);
    public static Length operator -(Length v)
      => Negate(v);
    public static Length operator %(Length a, Length b)
      => Remainder(a, b);
    public static Length operator -(Length a, Length b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Length other)
      => m_meters.CompareTo(other.m_meters);

    // IEquatable
    public bool Equals(Length other)
      => m_meters == other.m_meters;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => string.Format(formatProvider, format ?? $"<{nameof(Length)}: {{0:D3}}>", this);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Length o && Equals(o);
    public override int GetHashCode()
      => m_meters.GetHashCode();
    public override string ToString()
      => ToString(null, null);
    #endregion Object overrides
  }
}
