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
    public double Millimeter
      => ConvertMeterToMillimeter(m_meter);
    public double NauticalMile
      => ConvertMeterToNauticalMile(m_meter);

    #region Static methods
    public static Length Add(Length left, Length right)
      => new Length(left.m_meter + right.m_meter);
    public static double ConvertFootToMeter(double feet)
      => feet * 0.3048;
    public static double ConvertKilometerToMeter(double kilometer)
      => kilometer * 1000;
    public static double ConvertMeterToFoot(double meter)
      => meter / 0.3048;
    public static double ConvertMeterToNauticalMile(double meter)
      => meter / 1852;
    public static double ConvertMeterToKilometer(double meter)
      => meter / 1000;
    public static double ConvertMeterToMile(double meter)
      => meter / 1609.344;
    public static double ConvertMeterToMillimeter(double meter)
      => meter * 1000;
    public static double ConvertMileToMeter(double mile)
      => mile * 1609.344;
    public static double ConvertMillimeterToMeter(double millimeter)
      => millimeter / 1000;
    public static double ConvertNauticalMileToMeter(double nauticalMile)
      => nauticalMile * 1852;
    public static Length Divide(Length left, Length right)
      => new Length(left.m_meter / right.m_meter);
    public static Frequency FromAcousticsAsWaveLength(Speed soundVelocity, Frequency frequency)
      => new Frequency(soundVelocity.MeterPerSecond / frequency.Hertz);
    public static Length FromFoot(double foot)
      => new Length(ConvertFootToMeter(foot));
    public static Length FromKilometer(double kilometer)
      => new Length(ConvertKilometerToMeter(kilometer));
    public static Length FromMile(double mile)
      => new Length(ConvertMileToMeter(mile));
    public static Length FromMillimeter(double millimeter)
      => new Length(ConvertMillimeterToMeter(millimeter));
    public static Length FromNauticalMile(double nauticalMile)
      => new Length(ConvertNauticalMileToMeter(nauticalMile));
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
    public static explicit operator double(Length v)
      => v.m_meter;
    public static explicit operator Length(double v)
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
