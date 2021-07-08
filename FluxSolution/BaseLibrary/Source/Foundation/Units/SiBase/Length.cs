namespace Flux.Units
{
  public enum LengthUnit
  {
    Millimeter,
    Centimeter,
    Inch,
    Decimeter,
    Foot,
    Yard,
    Meter,
    NauticalMile,
    Mile,
    Kilometer,
  }

  /// <summary>Length.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Length"/>
  public struct Length
    : System.IComparable<Length>, System.IEquatable<Length>, IStandardizedScalar
  {
    private readonly double m_meter;

    public Length(double meter)
      => m_meter = meter;

    public double Meter
      => m_meter;

    public double ToUnitValue(LengthUnit unit)
    {
      switch (unit)
      {
        case LengthUnit.Millimeter:
          return m_meter * 1000;
        case LengthUnit.Centimeter:
          return m_meter * 100;
        case LengthUnit.Inch:
          return m_meter / 0.0254;
        case LengthUnit.Decimeter:
          return m_meter * 10;
        case LengthUnit.Foot:
          return m_meter / 0.3048;
        case LengthUnit.Yard:
          return m_meter / 0.9144;
        case LengthUnit.Meter:
          return m_meter;
        case LengthUnit.NauticalMile:
          return m_meter / 1852;
        case LengthUnit.Mile:
          return m_meter / 1609.344;
        case LengthUnit.Kilometer:
          return m_meter / 1000;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }

    #region Static methods
    public static Length Add(Length left, Length right)
      => new Length(left.m_meter + right.m_meter);
    public static Length Divide(Length left, Length right)
      => new Length(left.m_meter / right.m_meter);
    public static Frequency FromAcousticsAsWaveLength(Speed soundVelocity, Frequency frequency)
      => new Frequency(soundVelocity.MeterPerSecond / frequency.Hertz);
    public static Length FromUnitValue(LengthUnit unit, double value)
    {
      switch (unit)
      {
        case LengthUnit.Millimeter:
          return new Length(value / 1000);
        case LengthUnit.Centimeter:
          return new Length(value / 100);
        case LengthUnit.Inch:
          return new Length(value * 0.0254);
        case LengthUnit.Decimeter:
          return new Length(value / 10);
        case LengthUnit.Foot:
          return new Length(value * 0.3048);
        case LengthUnit.Yard:
          return new Length(value * 0.9144);
        case LengthUnit.Meter:
          return new Length(value);
        case LengthUnit.NauticalMile:
          return new Length(value * 1852);
        case LengthUnit.Mile:
          return new Length(value * 1609.344);
        case LengthUnit.Kilometer:
          return new Length(value * 1000);
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }
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
