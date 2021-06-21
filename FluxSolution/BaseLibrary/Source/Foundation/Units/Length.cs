namespace Flux.Units
{
  /// <summary>Length.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Length"/>
  public struct Length
    : System.IComparable<Length>, System.IEquatable<Length>, IStandardizedScalar
  {
    private readonly double m_meter;

    public Length(double meter)
      => m_meter = meter;

    public double Foot
      => ConvertMeterToFoot(m_meter);
    public double Kilometer
      => ConvertMeterToKilometer(m_meter);
    public double Meter
      => m_meter;
    public double Mile
      => ConvertMeterToMile(m_meter);
    public double NauticalMile
      => ConvertMeterToNauticalMile(m_meter);

    #region Static methods
    public static Length Add(Length left, Length right)
      => new Length(left.m_meter + right.m_meter);
    public static double ConvertFootToMeter(double feet)
      => feet * 0.3048;
    public static double ConvertKilometerToMeter(double kilometers)
      => kilometers * 1000;
    public static double ConvertMeterToFoot(double meters)
      => meters / 0.3048;
    public static double ConvertMeterToNauticalMile(double meters)
      => meters / 1852;
    public static double ConvertMeterToKilometer(double meters)
      => meters / 1000;
    public static double ConvertMeterToMile(double meters)
      => meters / 1609.344;
    public static double ConvertMileToMeter(double miles)
      => miles * 1609.344;
    public static double ConvertNauticalMileToMeter(double nauticalMiles)
      => nauticalMiles * 1852;
    public static Length Divide(Length left, Length right)
      => new Length(left.m_meter / right.m_meter);
    public static Length FromFeet(double feet)
      => new Length(ConvertFootToMeter(feet));
    public static Length FromKilometers(double kilometer)
      => new Length(ConvertKilometerToMeter(kilometer));
    public static Length FromMiles(double miles)
      => new Length(ConvertMileToMeter(miles));
    public static Length FromNauticalMiles(double nauticalMiles)
      => new Length(ConvertNauticalMileToMeter(nauticalMiles));
    public static Length Multiply(Length left, Length right)
      => new Length(left.m_meter * right.m_meter);
    public static Length Negate(Length value)
      => new Length(-value.m_meter);
    public static Length Remainder(Length dividend, Length divisor)
      => new Length(dividend.m_meter % divisor.m_meter);
    public static Length Subtract(Length left, Length right)
      => new Length(left.m_meter - right.m_meter);
    #endregion Static methods

    #region Overloaded operators
    public static implicit operator double(Length v)
      => v.m_meter;
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
      => m_meter.CompareTo(other.m_meter);

    // IEquatable
    public bool Equals(Length other)
      => m_meter == other.m_meter;

    // IUnitStandardized
    public double GetScalar()
      => m_meter;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Length o && Equals(o);
    public override int GetHashCode()
      => m_meter.GetHashCode();
    public override string ToString()
      => $"<{nameof(Length)}: {m_meter} m>";
    #endregion Object overrides
  }
}
