namespace Flux.Quantities
{
  public enum TemperatureUnit
  {
    /// <summary>This is the default unit for <see cref="Temperature"/>.</summary>
    Kelvin,
    Celsius,
    Fahrenheit,
    Rankine,
  }

  /// <summary>
  /// <para>Temperature. SI unit of Kelvin. This is a base quantity.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Temperature"/></para>
  /// </summary>
  public readonly record struct Temperature
    : System.IComparable, System.IComparable<Temperature>, System.IFormattable, ISiPrefixValueQuantifiable<double, TemperatureUnit>
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

    #region Conversion methods

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

    #endregion // Conversion methods

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

    readonly System.Globalization.NumberFormatInfo m_nfi = new System.Globalization.NumberFormatInfo() { NumberDecimalSeparator = ",", NumberGroupSeparator = " " };

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiPrefixValueSymbolString(MetricPrefix.NoPrefix, format, formatProvider);

    #region IQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Temperature.Value"/> property is in <see cref="TemperatureUnit.Kelvin"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region ISiUnitValueQuantifiable<>

    public string GetSiPrefixName(MetricPrefix prefix, bool preferPlural) => prefix.GetUnitName() + GetUnitName(TemperatureUnit.Kelvin, preferPlural);

    public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetUnitSymbol(preferUnicode) + GetUnitSymbol(TemperatureUnit.Kelvin, preferUnicode);

    public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.NoPrefix.Convert(m_value, prefix);

    public string ToSiPrefixValueNameString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = true)
      => GetSiPrefixValue(prefix).ToSiFormattedString() + unitSpacing.ToSpacingString() + GetSiPrefixName(prefix, preferPlural);

    public string ToSiPrefixValueSymbolString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => GetSiPrefixValue(prefix).ToSiFormattedString() + unitSpacing.ToSpacingString() + GetSiPrefixSymbol(prefix, preferUnicode);

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitQuantifiable<>

    public string GetUnitName(TemperatureUnit unit, bool preferPlural) => unit.ToString() is var us && preferPlural ? us + GetUnitValue(unit).PluralStringSuffix() : us;

    public string GetUnitSymbol(TemperatureUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.TemperatureUnit.Celsius => preferUnicode ? "\u2103" : "\u00B0C",
        Quantities.TemperatureUnit.Fahrenheit => preferUnicode ? "\u2109" : "\u00B0F",
        Quantities.TemperatureUnit.Kelvin => preferUnicode ? "\u212A" : "K",
        Quantities.TemperatureUnit.Rankine => $"\u00B0Ra",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(TemperatureUnit unit)
        => unit switch
        {
          TemperatureUnit.Celsius => KelvinToCelsius(m_value),
          TemperatureUnit.Fahrenheit => KelvinToFahrenheit(m_value),
          TemperatureUnit.Kelvin => m_value,
          TemperatureUnit.Rankine => KelvinToRankine(m_value),
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

    public string ToUnitValueNameString(TemperatureUnit unit = TemperatureUnit.Kelvin, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitName(unit, preferPlural);

    public string ToUnitValueSymbolString(TemperatureUnit unit = TemperatureUnit.Kelvin, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitSymbol(unit, preferUnicode);

    #endregion // IUnitQuantifiable<>

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
