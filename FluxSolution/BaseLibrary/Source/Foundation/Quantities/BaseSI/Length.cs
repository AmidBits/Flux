namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static Length Create(this LengthUnit unit, double value)
      => new(value, unit);
    public static string GetUnitSymbol(this LengthUnit unit)
      => unit switch
      {
        LengthUnit.Millimeter => @" mm",
        LengthUnit.Centimeter => @" cm",
        LengthUnit.Inch => @" in",
        LengthUnit.Decimeter => @" dm",
        LengthUnit.Foot => @" ft",
        LengthUnit.Yard => @" yd",
        LengthUnit.Meter => @" m",
        LengthUnit.NauticalMile => @" nm",
        LengthUnit.Mile => @" mi",
        LengthUnit.Kilometer => @" km",
        LengthUnit.AstronomicalUnit => @" au",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

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
    AstronomicalUnit
  }

  /// <summary>Length. SI unit of meter. This is a base quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Length"/>
  public struct Length
    : System.IComparable<Length>, System.IEquatable<Length>, IValueGeneralizedUnit<double>, IValueBaseUnitSI<double>
  {
    public const LengthUnit DefaultUnit = LengthUnit.Meter;

    private readonly double m_value;

    public Length(double value, LengthUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        LengthUnit.Millimeter => value / 1000,
        LengthUnit.Centimeter => value / 100,
        LengthUnit.Inch => value * 0.0254,
        LengthUnit.Decimeter => value / 10,
        LengthUnit.Foot => value * 0.3048,
        LengthUnit.Yard => value * 0.9144,
        LengthUnit.Meter => value,
        LengthUnit.NauticalMile => value * 1852,
        LengthUnit.Mile => value * 1609.344,
        LengthUnit.Kilometer => value * 1000,
        LengthUnit.AstronomicalUnit => value * 149597870700,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double BaseUnitValue
      => m_value;

    public double GeneralUnitValue
      => m_value;

    public string ToUnitString(LengthUnit unit = DefaultUnit, string? format = null)
      => $"{(format is null ? ToUnitValue(unit) : string.Format($"{{0:{format}}}", ToUnitValue(unit)))}{unit.GetUnitSymbol()}";
    public double ToUnitValue(LengthUnit unit = DefaultUnit)
      => unit switch
      {
        LengthUnit.Millimeter => m_value * 1000,
        LengthUnit.Centimeter => m_value * 100,
        LengthUnit.Inch => m_value / 0.0254,
        LengthUnit.Decimeter => m_value * 10,
        LengthUnit.Foot => m_value / 0.3048,
        LengthUnit.Yard => m_value / 0.9144,
        LengthUnit.Meter => m_value,
        LengthUnit.NauticalMile => m_value / 1852,
        LengthUnit.Mile => m_value / 1609.344,
        LengthUnit.Kilometer => m_value / 1000,
        LengthUnit.AstronomicalUnit => m_value / 149597870700,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    /// <summary>Computes the wavelength from the specified phase velocity and frequency. A wavelength is the spatial period of a periodic wave, i.e. the distance over which the wave's shape repeats. The default reference value for the speed of sound is 343.21 m/s. This determines the unit of measurement (i.e. meters per second) for the wavelength distance.</summary>
    /// <param name="phaseVelocity">The constant speed of the traveling wave. If these are sound waves then typically this is the speed of sound. If electromagnetic radiation (e.g. light) in free space then speed of light.</param>
    /// <param name="frequency"></param>
    /// <returns>The wavelength of the frequency cycle at the phase velocity.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Wavelength"/>
    public static Length ComputeWavelength(Speed phaseVelocity, Frequency frequency)
      => new(phaseVelocity.GeneralUnitValue / frequency.GeneralUnitValue);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Length v)
      => v.m_value;
    public static explicit operator Length(double v)
      => new(v);

    public static bool operator <(Length a, Length b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Length a, Length b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Length a, Length b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Length a, Length b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Length a, Length b)
      => a.Equals(b);
    public static bool operator !=(Length a, Length b)
      => !a.Equals(b);

    public static Length operator -(Length v)
      => new(-v.m_value);
    public static Length operator +(Length a, double b)
      => new(a.m_value + b);
    public static Length operator +(Length a, Length b)
      => a + b.m_value;
    public static Length operator /(Length a, double b)
      => new(a.m_value / b);
    public static Length operator /(Length a, Length b)
      => a / b.m_value;
    public static Length operator *(Length a, double b)
      => new(a.m_value * b);
    public static Length operator *(Length a, Length b)
      => a * b.m_value;
    public static Length operator %(Length a, double b)
      => new(a.m_value % b);
    public static Length operator %(Length a, Length b)
      => a % b.m_value;
    public static Length operator -(Length a, double b)
      => new(a.m_value - b);
    public static Length operator -(Length a, Length b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Length other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Length other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
       => obj is Length o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{nameof(Length)} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
