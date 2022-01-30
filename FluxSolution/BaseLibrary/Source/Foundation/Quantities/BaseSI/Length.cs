namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this LengthUnit unit, bool useNameInstead = false, bool useUnicodeIfAvailable = false)
      => useNameInstead ? unit.ToString() : unit switch
      {
        LengthUnit.Millimeter => "mm",
        LengthUnit.Centimeter => "cm",
        LengthUnit.Inch => "in",
        LengthUnit.Decimeter => "dm",
        LengthUnit.Foot => "ft",
        LengthUnit.Yard => "yd",
        LengthUnit.Meter => "m",
        LengthUnit.NauticalMile => "nm",
        LengthUnit.Mile => "mi",
        LengthUnit.Kilometer => "km",
        LengthUnit.AstronomicalUnit => "au",
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
    : System.IComparable<Length>, System.IConvertible, System.IEquatable<Length>, IMetricPrefixFormattable, IValueSiBaseUnit<double>
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

    public double Value
      => m_value;

    public string GetMetricPrefixString(MetricMultiplicativePrefix prefix, string? format = null, bool useNameInstead = false, bool useUnicodeIfAvailable = false)
      => $"{new MetricMultiplicative(m_value, MetricMultiplicativePrefix.None).ToPrefixString(prefix, format, useNameInstead, useUnicodeIfAvailable)}{MassUnit.Gram.GetUnitString(useNameInstead, useUnicodeIfAvailable)}";

    public string ToUnitString(LengthUnit unit = DefaultUnit, string? format = null)
      => $"{string.Format($"{{0:{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString()}";
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
      => new(phaseVelocity.Value / frequency.Value);
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

    #region IConvertible
    public System.TypeCode GetTypeCode() => System.TypeCode.Object;
    public bool ToBoolean(System.IFormatProvider? provider) => Value != 0;
    public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(Value);
    public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(Value);
    public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(Value);
    public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(Value);
    public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(Value);
    public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(Value);
    public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(Value);
    public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(Value);
    [System.CLSCompliant(false)] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(Value);
    public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(Value);
    public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", Value);
    public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(Value, conversionType, provider);
    [System.CLSCompliant(false)] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(Value);
    [System.CLSCompliant(false)] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(Value);
    [System.CLSCompliant(false)] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(Value);
    #endregion IConvertible

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
