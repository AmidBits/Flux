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
    : System.IComparable, System.IComparable<Temperature>, System.IFormattable, ISiUnitValueQuantifiable<double, TemperatureUnit>
  {
    public static readonly Temperature AbsoluteZero;

    private readonly double m_value;

    public Temperature(double value, TemperatureUnit unit = TemperatureUnit.Kelvin) => m_value = ConvertFromUnit(unit, value);

    #region Static methods

    #region Conversion methods

    /// <summary>Convert the temperature specified in Celsius to Fahrenheit.</summary>
    public static double ConvertCelsiusToFahrenheit(double celsius) => celsius * 1.8 + 32;
    /// <summary>Convert the temperature specified in Celsius to Kelvin.</summary>
    public static double ConvertCelsiusToKelvin(double celsius) => celsius + 273.15;
    /// <summary>Convert the temperature specified in Celsius to Rankine.</summary>
    public static double ConvertCelsiusToRankine(double celsius) => (celsius + 273.15) * 1.8;
    /// <summary>Convert the temperature specified in Fahrenheit to Celsius.</summary>
    public static double ConvertFahrenheitToCelsius(double fahrenheit) => (fahrenheit - 32) / 1.8;
    /// <summary>Convert the temperature specified in Fahrenheit to Kelvin.</summary>
    public static double ConvertFahrenheitToKelvin(double fahrenheit) => (fahrenheit + 459.67) / 1.8;
    /// <summary>Convert the temperature specified in Fahrenheit to Rankine.</summary>
    public static double ConvertFahrenheitToRankine(double fahrenheit) => fahrenheit + 459.67;
    /// <summary>Convert the temperature specified in Kelvin to Celsius.</summary>
    public static double ConvertKelvinToCelsius(double kelvin) => kelvin - 273.15;
    /// <summary>Convert the temperature specified in Kelvin to Fahrenheit.</summary>
    public static double ConvertKelvinToFahrenheit(double kelvin) => kelvin * 1.8 - 459.67;
    /// <summary>Convert the temperature specified in Kelvin to Rankine.</summary>
    public static double ConvertKelvinToRankine(double kelvin) => kelvin * 1.8;
    /// <summary>Convert the temperature specified in Rankine to Celsius.</summary>
    public static double ConvertRankineToCelsius(double rankine) => (rankine - 491.67) / 1.8;
    /// <summary>Convert the temperature specified in Rankine to Kelvin.</summary>
    public static double ConvertRankineToKelvin(double rankine) => rankine / 1.8;
    /// <summary>Convert the temperature specified in Rankine to Fahrenheit.</summary>
    public static double ConvertRankineToFahrenheit(double rankine) => rankine - 491.67;

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

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region IQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Temperature.Value"/> property is in <see cref="TemperatureUnit.Kelvin"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region ISiUnitValueQuantifiable<>

    public string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(TemperatureUnit.Kelvin, preferPlural);

    public string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(TemperatureUnit.Kelvin, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, true) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitQuantifiable<>

    private static double ConvertFromUnit(TemperatureUnit unit, double value)
      => unit switch
      {
        TemperatureUnit.Kelvin => value,

        TemperatureUnit.Celsius => ConvertCelsiusToKelvin(value),
        TemperatureUnit.Fahrenheit => ConvertFahrenheitToKelvin(value),
        TemperatureUnit.Rankine => ConvertRankineToKelvin(value),

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(TemperatureUnit unit, double value)
      => unit switch
      {
        TemperatureUnit.Kelvin => value,

        TemperatureUnit.Celsius => ConvertKelvinToCelsius(value),
        TemperatureUnit.Fahrenheit => ConvertKelvinToFahrenheit(value),
        TemperatureUnit.Rankine => ConvertKelvinToRankine(value),

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, TemperatureUnit from, TemperatureUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(TemperatureUnit unit)
      => unit switch
      {
        TemperatureUnit.Kelvin => 1,

        _ => throw new System.NotImplementedException()
      };

    public string GetUnitName(TemperatureUnit unit, bool preferPlural) => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(TemperatureUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.TemperatureUnit.Celsius => preferUnicode ? "\u2103" : "\u00B0C",
        Quantities.TemperatureUnit.Fahrenheit => preferUnicode ? "\u2109" : "\u00B0F",
        Quantities.TemperatureUnit.Kelvin => preferUnicode ? "\u212A" : "K",
        Quantities.TemperatureUnit.Rankine => $"\u00B0Ra",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(TemperatureUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(TemperatureUnit unit = TemperatureUnit.Kelvin, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}