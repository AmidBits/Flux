namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.TemperatureUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.TemperatureUnit.Celsius => preferUnicode ? "\u2103" : "\u00B0C",
        Units.TemperatureUnit.Fahrenheit => preferUnicode ? "\u2109" : "\u00B0F",
        Units.TemperatureUnit.Kelvin => preferUnicode ? "\u212A" : "K",
        Units.TemperatureUnit.Rankine => $"\u00B0R",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
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
      : System.IComparable, System.IComparable<Temperature>, System.IFormattable, IUnitValueQuantifiable<double, TemperatureUnit>
    {
      public const TemperatureUnit DefaultUnit = TemperatureUnit.Kelvin;

      public static readonly Temperature AbsoluteZero;

      private readonly double m_value;

      public Temperature(double value, TemperatureUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          TemperatureUnit.Celsius => CelsiusToKelvin(value),
          TemperatureUnit.Fahrenheit => FahrenheitToKelvin(value),
          TemperatureUnit.Kelvin => value,
          TemperatureUnit.Rankine => RankineToKelvin(value),
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods

      /// <summary>Convert the temperature specified in Celsius to Fahrenheit.</summary>
      public static double CelsiusToFahrenheit(double celsius) => celsius * 1.8 + 32;
      /// <summary>Convert the temperature specified in Celsius to Kelvin.</summary>
      public static double CelsiusToKelvin(double celsius) => celsius + 273.15;
      /// <summary>Convert the temperature specified in Celsius to Rankine.</summary>
      public static double CelsiusToRankine(double celsius) => (celsius + 273.15) * 1.8;
      /// <summary>Convert the temperature specified in Fahrenheit to Celsius.</summary>
      public static double FahrenheitToCelsius(double fahrenheit) => (fahrenheit - 32) / 1.8;
      /// <summary>Convert the temperature specified in Fahrenheit to Kelvin.</summary>
      public static double FahrenheitToKelvin(double fahrenheit) => (fahrenheit + 459.67) / 1.8;
      /// <summary>Convert the temperature specified in Fahrenheit to Rankine.</summary>
      public static double FahrenheitToRankine(double fahrenheit) => fahrenheit + 459.67;
      /// <summary>Convert the temperature specified in Kelvin to Celsius.</summary>
      public static double KelvinToCelsius(double kelvin) => kelvin - 273.15;
      /// <summary>Convert the temperature specified in Kelvin to Fahrenheit.</summary>
      public static double KelvinToFahrenheit(double kelvin) => kelvin * 1.8 - 459.67;
      /// <summary>Convert the temperature specified in Kelvin to Rankine.</summary>
      public static double KelvinToRankine(double kelvin) => kelvin * 1.8;
      /// <summary>Convert the temperature specified in Rankine to Celsius.</summary>
      public static double RankineToCelsius(double rankine) => (rankine - 491.67) / 1.8;
      /// <summary>Convert the temperature specified in Rankine to Kelvin.</summary>
      public static double RankineToKelvin(double rankine) => rankine / 1.8;
      /// <summary>Convert the temperature specified in Rankine to Fahrenheit.</summary>
      public static double RankineToFahrenheit(double rankine) => rankine - 491.67;

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

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Temperature o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Temperature other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToValueString(string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => ToUnitValueString(DefaultUnit, format, preferUnicode, useFullName, culture);

      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(TemperatureUnit unit)
        => unit switch
        {
          TemperatureUnit.Celsius => KelvinToCelsius(m_value),
          TemperatureUnit.Fahrenheit => KelvinToFahrenheit(m_value),
          TemperatureUnit.Kelvin => m_value,
          TemperatureUnit.Rankine => KelvinToRankine(m_value),
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(TemperatureUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => $"{string.Format(culture, $"{{0{(format is null ? string.Empty : $":{format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
