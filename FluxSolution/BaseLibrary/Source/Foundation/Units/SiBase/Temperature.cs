namespace Flux.Units
{
  public enum TemperatureUnit
  {
    Celsius,
    Fahrenheit,
    Kelvin,
    Rankine,
  }

  /// <summary>Temperature.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Temperature"/>
  public struct Temperature
    : System.IComparable<Temperature>, System.IEquatable<Temperature>, IStandardizedScalar
  {
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

    private readonly double m_kelvin;

    public Temperature(double kelvin)
      => m_kelvin = kelvin;

    public double Kelvin
      => m_kelvin;

    public double ToUnitValue(TemperatureUnit unit)
    {
      switch (unit)
      {
        case TemperatureUnit.Celsius:
          return m_kelvin - KelvinIcePoint;
        case TemperatureUnit.Fahrenheit:
          return m_kelvin * 1.8 + FahrenheitAbsoluteZero;
        case TemperatureUnit.Kelvin:
          return m_kelvin;
        case TemperatureUnit.Rankine:
          return m_kelvin * 1.8;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }

    #region Static methods
    /// <summary>Convert the temperature specified in Celsius to Fahrenheit.</summary>
    public static double ConvertCelsiusToFahrenheit(double celsius)
      => celsius * 1.8 + FahrenheitIcePoint;
    /// <summary>Convert the temperature specified in Celsius to Kelvin.</summary>
    public static double ConvertCelsiusToKelvin(double celsius)
      => celsius - CelsiusAbsoluteZero;
    /// <summary>Convert the temperature specified in Celsius to Rankine.</summary>
    public static double ConvertCelsiusToRankine(double celsius)
      => (celsius - CelsiusAbsoluteZero) * 1.8;
    /// <summary>Convert the temperature specified in Fahrenheit to Celsius.</summary>
    public static double ConvertFahrenheitToCelsius(double fahrenheit)
      => (fahrenheit - FahrenheitIcePoint) / 1.8;
    /// <summary>Convert the temperature specified in Fahrenheit to Kelvin.</summary>
    public static double ConvertFahrenheitToKelvin(double fahrenheit)
      => (fahrenheit - FahrenheitAbsoluteZero) / 1.8;
    /// <summary>Convert the temperature specified in Fahrenheit to Rankine.</summary>
    public static double ConvertFahrenheitToRankine(double fahrenheit)
      => fahrenheit - FahrenheitAbsoluteZero;
    /// <summary>Convert the temperature specified in Kelvin to Celsius.</summary>
    public static double ConvertKelvinToCelsius(double kelvin)
      => kelvin - KelvinIcePoint;
    /// <summary>Convert the temperature specified in Kelvin to Fahrenheit.</summary>
    public static double ConvertKelvinToFahrenheit(double kelvin)
      => kelvin * 1.8 + FahrenheitAbsoluteZero;
    /// <summary>Convert the temperature specified in Kelvin to Rankine.</summary>
    public static double ConvertKelvinToRankine(double kelvin)
      => kelvin * 1.8;
    /// <summary>Convert the temperature specified in Rankine to Celsius.</summary>
    public static double ConvertRankineToCelsius(double rankine)
      => (rankine - RankineIcePoint) / 1.8;
    /// <summary>Convert the temperature specified in Rankine to Kelvin.</summary>
    public static double ConvertRankineToKelvin(double rankine)
      => rankine / 1.8;
    /// <summary>Convert the temperature specified in Rankine to Fahrenheit.</summary>
    public static double ConvertRankineToFahrenheit(double rankine)
      => rankine - RankineIcePoint;

    public static Temperature Add(Temperature left, Temperature right)
      => new Temperature(left.m_kelvin + right.m_kelvin);
    public static Temperature Divide(Temperature left, Temperature right)
      => new Temperature(left.m_kelvin / right.m_kelvin);
    public static Temperature FromUnitValue(TemperatureUnit unit, double value)
    {
      switch (unit)
      {
        case TemperatureUnit.Celsius:
          return new Temperature(value - CelsiusAbsoluteZero);
        case TemperatureUnit.Fahrenheit:
          return new Temperature((value - FahrenheitAbsoluteZero) / 1.8);
        case TemperatureUnit.Kelvin:
          return new Temperature(value);
        case TemperatureUnit.Rankine:
          return new Temperature(value / 1.8);
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }
    public static Temperature Multiply(Temperature left, Temperature right)
      => new Temperature(left.m_kelvin * right.m_kelvin);
    public static Temperature Negate(Temperature value)
      => new Temperature(-value.m_kelvin);
    public static Temperature Remainder(Temperature dividend, Temperature divisor)
      => new Temperature(dividend.m_kelvin % divisor.m_kelvin);
    public static Temperature Subtract(Temperature left, Temperature right)
      => new Temperature(left.m_kelvin - right.m_kelvin);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Temperature v)
      => v.m_kelvin;
    public static explicit operator Temperature(double v)
      => new Temperature(v);

    public static bool operator <(Temperature a, Temperature b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Temperature a, Temperature b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Temperature a, Temperature b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Temperature a, Temperature b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Temperature a, Temperature b)
      => a.Equals(b);
    public static bool operator !=(Temperature a, Temperature b)
      => !a.Equals(b);

    public static Temperature operator +(Temperature a, Temperature b)
      => Add(a, b);
    public static Temperature operator /(Temperature a, Temperature b)
      => Divide(a, b);
    public static Temperature operator %(Temperature a, Temperature b)
      => Remainder(a, b);
    public static Temperature operator *(Temperature a, Temperature b)
      => Multiply(a, b);
    public static Temperature operator -(Temperature a, Temperature b)
      => Subtract(a, b);
    public static Temperature operator -(Temperature v)
      => Negate(v);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Temperature other)
      => m_kelvin.CompareTo(other.m_kelvin);

    // IEquatable<Angle>
    public bool Equals(Temperature other)
      => m_kelvin == other.m_kelvin;

    // IUnitStandardized
    public double GetScalar()
      => m_kelvin;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Temperature o && Equals(o);
    public override int GetHashCode()
      => m_kelvin.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_kelvin} K>";
    #endregion Object overrides
  }
}
