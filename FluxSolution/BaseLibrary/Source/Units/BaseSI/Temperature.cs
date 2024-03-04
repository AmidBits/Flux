namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.TemperatureUnit unit, bool preferUnicode = false, bool useFullName = false)
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
      /// <summary>This is the default unit for <see cref="Temperature"/>.</summary>
      Kelvin,
      Celsius,
      Fahrenheit,
      Rankine,
    }

    /// <summary>Temperature. SI unit of Kelvin. This is a base quantity.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Temperature"/>
    public readonly record struct Temperature
      : System.IComparable, System.IComparable<Temperature>, System.IFormattable, IUnitValueQuantifiable<double, TemperatureUnit>
    {
      public static readonly Temperature AbsoluteZero;

      private readonly double m_value;

      public Temperature(double value, TemperatureUnit unit = TemperatureUnit.Kelvin)
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
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(TemperatureUnit.Kelvin, UnitValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="Temperature.Value"/> property is in <see cref="TemperatureUnit.Kelvin"/>.</para>
      /// </summary>
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

      public string ToUnitValueString(TemperatureUnit unit, string? format, System.IFormatProvider? formatProvider, bool preferUnicode, UnicodeSpacing unicodeSpacing, bool useFullName)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unicodeSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(preferUnicode, useFullName));
        return sb.ToString();
      }

      public string ToUnitValueString(TemperatureUnit unit, UnitValueStringOptions options = default)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(options.Format, options.FormatProvider));
        sb.Append(options.UnitSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(options.PreferUnicode, options.UseFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
