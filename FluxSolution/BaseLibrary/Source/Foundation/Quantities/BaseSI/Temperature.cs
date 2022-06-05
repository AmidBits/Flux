namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this TemperatureUnit unit, bool useFullName = false, bool preferUnicode = false)
      => useFullName ? unit.ToString() : unit switch
      {
        TemperatureUnit.Celsius => preferUnicode ? "\u2103" : "\u00B0C",
        TemperatureUnit.Fahrenheit => preferUnicode ? "\u2109" : "\u00B0F",
        TemperatureUnit.Kelvin => preferUnicode ? "\u212A" : $"K",
        TemperatureUnit.Rankine => $"\u00B0R",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum TemperatureUnit
  {
    /// <summary>This is the default unit for temperature.</summary>
    Kelvin,
    Celsius,
    Fahrenheit,
    Rankine,
  }

  /// <summary>Temperature. SI unit of Kelvin. This is a base quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Temperature"/>
  public readonly struct Temperature
    : System.IComparable, System.IComparable<Temperature>, System.IConvertible, System.IEquatable<Temperature>, System.IFormattable, IUnitQuantifiable<double, TemperatureUnit>
  {
    public const TemperatureUnit DefaultUnit = TemperatureUnit.Kelvin;

    public const double CelsiusAbsoluteZero = -273.15;
    public const double CelsiusBoilingPoint = 99.9839;
    public const double CelsiusIcePoint = 0;

    public const double FahrenheitAbsoluteZero = -459.67;
    public const double FahrenheitBoilingPoint = 211.97102;
    public const double FahrenheitIcePoint = 32;

    public const double KelvinAbsoluteZero = 0;
    public const double KelvinBoilingPoint = 373.1339;
    public const double KelvinIcePoint = 273.15;

    public const double RankineAbsoluteZero = 0;
    public const double RankineBoilingPoint = 671.64102;
    public const double RankineIcePoint = 491.67;

    private readonly double m_value;

    public Temperature(double value, TemperatureUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        TemperatureUnit.Celsius => value - CelsiusAbsoluteZero,
        TemperatureUnit.Fahrenheit => (value - FahrenheitAbsoluteZero) / 1.8,
        TemperatureUnit.Kelvin => value,
        TemperatureUnit.Rankine => value / 1.8,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    /// <summary>Convert the temperature specified in Celsius to Fahrenheit.</summary>
    [System.Diagnostics.Contracts.Pure] public static double ConvertCelsiusToFahrenheit(double celsius) => celsius * 1.8 + FahrenheitIcePoint;
    /// <summary>Convert the temperature specified in Celsius to Kelvin.</summary>
    [System.Diagnostics.Contracts.Pure] public static double ConvertCelsiusToKelvin(double celsius) => celsius - CelsiusAbsoluteZero;
    /// <summary>Convert the temperature specified in Celsius to Rankine.</summary>
    [System.Diagnostics.Contracts.Pure] public static double ConvertCelsiusToRankine(double celsius) => (celsius - CelsiusAbsoluteZero) * 1.8;
    /// <summary>Convert the temperature specified in Fahrenheit to Celsius.</summary>
    [System.Diagnostics.Contracts.Pure] public static double ConvertFahrenheitToCelsius(double fahrenheit) => (fahrenheit - FahrenheitIcePoint) / 1.8;
    /// <summary>Convert the temperature specified in Fahrenheit to Kelvin.</summary>
    [System.Diagnostics.Contracts.Pure] public static double ConvertFahrenheitToKelvin(double fahrenheit) => (fahrenheit - FahrenheitAbsoluteZero) / 1.8;
    /// <summary>Convert the temperature specified in Fahrenheit to Rankine.</summary>
    [System.Diagnostics.Contracts.Pure] public static double ConvertFahrenheitToRankine(double fahrenheit) => fahrenheit - FahrenheitAbsoluteZero;
    /// <summary>Convert the temperature specified in Kelvin to Celsius.</summary>
    [System.Diagnostics.Contracts.Pure] public static double ConvertKelvinToCelsius(double kelvin) => kelvin - KelvinIcePoint;
    /// <summary>Convert the temperature specified in Kelvin to Fahrenheit.</summary>
    [System.Diagnostics.Contracts.Pure] public static double ConvertKelvinToFahrenheit(double kelvin) => kelvin * 1.8 + FahrenheitAbsoluteZero;
    /// <summary>Convert the temperature specified in Kelvin to Rankine.</summary>
    [System.Diagnostics.Contracts.Pure] public static double ConvertKelvinToRankine(double kelvin) => kelvin * 1.8;
    /// <summary>Convert the temperature specified in Rankine to Celsius.</summary>
    [System.Diagnostics.Contracts.Pure] public static double ConvertRankineToCelsius(double rankine) => (rankine - RankineIcePoint) / 1.8;
    /// <summary>Convert the temperature specified in Rankine to Kelvin.</summary>
    [System.Diagnostics.Contracts.Pure] public static double ConvertRankineToKelvin(double rankine) => rankine / 1.8;
    /// <summary>Convert the temperature specified in Rankine to Fahrenheit.</summary>
    [System.Diagnostics.Contracts.Pure] public static double ConvertRankineToFahrenheit(double rankine) => rankine - RankineIcePoint;
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static explicit operator double(Temperature v) => v.m_value;
    [System.Diagnostics.Contracts.Pure] public static explicit operator Temperature(double v) => new(v);

    [System.Diagnostics.Contracts.Pure] public static bool operator <(Temperature a, Temperature b) => a.CompareTo(b) < 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator <=(Temperature a, Temperature b) => a.CompareTo(b) <= 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >(Temperature a, Temperature b) => a.CompareTo(b) > 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >=(Temperature a, Temperature b) => a.CompareTo(b) >= 0;

    [System.Diagnostics.Contracts.Pure] public static bool operator ==(Temperature a, Temperature b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(Temperature a, Temperature b) => !a.Equals(b);

    [System.Diagnostics.Contracts.Pure] public static Temperature operator -(Temperature v) => new(-v.m_value);
    [System.Diagnostics.Contracts.Pure] public static Temperature operator +(Temperature a, double b) => new(a.m_value + b);
    [System.Diagnostics.Contracts.Pure] public static Temperature operator +(Temperature a, Temperature b) => a + b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Temperature operator /(Temperature a, double b) => new(a.m_value / b);
    [System.Diagnostics.Contracts.Pure] public static Temperature operator /(Temperature a, Temperature b) => a / b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Temperature operator *(Temperature a, double b) => new(a.m_value * b);
    [System.Diagnostics.Contracts.Pure] public static Temperature operator *(Temperature a, Temperature b) => a * b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Temperature operator %(Temperature a, double b) => new(a.m_value % b);
    [System.Diagnostics.Contracts.Pure] public static Temperature operator %(Temperature a, Temperature b) => a % b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Temperature operator -(Temperature a, double b) => new(a.m_value - b);
    [System.Diagnostics.Contracts.Pure] public static Temperature operator -(Temperature a, Temperature b) => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<>
    [System.Diagnostics.Contracts.Pure] public int CompareTo(Temperature other) => m_value.CompareTo(other.m_value);
    // IComparable
    [System.Diagnostics.Contracts.Pure] public int CompareTo(object? other) => other is not null && other is Temperature o ? CompareTo(o) : -1;

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
    [System.Diagnostics.Contracts.Pure] public bool Equals(Temperature other) => m_value == other.m_value;

    // IFormattable
    [System.Diagnostics.Contracts.Pure] public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

    // IQuantifiable<>
    [System.Diagnostics.Contracts.Pure] public double Value { get => m_value; init => m_value = value; }
    // IUnitQuantifiable<>
    [System.Diagnostics.Contracts.Pure]
    public string ToUnitString(TemperatureUnit unit = DefaultUnit, string? format = null, bool useFullName = false, bool preferUnicode = false)
      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(useFullName, preferUnicode)}";
    [System.Diagnostics.Contracts.Pure]
    public double ToUnitValue(TemperatureUnit unit = DefaultUnit)
      => unit switch
      {
        TemperatureUnit.Celsius => m_value - KelvinIcePoint,
        TemperatureUnit.Fahrenheit => m_value * 1.8 + FahrenheitAbsoluteZero,
        TemperatureUnit.Kelvin => m_value,
        TemperatureUnit.Rankine => m_value * 1.8,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is Temperature o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => m_value.GetHashCode();
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ {ToUnitString()} }}";
    #endregion Object overrides
  }
}
