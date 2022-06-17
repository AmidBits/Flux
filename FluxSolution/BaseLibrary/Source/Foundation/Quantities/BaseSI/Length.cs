namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this LengthUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        LengthUnit.Nanometer => preferUnicode ? "\u339A" : "nm",
        LengthUnit.Micrometer => preferUnicode ? "\u339B" : "µm",
        LengthUnit.Millimeter => preferUnicode ? "\u339C" : "mm",
        LengthUnit.Centimeter => preferUnicode ? "\u339D" : "cm",
        LengthUnit.Inch => preferUnicode ? "\u33CC" : "in",
        LengthUnit.Decimeter => preferUnicode ? "\u3377" : "dm",
        LengthUnit.Foot => "ft",
        LengthUnit.Yard => "yd",
        LengthUnit.Meter => "m",
        LengthUnit.Kilometer => preferUnicode ? "\u339E" : "km",
        LengthUnit.Mile => "mi",
        LengthUnit.NauticalMile => "NM", // There is no single internationally agreed symbol. Others used are "N", "NM", "nmi" and "nm".
        LengthUnit.AstronomicalUnit => preferUnicode ? "\u3373" : "au",
        LengthUnit.Parsec => preferUnicode ? "\u3376" : "pc",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum LengthUnit
  {
    /// <summary>This is the default unit for length.</summary>
    Meter,
    Nanometer,
    Micrometer,
    Millimeter,
    Centimeter,
    Inch,
    Decimeter,
    Foot,
    Yard,
    Kilometer,
    Mile,
    NauticalMile,
    AstronomicalUnit,
    Parsec,
  }

  /// <summary>Length. SI unit of meter. This is a base quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Length"/>
  public readonly struct Length
    : System.IComparable, System.IComparable<Length>, System.IConvertible, System.IEquatable<Length>, System.IFormattable, IUnitQuantifiable<double, LengthUnit>
  {
    public const double PiParsecsInMeters = 96939420213600000;
    public const double OneParsecInMeters = PiParsecsInMeters / System.Math.PI;

    public const LengthUnit DefaultUnit = LengthUnit.Meter;

    private readonly double m_value;

    public Length(double value, LengthUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        LengthUnit.Nanometer => value * 1E-9,
        LengthUnit.Micrometer => value * 1E-6,
        LengthUnit.Millimeter => value * 1E-3,
        LengthUnit.Centimeter => value * 1E-2,
        LengthUnit.Inch => value * 0.0254,
        LengthUnit.Decimeter => value * 1E-1,
        LengthUnit.Foot => value * 0.3048,
        LengthUnit.Yard => value * 0.9144,
        LengthUnit.Meter => value,
        LengthUnit.Kilometer => value * 1E+3,
        LengthUnit.Mile => value * 1609.344,
        LengthUnit.NauticalMile => value * 1852,
        LengthUnit.AstronomicalUnit => value * 149597870700,
        LengthUnit.Parsec => value * OneParsecInMeters,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    /// <summary>Computes the wavelength from the specified phase velocity and frequency. A wavelength is the spatial period of a periodic wave, i.e. the distance over which the wave's shape repeats. The default reference value for the speed of sound is 343.21 m/s. This determines the unit of measurement (i.e. meters per second) for the wavelength distance.</summary>
    /// <param name="phaseVelocity">The constant speed of the traveling wave. If these are sound waves then typically this is the speed of sound. If electromagnetic radiation (e.g. light) in free space then speed of light.</param>
    /// <param name="frequency"></param>
    /// <returns>The wavelength of the frequency cycle at the phase velocity.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Wavelength"/>
    [System.Diagnostics.Contracts.Pure]
    public static Length ComputeWavelength(LinearVelocity phaseVelocity, Frequency frequency)
      => new(phaseVelocity.Value / frequency.Value);

    /// <summary>Creates a new <see cref="Length"/> instance from <see cref="LinearVelocity"/> and <see cref="AngularVelocity"/></summary>
    /// <param name="speed"></param>
    /// <param name="angularVelocity"></param>
    [System.Diagnostics.Contracts.Pure]
    public static Length From(LinearVelocity speed, AngularVelocity angularVelocity)
      => new(speed.Value / angularVelocity.Value);
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static explicit operator double(Length v) => v.m_value;
    [System.Diagnostics.Contracts.Pure] public static explicit operator Length(double v) => new(v);

    [System.Diagnostics.Contracts.Pure] public static bool operator <(Length a, Length b) => a.CompareTo(b) < 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator <=(Length a, Length b) => a.CompareTo(b) <= 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >(Length a, Length b) => a.CompareTo(b) > 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >=(Length a, Length b) => a.CompareTo(b) >= 0;

    [System.Diagnostics.Contracts.Pure] public static bool operator ==(Length a, Length b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(Length a, Length b) => !a.Equals(b);

    [System.Diagnostics.Contracts.Pure] public static Length operator -(Length v) => new(-v.m_value);
    [System.Diagnostics.Contracts.Pure] public static Length operator +(Length a, double b) => new(a.m_value + b);
    [System.Diagnostics.Contracts.Pure] public static Length operator +(Length a, Length b) => a + b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Length operator /(Length a, double b) => new(a.m_value / b);
    [System.Diagnostics.Contracts.Pure] public static Length operator /(Length a, Length b) => a / b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Length operator *(Length a, double b) => new(a.m_value * b);
    [System.Diagnostics.Contracts.Pure] public static Length operator *(Length a, Length b) => a * b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Length operator %(Length a, double b) => new(a.m_value % b);
    [System.Diagnostics.Contracts.Pure] public static Length operator %(Length a, Length b) => a % b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Length operator -(Length a, double b) => new(a.m_value - b);
    [System.Diagnostics.Contracts.Pure] public static Length operator -(Length a, Length b) => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<>
    [System.Diagnostics.Contracts.Pure] public int CompareTo(Length other) => m_value.CompareTo(other.m_value);
    // IComparable
    [System.Diagnostics.Contracts.Pure] public int CompareTo(object? other) => other is not null && other is Length o ? CompareTo(o) : -1;

    #region IConvertible
    [System.Diagnostics.Contracts.Pure] public System.TypeCode GetTypeCode() => System.TypeCode.Object;
    [System.Diagnostics.Contracts.Pure] public bool ToBoolean(System.IFormatProvider? provider) => Value != 0;
    [System.Diagnostics.Contracts.Pure] public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(Value);
    [System.Diagnostics.Contracts.Pure] public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(Value);
    [System.Diagnostics.Contracts.Pure] public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(Value);
    [System.Diagnostics.Contracts.Pure] public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(Value);
    [System.Diagnostics.Contracts.Pure] public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(Value);
    [System.Diagnostics.Contracts.Pure] public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(Value);
    [System.Diagnostics.Contracts.Pure] public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(Value);
    [System.Diagnostics.Contracts.Pure] public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(Value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(Value);
    [System.Diagnostics.Contracts.Pure] public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(Value);
    [System.Diagnostics.Contracts.Pure] public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", Value);
    [System.Diagnostics.Contracts.Pure] public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(Value, conversionType, provider);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(Value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(Value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(Value);
    #endregion IConvertible

    // IEquatable<>
    [System.Diagnostics.Contracts.Pure] public bool Equals(Length other) => m_value == other.m_value;

    // IFormattable
    [System.Diagnostics.Contracts.Pure] public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

    // IQuantifiable<>
    [System.Diagnostics.Contracts.Pure] public double Value { get => m_value; init => m_value = value; }
    // IUnitQuantifiable<>
    [System.Diagnostics.Contracts.Pure]
    public string ToUnitString(LengthUnit unit = DefaultUnit, string? format = null, bool preferUnicode = false, bool useFullName = false)
      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
    [System.Diagnostics.Contracts.Pure]
    public double ToUnitValue(LengthUnit unit = DefaultUnit)
      => unit switch
      {
        LengthUnit.Nanometer => m_value * 1E+9,
        LengthUnit.Micrometer => m_value * 1E+6,
        LengthUnit.Millimeter => m_value * 1E+3,
        LengthUnit.Centimeter => m_value * 1E+2,
        LengthUnit.Inch => m_value / 0.0254,
        LengthUnit.Decimeter => m_value * 1E+1,
        LengthUnit.Foot => m_value / 0.3048,
        LengthUnit.Yard => m_value / 0.9144,
        LengthUnit.Meter => m_value,
        LengthUnit.Kilometer => m_value * 1E-3,
        LengthUnit.Mile => m_value / 1609.344,
        LengthUnit.NauticalMile => m_value / 1852,
        LengthUnit.AstronomicalUnit => m_value / 149597870700,
        LengthUnit.Parsec => m_value / OneParsecInMeters,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is Length o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => m_value.GetHashCode();
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{nameof(Length)} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
