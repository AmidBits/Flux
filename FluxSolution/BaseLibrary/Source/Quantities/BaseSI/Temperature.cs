namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this Quantities.TemperatureUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.TemperatureUnit.Celsius => preferUnicode ? "\u2103" : "\u00B0C",
        Quantities.TemperatureUnit.Fahrenheit => preferUnicode ? "\u2109" : "\u00B0F",
        Quantities.TemperatureUnit.Kelvin => preferUnicode ? "\u212A" : $"K",
        Quantities.TemperatureUnit.Rankine => $"\u00B0R",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
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
    public readonly record struct Temperature
      : System.IComparable, System.IComparable<Temperature>, System.IConvertible, System.IFormattable, IUnitQuantifiable<double, TemperatureUnit>
    {
      public static readonly Temperature Zero;

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
      public static double ConvertCelsiusToFahrenheit(double celsius) => celsius * 1.8 + FahrenheitIcePoint;
      /// <summary>Convert the temperature specified in Celsius to Kelvin.</summary>
      public static double ConvertCelsiusToKelvin(double celsius) => celsius - CelsiusAbsoluteZero;
      /// <summary>Convert the temperature specified in Celsius to Rankine.</summary>
      public static double ConvertCelsiusToRankine(double celsius) => (celsius - CelsiusAbsoluteZero) * 1.8;
      /// <summary>Convert the temperature specified in Fahrenheit to Celsius.</summary>
      public static double ConvertFahrenheitToCelsius(double fahrenheit) => (fahrenheit - FahrenheitIcePoint) / 1.8;
      /// <summary>Convert the temperature specified in Fahrenheit to Kelvin.</summary>
      public static double ConvertFahrenheitToKelvin(double fahrenheit) => (fahrenheit - FahrenheitAbsoluteZero) / 1.8;
      /// <summary>Convert the temperature specified in Fahrenheit to Rankine.</summary>
      public static double ConvertFahrenheitToRankine(double fahrenheit) => fahrenheit - FahrenheitAbsoluteZero;
      /// <summary>Convert the temperature specified in Kelvin to Celsius.</summary>
      public static double ConvertKelvinToCelsius(double kelvin) => kelvin - KelvinIcePoint;
      /// <summary>Convert the temperature specified in Kelvin to Fahrenheit.</summary>
      public static double ConvertKelvinToFahrenheit(double kelvin) => kelvin * 1.8 + FahrenheitAbsoluteZero;
      /// <summary>Convert the temperature specified in Kelvin to Rankine.</summary>
      public static double ConvertKelvinToRankine(double kelvin) => kelvin * 1.8;
      /// <summary>Convert the temperature specified in Rankine to Celsius.</summary>
      public static double ConvertRankineToCelsius(double rankine) => (rankine - RankineIcePoint) / 1.8;
      /// <summary>Convert the temperature specified in Rankine to Kelvin.</summary>
      public static double ConvertRankineToKelvin(double rankine) => rankine / 1.8;
      /// <summary>Convert the temperature specified in Rankine to Fahrenheit.</summary>
      public static double ConvertRankineToFahrenheit(double rankine) => rankine - RankineIcePoint;
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(Temperature v) => v.m_value;
      public static explicit operator Temperature(double v) => new(v);

      public static bool operator <(Temperature a, Temperature b) => a.CompareTo(b) < 0;
      public static bool operator <=(Temperature a, Temperature b) => a.CompareTo(b) <= 0;
      public static bool operator >(Temperature a, Temperature b) => a.CompareTo(b) > 0;
      public static bool operator >=(Temperature a, Temperature b) => a.CompareTo(b) >= 0;

      public static Temperature operator -(Temperature v) => new(-v.m_value);
      public static Temperature operator +(Temperature a, double b) => new(a.m_value + b);
      public static Temperature operator +(Temperature a, Temperature b) => a + b.m_value;
      public static Temperature operator /(Temperature a, double b) => new(a.m_value / b);
      public static Temperature operator /(Temperature a, Temperature b) => a / b.m_value;
      public static Temperature operator *(Temperature a, double b) => new(a.m_value * b);
      public static Temperature operator *(Temperature a, Temperature b) => a * b.m_value;
      public static Temperature operator %(Temperature a, double b) => new(a.m_value % b);
      public static Temperature operator %(Temperature a, Temperature b) => a % b.m_value;
      public static Temperature operator -(Temperature a, double b) => new(a.m_value - b);
      public static Temperature operator -(Temperature a, Temperature b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces
      // IComparable<>
      public int CompareTo(Temperature other) => m_value.CompareTo(other.m_value);
      // IComparable
      public int CompareTo(object? other) => other is not null && other is Temperature o ? CompareTo(o) : -1;

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

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false)
        => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);
      public double Value { get => m_value; init => m_value = value; }
      // IUnitQuantifiable<>
      public string ToUnitString(TemperatureUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

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

      public override string ToString() => ToQuantityString();
    }
  }
}
