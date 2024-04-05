namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Quantities.ThermaldynamicTemperatureUnit unit, bool preferUnicode = false, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.ThermaldynamicTemperatureUnit.Celsius => preferUnicode ? "\u2103" : "\u00B0C",
        Quantities.ThermaldynamicTemperatureUnit.Fahrenheit => preferUnicode ? "\u2109" : "\u00B0F",
        Quantities.ThermaldynamicTemperatureUnit.Kelvin => preferUnicode ? "\u212A" : "K",
        Quantities.ThermaldynamicTemperatureUnit.Rankine => $"\u00B0R",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum ThermaldynamicTemperatureUnit
    {
      /// <summary>This is the default unit for <see cref="ThermaldynamicTemperature"/>.</summary>
      Kelvin,
      Celsius,
      Fahrenheit,
      Rankine,
    }

    /// <summary>
    /// <para>Temperature. SI unit of Kelvin. This is a base quantity.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Temperature"/></para>
    /// </summary>
    public readonly record struct ThermaldynamicTemperature
      : System.IComparable, System.IComparable<ThermaldynamicTemperature>, System.IFormattable, IUnitValueQuantifiable<double, ThermaldynamicTemperatureUnit>
    {
      public static readonly ThermaldynamicTemperature AbsoluteZero;

      private readonly double m_value;

      public ThermaldynamicTemperature(double value, ThermaldynamicTemperatureUnit unit = ThermaldynamicTemperatureUnit.Kelvin)
        => m_value = unit switch
        {
          ThermaldynamicTemperatureUnit.Celsius => CelsiusToKelvin(value),
          ThermaldynamicTemperatureUnit.Fahrenheit => FahrenheitToKelvin(value),
          ThermaldynamicTemperatureUnit.Kelvin => value,
          ThermaldynamicTemperatureUnit.Rankine => RankineToKelvin(value),
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

      public static bool operator <(ThermaldynamicTemperature a, ThermaldynamicTemperature b) => a.CompareTo(b) < 0;
      public static bool operator <=(ThermaldynamicTemperature a, ThermaldynamicTemperature b) => a.CompareTo(b) <= 0;
      public static bool operator >(ThermaldynamicTemperature a, ThermaldynamicTemperature b) => a.CompareTo(b) > 0;
      public static bool operator >=(ThermaldynamicTemperature a, ThermaldynamicTemperature b) => a.CompareTo(b) >= 0;

      public static ThermaldynamicTemperature operator -(ThermaldynamicTemperature v) => new(-v.m_value);
      public static ThermaldynamicTemperature operator +(ThermaldynamicTemperature a, double b) => new(a.m_value + b);
      public static ThermaldynamicTemperature operator +(ThermaldynamicTemperature a, ThermaldynamicTemperature b) => a + b.m_value;
      public static ThermaldynamicTemperature operator /(ThermaldynamicTemperature a, double b) => new(a.m_value / b);
      public static ThermaldynamicTemperature operator /(ThermaldynamicTemperature a, ThermaldynamicTemperature b) => a / b.m_value;
      public static ThermaldynamicTemperature operator *(ThermaldynamicTemperature a, double b) => new(a.m_value * b);
      public static ThermaldynamicTemperature operator *(ThermaldynamicTemperature a, ThermaldynamicTemperature b) => a * b.m_value;
      public static ThermaldynamicTemperature operator %(ThermaldynamicTemperature a, double b) => new(a.m_value % b);
      public static ThermaldynamicTemperature operator %(ThermaldynamicTemperature a, ThermaldynamicTemperature b) => a % b.m_value;
      public static ThermaldynamicTemperature operator -(ThermaldynamicTemperature a, double b) => new(a.m_value - b);
      public static ThermaldynamicTemperature operator -(ThermaldynamicTemperature a, ThermaldynamicTemperature b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is ThermaldynamicTemperature o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(ThermaldynamicTemperature other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(ThermaldynamicTemperatureUnit.Kelvin, format, formatProvider);

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="ThermaldynamicTemperature.Value"/> property is in <see cref="ThermaldynamicTemperatureUnit.Kelvin"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public string GetUnitSymbol(ThermaldynamicTemperatureUnit unit, bool preferUnicode, bool useFullName) => unit.GetUnitString(preferUnicode, useFullName);

      public double GetUnitValue(ThermaldynamicTemperatureUnit unit)
        => unit switch
        {
          ThermaldynamicTemperatureUnit.Celsius => KelvinToCelsius(m_value),
          ThermaldynamicTemperatureUnit.Fahrenheit => KelvinToFahrenheit(m_value),
          ThermaldynamicTemperatureUnit.Kelvin => m_value,
          ThermaldynamicTemperatureUnit.Rankine => KelvinToRankine(m_value),
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(ThermaldynamicTemperatureUnit unit = ThermaldynamicTemperatureUnit.Kelvin, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unitSpacing.ToSpacingString());
        sb.Append(GetUnitSymbol(unit, preferUnicode, useFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
